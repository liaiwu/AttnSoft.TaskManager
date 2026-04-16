using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.BaseObjects;

namespace TaskManager.TaskCommon
{
    /// <summary>
    /// 任务处理帮助类
    /// </summary>
    public class QuartzHelper
    {
        private QuartzHelper() { }

        /// <summary>
        /// 缓存任务所在程序集信息
        /// </summary>
        private static readonly ConcurrentDictionary<string, Assembly> AssemblyDict = new ConcurrentDictionary<string, Assembly>(StringComparer.OrdinalIgnoreCase);

        private static readonly SemaphoreSlim SchedulerLock = new SemaphoreSlim(1, 1);

        private static IScheduler? scheduler;
        private static bool triggerListenerRegistered;
        private static bool jobListenerRegistered;
        private static readonly CustomTriggerListener CustomTriggerListener = new CustomTriggerListener();
        private static readonly CustomJobListener CustomJobListener = new CustomJobListener();

        private static IScheduler? GetSchedulerSnapshot()
        {
            return Volatile.Read(ref scheduler);
        }

        private static bool IsSchedulerAvailable(IScheduler? currentScheduler)
        {
            return currentScheduler != null && !currentScheduler.IsShutdown;
        }

        private static bool IsTriggerListenerRegistered()
        {
            return Volatile.Read(ref triggerListenerRegistered);
        }

        private static bool IsJobListenerRegistered()
        {
            return Volatile.Read(ref jobListenerRegistered);
        }

        /// <summary>
        /// Job 执行时调用
        /// </summary>
        public static event TriggerFiredEvent OnTriggerFired
        {
            add { CustomTriggerListener.OnTriggerFired += value; }
            remove { CustomTriggerListener.OnTriggerFired -= value; }
        }

        /// <summary>
        /// Trigger 触发后，job 执行时调用本方法。true 即否决，job 后面不执行。
        /// </summary>
        public static event VetoJobExecutionEvent OnVetoJobExecution
        {
            add { CustomTriggerListener.OnVetoJobExecution += value; }
            remove { CustomTriggerListener.OnVetoJobExecution -= value; }
        }

        /// <summary>
        /// Job 完成时调用
        /// </summary>
        public static event TriggerCompleteEvent OnTriggerComplete
        {
            add { CustomTriggerListener.OnTriggerComplete += value; }
            remove { CustomTriggerListener.OnTriggerComplete -= value; }
        }

        /// <summary>
        /// 错过触发时调用
        /// </summary>
        public static event TriggerMisfiredEvent OnTriggerMisfired
        {
            add { CustomTriggerListener.OnTriggerMisfired += value; }
            remove { CustomTriggerListener.OnTriggerMisfired -= value; }
        }

        /// <summary>
        /// 初始化任务调度对象
        /// </summary>
        public static async Task InitScheduler()
        {
            await EnsureSchedulerAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 启用任务调度
        /// 启动调度时会把任务表中的任务加入到任务调度队列中
        /// </summary>
        public static async Task StartScheduler()
        {
            try
            {
                IScheduler currentScheduler = await EnsureSchedulerAsync().ConfigureAwait(false);
                await RegisterTriggerListenerAsync(currentScheduler).ConfigureAwait(false);
                await RegisterJobListenerAsync(currentScheduler).ConfigureAwait(false);

                if (currentScheduler.IsStarted)
                {
                    return;
                }

                await SchedulePersistedJobsAsync(currentScheduler, false).ConfigureAwait(false);
                await currentScheduler.Start().ConfigureAwait(false);

                LogHelper.WriteLog("任务调度启动成功！");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("任务调度启动失败！", ex);
                throw;
            }
        }

        public static async Task Refresh()
        {
            try
            {
                IScheduler currentScheduler = await EnsureSchedulerAsync().ConfigureAwait(false);
                await RegisterTriggerListenerAsync(currentScheduler).ConfigureAwait(false);
                await RegisterJobListenerAsync(currentScheduler).ConfigureAwait(false);
                await SchedulePersistedJobsAsync(currentScheduler, true).ConfigureAwait(false);

                LogHelper.WriteLog("任务调度刷新成功！");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("任务调度刷新失败！", ex);
                throw;
            }
        }

        /// <summary>
        /// 删除现有任务
        /// </summary>
        public static async Task DeleteJob(string jobName)
        {
            IScheduler? currentScheduler = GetSchedulerSnapshot();
            if (!IsSchedulerAvailable(currentScheduler))
            {
                return;
            }

            await DeleteJobInternalAsync(new JobKey(jobName), currentScheduler!).ConfigureAwait(false);
        }

        /// <summary>
        /// 启用任务（如果任务已存在，先删除旧任务再添加新任务）
        /// </summary>
        /// <param name="taskUtil">任务信息</param>
        public static async Task ScheduleJob(TaskUtil taskUtil)
        {
            IScheduler currentScheduler = await EnsureSchedulerAsync().ConfigureAwait(false);
            await RegisterTriggerListenerAsync(currentScheduler).ConfigureAwait(false);
            await RegisterJobListenerAsync(currentScheduler).ConfigureAwait(false);
            await ScheduleJobInternalAsync(taskUtil, currentScheduler).ConfigureAwait(false);
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        public static async Task PauseJob(string jobName)
        {
            IScheduler? currentScheduler = GetSchedulerSnapshot();
            if (!IsSchedulerAvailable(currentScheduler))
            {
                return;
            }

            JobKey jobKey = new JobKey(jobName);
            if (await currentScheduler!.CheckExists(jobKey).ConfigureAwait(false))
            {
                await currentScheduler.PauseJob(jobKey).ConfigureAwait(false);
                LogHelper.WriteLog($"任务{jobName}已经暂停");
            }
        }

        /// <summary>
        /// 恢复运行暂停的任务
        /// </summary>
        public static async Task ResumeJob(string jobName)
        {
            IScheduler? currentScheduler = GetSchedulerSnapshot();
            if (!IsSchedulerAvailable(currentScheduler))
            {
                return;
            }

            JobKey jobKey = new JobKey(jobName);
            if (await currentScheduler!.CheckExists(jobKey).ConfigureAwait(false))
            {
                await currentScheduler.ResumeJob(jobKey).ConfigureAwait(false);
                LogHelper.WriteLog($"任务{jobName}恢复运行");
            }
        }

        public static async Task ExeTask(TaskUtil task)
        {
            if (task == null)
            {
                return;
            }

            IScheduler? currentScheduler = GetSchedulerSnapshot();
            if (!IsSchedulerAvailable(currentScheduler))
            {
                throw new InvalidOperationException("任务调度尚未初始化。");
            }

            JobKey jobKey = new JobKey(task.TaskID.ToString());
            if (!await currentScheduler!.CheckExists(jobKey).ConfigureAwait(false))
            {
                throw new InvalidOperationException($"任务{task.TaskName}不存在或未加入调度器，无法立即执行。");
            }

            await currentScheduler.TriggerJob(jobKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取类的属性、方法
        /// </summary>
        private static Type GetClassInfo(string assemblyName, string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentException("任务类型名不能为空。", nameof(className));
            }

            Assembly assembly;
            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                string assemblyFile = assemblyName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)
                    ? assemblyName
                    : assemblyName + ".dll";
                string assemblyPath = FileHelper.GetAbsolutePath(assemblyFile);
                assembly = AssemblyDict.GetOrAdd(assemblyPath, Assembly.LoadFrom);
            }
            else
            {
                assembly = typeof(QuartzHelper).Assembly;
            }

            return assembly.GetType(className, true, true);
        }

        public static async Task StopScheduleAsync(bool waitForJobComplete = false)
        {
            try
            {
                IScheduler? currentScheduler = GetSchedulerSnapshot();
                if (currentScheduler == null)
                {
                    return;
                }

                await SchedulerLock.WaitAsync().ConfigureAwait(false);
                try
                {
                    currentScheduler = GetSchedulerSnapshot();
                    if (!IsSchedulerAvailable(currentScheduler))
                    {
                        return;
                    }

                    await currentScheduler!.Shutdown(waitForJobComplete).ConfigureAwait(false);
                    if (ReferenceEquals(GetSchedulerSnapshot(), currentScheduler))
                    {
                        Volatile.Write(ref scheduler, null);
                    }
                    Volatile.Write(ref triggerListenerRegistered, false);
                    Volatile.Write(ref jobListenerRegistered, false);

                    LogHelper.WriteLog("任务调度停止！");
                }
                finally
                {
                    SchedulerLock.Release();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("任务调度停止失败！", ex);
                throw;
            }
        }

        /// <summary>
        /// 校验字符串是否为正确的 Cron 表达式
        /// </summary>
        public static bool ValidExpression(string cronExpression)
        {
            return CronExpression.IsValidExpression(cronExpression);
        }

        private static async Task<IScheduler> EnsureSchedulerAsync()
        {
            IScheduler? currentScheduler = GetSchedulerSnapshot();
            if (IsSchedulerAvailable(currentScheduler))
            {
                return currentScheduler!;
            }

            await SchedulerLock.WaitAsync().ConfigureAwait(false);
            try
            {
                currentScheduler = GetSchedulerSnapshot();
                if (IsSchedulerAvailable(currentScheduler))
                {
                    return currentScheduler!;
                }

                NameValueCollection properties = new NameValueCollection
                {
                    [StdSchedulerFactory.PropertySchedulerInstanceName] = "AttnSoft-DefaultQuartzScheduler"
                };

                ISchedulerFactory factory = new StdSchedulerFactory(properties);
                currentScheduler = await factory.GetScheduler().ConfigureAwait(false);
                Volatile.Write(ref scheduler, currentScheduler);
                Volatile.Write(ref triggerListenerRegistered, false);
                Volatile.Write(ref jobListenerRegistered, false);
                LogHelper.WriteLog("任务调度初始化成功！");
                return currentScheduler;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("任务调度初始化失败！", ex);
                throw;
            }
            finally
            {
                SchedulerLock.Release();
            }
        }

        private static async Task RegisterTriggerListenerAsync(IScheduler currentScheduler)
        {
            if (IsTriggerListenerRegistered())
            {
                return;
            }

            await SchedulerLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (IsTriggerListenerRegistered())
                {
                    return;
                }

                currentScheduler.ListenerManager.AddTriggerListener(CustomTriggerListener, GroupMatcher<TriggerKey>.AnyGroup());
                Volatile.Write(ref triggerListenerRegistered, true);
            }
            finally
            {
                SchedulerLock.Release();
            }
        }

        private static async Task RegisterJobListenerAsync(IScheduler currentScheduler)
        {
            if (IsJobListenerRegistered())
            {
                return;
            }

            await SchedulerLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (IsJobListenerRegistered())
                {
                    return;
                }

                currentScheduler.ListenerManager.AddJobListener(CustomJobListener, GroupMatcher<JobKey>.AnyGroup());
                Volatile.Write(ref jobListenerRegistered, true);
            }
            finally
            {
                SchedulerLock.Release();
            }
        }

        private static async Task SchedulePersistedJobsAsync(IScheduler currentScheduler, bool clearExistingJobs)
        {
            if (clearExistingJobs)
            {
                await currentScheduler.Clear().ConfigureAwait(false);
            }

            List<TaskUtil> listTask = TaskUtilService.Instance.GetAll();
            if (listTask == null || listTask.Count == 0)
            {
                return;
            }

            foreach (TaskUtil taskUtil in listTask)
            {
                try
                {
                    await ScheduleJobInternalAsync(taskUtil, currentScheduler).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog($"任务{taskUtil.TaskName}启动失败！", ex);
                }
            }
        }

        private static async Task ScheduleJobInternalAsync(TaskUtil taskUtil, IScheduler currentScheduler)
        {
            if (taskUtil == null)
            {
                throw new ArgumentNullException(nameof(taskUtil));
            }

            if (!ValidExpression(taskUtil.CronExpressionString))
            {
                throw new InvalidOperationException($"无效的 Cron 表达式：{taskUtil.CronExpressionString}");
            }

            JobKey jobKey = new JobKey(taskUtil.TaskID.ToString());

            // 如果任务已存在，先删除（支持更新场景）
            await DeleteJobInternalAsync(jobKey, currentScheduler).ConfigureAwait(false);

            Type jobType = GetClassInfo(taskUtil.Assembly, taskUtil.Class);
            TriggerKey triggerKey = new TriggerKey(taskUtil.TaskID.ToString());

            IJobDetail job = JobBuilder.Create(jobType)
                .WithIdentity(jobKey)
                .WithDescription(taskUtil.TaskName)
                .UsingJobData("TaskID", taskUtil.TaskID)
                .UsingJobData("TaskParam", taskUtil.TaskParam ?? string.Empty)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .WithDescription(taskUtil.TaskName)
                .ForJob(jobKey)
                .WithCronSchedule(taskUtil.CronExpressionString)
                .Build();

            await currentScheduler.ScheduleJob(job, trigger).ConfigureAwait(false);

            // 如果状态为停止，则暂停任务
            if (taskUtil.Status == TaskUtil.TaskStatus.STOP)
            {
                await currentScheduler.PauseJob(jobKey).ConfigureAwait(false);
            }
        }

        private static async Task DeleteJobInternalAsync(JobKey jobKey, IScheduler currentScheduler)
        {
            if (await currentScheduler.CheckExists(jobKey).ConfigureAwait(false))
            {
                bool deleted = await currentScheduler.DeleteJob(jobKey).ConfigureAwait(false);
                if (deleted)
                {
                    LogHelper.WriteLog($"任务{jobKey.Name}已经删除");
                }
            }
        }
    }
}

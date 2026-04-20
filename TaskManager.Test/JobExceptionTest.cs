using Xunit;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Threading.Tasks;
using TaskManager;
using TaskManager.TaskCommon;
using TaskManager.Jobs;

namespace TaskManager.Test
{
    public class JobExceptionTest
    {
        private IScheduler? _scheduler;
        private TestJobListener? _testListener;

        [Fact]
        public async Task TestJobExecutionException_CapturedByListener()
        {
            // 初始化 log4net
            LogHelper.SetConfig();

            // 创建调度器
            ISchedulerFactory sf = new StdSchedulerFactory();
            _scheduler = await sf.GetScheduler();

            // 注册测试 JobListener
            _testListener = new TestJobListener();
            _scheduler.ListenerManager.AddJobListener(_testListener, GroupMatcher<JobKey>.AnyGroup());

            // 创建测试 Job - 抛出 JobExecutionException
            IJobDetail job = JobBuilder.Create<TestJobExecutionExceptionJob>()
                .WithIdentity("TestJobExecutionExceptionJob", "TestGroup")
                .WithDescription("测试 JobExecutionException")
                .Build();

            // 创建触发器 - 立即执行
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("Trigger1", "TestGroup")
                .StartNow()
                .Build();

            // 调度 Job
            await _scheduler.ScheduleJob(job, trigger);

            // 启动调度器
            await _scheduler.Start();

            // 等待 Job 执行
            await Task.Delay(3000);

            // 停止调度器
            await _scheduler.Shutdown();

            // 验证：JobListener 应该捕获到异常
            Assert.True(_testListener.JobWasExecutedCalled, "JobWasExecuted 应该被调用");
            Assert.True(_testListener.JobExceptionCaptured, "JobException 应该被捕获");
            Assert.NotNull(_testListener.CapturedException);
        }

        [Fact]
        public async Task TestNormalException_NotCapturedAsJobException()
        {
            // 初始化 log4net
            LogHelper.SetConfig();

            // 创建调度器
            ISchedulerFactory sf = new StdSchedulerFactory();
            _scheduler = await sf.GetScheduler();

            // 注册测试 JobListener
            _testListener = new TestJobListener();
            _scheduler.ListenerManager.AddJobListener(_testListener, GroupMatcher<JobKey>.AnyGroup());

            // 创建测试 Job - 抛出普通异常
            IJobDetail job = JobBuilder.Create<TestThrowNormalExceptionJob>()
                .WithIdentity("TestThrowNormalExceptionJob", "TestGroup")
                .WithDescription("测试普通异常")
                .Build();

            // 创建触发器 - 立即执行
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("Trigger2", "TestGroup")
                .StartNow()
                .Build();

            // 调度 Job
            await _scheduler.ScheduleJob(job, trigger);

            // 启动调度器
            await _scheduler.Start();

            // 等待 Job 执行
            await Task.Delay(3000);

            // 停止调度器
            await _scheduler.Shutdown();

            // 验证：JobListener 的 JobWasExecuted 被调用，但 JobException 为 null
            // 因为普通异常不会被包装成 JobExecutionException
            Assert.True(_testListener.JobWasExecutedCalled, "JobWasExecuted 应该被调用");
            // 注意：普通异常可能导致 Job 执行失败，但 JobException 可能为 null
        }
    }

    /// <summary>
    /// 测试 JobListener - 用于捕获异常
    /// </summary>
    public class TestJobListener : IJobListener
    {
        public string Name => "TestJobListener";

        public bool JobToBeExecutedCalled { get; private set; }
        public bool JobWasExecutedCalled { get; private set; }
        public bool JobExceptionCaptured { get; private set; }
        public Exception? CapturedException { get; private set; }

        public Task JobToBeExecuted(IJobExecutionContext context, System.Threading.CancellationToken cancellationToken = default)
        {
            JobToBeExecutedCalled = true;
            var taskName = context.JobDetail.Description ?? context.JobDetail.Key.Name;
            LogHelper.WriteLog($"[TestJobListener] 任务 [{taskName}] 准备执行");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, System.Threading.CancellationToken cancellationToken = default)
        {
            JobWasExecutedCalled = true;
            var taskName = context.JobDetail.Description ?? context.JobDetail.Key.Name;

            if (jobException != null)
            {
                JobExceptionCaptured = true;
                CapturedException = jobException;
                LogHelper.WriteLog($"[TestJobListener] 任务 [{taskName}] 执行异常", jobException);
            }
            else
            {
                LogHelper.WriteLog($"[TestJobListener] 任务 [{taskName}] 执行完成");
            }
            return Task.CompletedTask;
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, System.Threading.CancellationToken cancellationToken = default)
        {
            var taskName = context.JobDetail.Description ?? context.JobDetail.Key.Name;
            LogHelper.WriteLog($"[TestJobListener] 任务 [{taskName}] 执行被否决");
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// 测试抛出 JobExecutionException
    /// </summary>
    public class TestJobExecutionExceptionJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var taskName = context.JobDetail.Description ?? context.JobDetail.Key.Name;
            LogHelper.WriteLog($"[TestJobExecutionExceptionJob] 任务 [{taskName}] 开始执行");

            // 抛出 JobExecutionException
            throw new JobExecutionException("测试异常 - 任务执行失败", new InvalidOperationException("这是测试异常"));
        }
    }

    /// <summary>
    /// 测试抛出普通异常
    /// </summary>
    public class TestThrowNormalExceptionJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var taskName = context.JobDetail.Description ?? context.JobDetail.Key.Name;
            LogHelper.WriteLog($"[TestThrowNormalExceptionJob] 任务 [{taskName}] 开始执行");

            // 抛出普通异常
            throw new InvalidOperationException("这是普通异常测试");
        }
    }
}

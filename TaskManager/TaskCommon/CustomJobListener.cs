using ICSharpCode.SharpDevelop;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager;


namespace TaskManager.TaskCommon
{

    /// <summary>
    /// 自定义 Job 监听器 - 用于捕获 Job 执行时的异常
    /// </summary>
    public class CustomJobListener : IJobListener
    {
        public string Name
        {
            get
            {
                return "CustomJobListener";
            }
        }

        /// <summary>
        /// Job 执行前调用
        /// </summary>
        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                string taskName = context.JobDetail.Description ?? context.JobDetail.Key.Name;
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string message = $"[{timestamp}] 任务 [{taskName}] 准备执行";
                LogHelper.WriteLog(message);
                WriteToOutputPad(message);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("JobToBeExecuted 事件处理异常", ex);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Job 执行完成时调用
        /// </summary>
        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? exception, CancellationToken cancellationToken = default)
        {
            try
            {
                string taskName = context.JobDetail.Description ?? context.JobDetail.Key.Name;
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (exception != null)
                {
                    // Job 执行异常
                    string message = $"[{timestamp}] 任务 [{taskName}] 执行异常：{exception.Message}";
                    LogHelper.WriteLog(message, exception);
                    WriteToOutputPad(message);
                    // 输出异常堆栈
                    if (exception.InnerException != null)
                    {
                        WriteToOutputPad($"  内部异常：{exception.InnerException.Message}");
                    }
                }
                else
                {
                    string message = $"[{timestamp}] 任务 [{taskName}] 执行完成";
                    LogHelper.WriteLog(message);
                    WriteToOutputPad(message);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("JobWasExecuted 事件处理异常", ex);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Job 执行被否决时调用
        /// </summary>
        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                string taskName = context.JobDetail.Description ?? context.JobDetail.Key.Name;
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string message = $"[{timestamp}] 任务 [{taskName}] 执行被否决";
                LogHelper.WriteLog(message);
                WriteToOutputPad(message);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("JobExecutionVetoed 事件处理异常", ex);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 输出到 OutputPad
        /// </summary>
        private static void WriteToOutputPad(string message)
        {
            try
            {
                SD.MainThread.InvokeIfRequired(() =>
                {
                    var output = SD.OutputPad.CurrentCategory;
                    output.AppendLine(message);
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog($"输出到 OutputPad 失败：{message}", ex);
            }
        }
    }
}

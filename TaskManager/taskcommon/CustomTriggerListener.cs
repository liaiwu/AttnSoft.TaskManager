using Quartz;
using System;
using TaskManager;


namespace TaskManager.TaskCommon
{

    /// <summary>
    /// 自定义触发器监听
    /// </summary>
    public class CustomTriggerListener : ITriggerListener
    {
        public event TriggerFiredEvent? OnTriggerFired;
        public event VetoJobExecutionEvent? OnVetoJobExecution;
        public event TriggerCompleteEvent? OnTriggerComplete;
        public event TriggerMisfiredEvent? OnTriggerMisfired;

        public string Name
        {
            get
            {
                return "All_TriggerListener";
            }
        }

        /// <summary>
        /// Job 执行时调用
        /// </summary>
        /// <param name="trigger">触发器</param>
        /// <param name="context">上下文</param>
        public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                OnTriggerFired?.Invoke(trigger, context);
            }
            catch (Exception ex)
            {
                string taskName = context?.JobDetail?.Description ?? context?.JobDetail?.Key?.Name ?? "Unknown";
                LogHelper.WriteLog($"任务 [{taskName}] TriggerFired 事件处理异常", ex);
            }
            return Task.CompletedTask;
        }


        /// <summary>
        ///  Trigger 触发后，job 执行时调用本方法。true 即否决，job 后面不执行。
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                if (OnVetoJobExecution != null)
                {
                    return Task.FromResult(OnVetoJobExecution(trigger, context));
                }
            }
            catch (Exception ex)
            {
                string taskName = context?.JobDetail?.Description ?? context?.JobDetail?.Key?.Name ?? "Unknown";
                LogHelper.WriteLog($"任务 [{taskName}] VetoJobExecution 事件处理异常", ex);
            }
            return Task.FromResult(false);
        }

        /// <summary>
        /// Job 完成时调用
        /// </summary>
        /// <param name="trigger">触发器</param>
        /// <param name="context">上下文</param>
        /// <param name="triggerInstructionCode"></param>
        public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                OnTriggerComplete?.Invoke(trigger, context, triggerInstructionCode);
            }
            catch (Exception ex)
            {
                string taskName = context?.JobDetail?.Description ?? context?.JobDetail?.Key?.Name ?? "Unknown";
                LogHelper.WriteLog($"任务 [{taskName}] TriggerComplete 事件处理异常", ex);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 错过触发时调用
        /// </summary>
        /// <param name="trigger">触发器</param>
        public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                OnTriggerMisfired?.Invoke(trigger);
            }
            catch (Exception ex)
            {
                string taskName = trigger?.JobKey?.Name ?? "Unknown";
                LogHelper.WriteLog($"任务 [{taskName}] TriggerMisfired 事件处理异常", ex);
            }
            return Task.CompletedTask;
        }

    }
    public delegate void TriggerFiredEvent(ITrigger trigger, IJobExecutionContext context);
    public delegate bool VetoJobExecutionEvent(ITrigger trigger, IJobExecutionContext context);
    public delegate void TriggerCompleteEvent(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode);
    public delegate void TriggerMisfiredEvent(ITrigger trigger);
}

using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TaskManager.TaskCommon
{

    /// <summary>
    /// 自定义触发器监听
    /// </summary>
    public class CustomTriggerListener : ITriggerListener
    {
        public event TriggerFiredEvent OnTriggerFired;
        public event VetoJobExecutionEvent OnVetoJobExecution;
        public event TriggerCompleteEvent OnTriggerComplete;
        public event TriggerMisfiredEvent OnTriggerMisfired;

        public string Name
        {
            get
            {
                return "All_TriggerListener";
            }
        }

        /// <summary>
        /// Job执行时调用
        /// </summary>
        /// <param name="trigger">触发器</param>
        /// <param name="context">上下文</param>
        public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (OnTriggerFired != null)
            {
                OnTriggerFired(trigger,context);
            }
            return Task.CompletedTask;
        }


        /// <summary>
        ///  Trigger触发后，job执行时调用本方法。true即否决，job后面不执行。
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {            
            if (OnVetoJobExecution != null)
            {
                return Task.FromResult(OnVetoJobExecution(trigger, context));
            }
            return Task.FromResult(false);
        }

        /// <summary>
        /// Job完成时调用
        /// </summary>
        /// <param name="trigger">触发器</param>
        /// <param name="context">上下文</param>
        /// <param name="triggerInstructionCode"></param>
        public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default(CancellationToken))
        {
           
            if (OnTriggerComplete != null)
            {
                OnTriggerComplete(trigger, context, triggerInstructionCode);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 错过触发时调用
        /// </summary>
        /// <param name="trigger">触发器</param>
        public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (OnTriggerMisfired != null)
            {
                OnTriggerMisfired(trigger);
            }
            return Task.CompletedTask;
        }

    }
    public delegate void TriggerFiredEvent(ITrigger trigger, IJobExecutionContext context);
    public delegate bool VetoJobExecutionEvent(ITrigger trigger, IJobExecutionContext context);
    public delegate void TriggerCompleteEvent(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode);
    public delegate void TriggerMisfiredEvent(ITrigger trigger);
}
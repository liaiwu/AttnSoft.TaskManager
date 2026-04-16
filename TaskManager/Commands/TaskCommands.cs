using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Workbench;
using TaskManager.TaskCommon;

namespace TaskManager.Commands
{
    public class LoadTasks : AbstractMenuCommand
    {
        private UC_TaskList? uC_TaskList1;

        public override void Run()
        {
            uC_TaskList1 = new UC_TaskList();

            var view = new EditFormViewContent("定时任务列表", uC_TaskList1);
            SD.Workbench.ShowView(view);

            Start();
        }

        private async void Start()
        {
            try
            {
                LogHelper.SetConfig();
                await QuartzHelper.InitScheduler();
                await QuartzHelper.StartScheduler();
            }
            catch (Exception ex)
            {
                MessageService.ShowError(ex.Message);
            }
        }
    }

    public class StopScheduler : AbstractMenuCommand
    {
        public override async void Run()
        {
            try
            {
                await QuartzHelper.StopScheduleAsync();
            }
            catch (Exception ex)
            {
                MessageService.ShowError(ex.Message);
            }
        }
    }
}

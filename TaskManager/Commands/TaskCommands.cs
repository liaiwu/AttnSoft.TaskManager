using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Workbench;
using TaskManager.TaskCommon;

namespace TaskManager.Commands
{
    public class LoadTasks : AbstractMenuCommand
    {
        UC_TaskList uC_TaskList1;
        public override void Run()
        {
            uC_TaskList1 = new UC_TaskList();
            //uC_TaskList1.LoadData();
            //MainForm.Instance.ShowControl(uC_TaskList1);

            //DbCodon dbcodon = new DbCodon();

            //dbcodon.Title = "定时任务列表";
            //dbcodon.Assemblys = "TaskManager.dll";
            //dbcodon.Class = "TaskManager.UC_TaskList";

            var view = new EditFormViewContent("定时任务列表", uC_TaskList1);
            //view.CloseButton = false;

            //GuiService.ShowView(dbcodon);
            //uC_T6BomExport1 = new UC_T6BomExport();

            ////DefaultViewContent view = new DefaultViewContent(dbcodon.Title, c);
            //DefaultViewContent view = new DefaultViewContent("BOM全阶展开", uC_T6BomExport1);
            SD.Workbench.ShowView(view);
            //WorkbenchSingleton.Workbench.ShowView(view);

            //MainForm.Instance.ShowControl(uC_T6BomExport1);

            Start();
        }
        private async void Start()
        {
            await QuartzHelper.InitScheduler();
            QuartzHelper.StartScheduler();
        }
    }
    public class StopScheduler : AbstractMenuCommand
    {
        public override void Run()
        {
            QuartzHelper.StopSchedule();
        }
    }
}

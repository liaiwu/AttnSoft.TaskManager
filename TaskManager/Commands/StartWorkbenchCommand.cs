using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using ICSharpCode.Core;

namespace TaskManager.Commands
{
    //public class StartWorkbenchCommand : AbstractCommand
    //{
    //    public override void Run()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    public class StartWorkbenchCommand : AbstractCommand
    {
        private const string workbenchMemento = "WorkbenchMemento";

        public static void BeforeShowWorkbench()
        {
            foreach (ICommand command in AddInTree.BuildItems<ICommand>("/Workspace/BeforeShowWorkbench", null, false))
            {
                command.Execute(null);
            }
        }

        public override void Run()
        {
            BeforeShowWorkbench();
            MainForm manform = MainForm.Instance;
            manform.InitializeWorkspace();
            //Form workbench = (Form)WorkbenchSingleton.Workbench;
            manform.Show();
            if (true)
            {
                foreach (ICommand command in AddInTree.BuildItems<ICommand>("/Workspace/AutostartNothingLoaded", null, false))
                {
                    command.Execute(null);
                }
            }
            manform.Focus();
            //Application.AddMessageFilter(new FormKeyHandler());
            Application.Run(manform);
            //try
            //{
            //    PropertyService.Set<Properties>("WorkbenchMemento", WorkbenchSingleton.Workbench.CreateMemento());
            //}
            //catch (Exception exception)
            //{
            //    MessageService.ShowError(exception, "Exception while saving workbench state.");
            //}
            foreach (ICommand command in AddInTree.BuildItems<ICommand>("/Workspace/AutoEnd", null, false))
            {
                command.Execute(null);
            }
        }

        //private class FormKeyHandler : IMessageFilter
        //{
        //    private const int keyPressedMessage = 0x100;
        //    private string oldLayout = "Default";

        //    private bool PadHasFocus()
        //    {
        //        foreach (PadDescriptor descriptor in WorkbenchSingleton.Workbench.PadContentCollection)
        //        {
        //            if (descriptor.HasFocus)
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }

        //    public bool PreFilterMessage(ref Message m)
        //    {
        //        if (m.Msg != 0x100)
        //        {
        //            return false;
        //        }
        //        Keys keys = ((Keys)m.WParam.ToInt32()) | Control.ModifierKeys;
        //        if (keys == Keys.Escape)
        //        {
        //            if (this.PadHasFocus() && !MenuService.IsContextMenuOpen)
        //            {
        //                this.SelectActiveWorkbenchWindow();
        //                return true;
        //            }
        //            return false;
        //        }
        //        if (keys != (Keys.Shift | Keys.Escape))
        //        {
        //            return false;
        //        }
        //        if (LayoutConfiguration.CurrentLayoutName == "Plain")
        //        {
        //            LayoutConfiguration.CurrentLayoutName = this.oldLayout;
        //        }
        //        else
        //        {
        //            WorkbenchSingleton.Workbench.WorkbenchLayout.StoreConfiguration();
        //            this.oldLayout = LayoutConfiguration.CurrentLayoutName;
        //            LayoutConfiguration.CurrentLayoutName = "Plain";
        //        }
        //        this.SelectActiveWorkbenchWindow();
        //        return true;
        //    }

        //    private void SelectActiveWorkbenchWindow()
        //    {
        //        if (((WorkbenchSingleton.Workbench.ActiveWorkbenchWindow != null) && !WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.ActiveViewContent.Control.ContainsFocus) && (Form.ActiveForm == WorkbenchSingleton.MainForm))
        //        {
        //            WorkbenchSingleton.Workbench.ActiveWorkbenchWindow.ActiveViewContent.Control.Focus();
        //        }
        //    }
        //}
    }
}

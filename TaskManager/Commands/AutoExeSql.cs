using AttnSoft.DevControls;
using ICSharpCode.Core;

namespace TaskManager.Commands
{
    public class AutoExeSql : AbstractMenuCommand
    {
        public override void Run()
        {
            DevExpress.XtraEditors.ButtonEdit? txtBoxButton = Owner as DevExpress.XtraEditors.ButtonEdit;

            if (txtBoxButton?.Text != null)
            {
                UC_EXESqlSetting ucExeSql = new UC_EXESqlSetting();
                Fm_ControlContainer fmContainer = new Fm_ControlContainer(ucExeSql);
                ucExeSql.Params = txtBoxButton.Text;

                fmContainer.Text = "执行SQL命令设置";
                if (fmContainer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    txtBoxButton.Text = ucExeSql.Params;

                }
            }
        }
    }
}

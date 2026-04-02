using AttnSoft.DevControls;
using ICSharpCode.Core;
using System;
using System.Windows.Forms;
using TaskManager.BaseObjects;
using TaskManager.Commands;


namespace TaskManager
{
    public partial class Fm_Task : Form
    {
        TaskUtil task;
        bool isNew = true;
        public Fm_Task()
        {
            InitializeComponent();
        }
        public TaskUtil Task
        {
            get { return task; }
        }
        //public string TaskParam
        //{
        //    get { return txt_TaskParam.Text; }
        //    set { txt_TaskParam.Text = value; }
        //}
        //public string Assembly
        //{
        //    get { return this.txt_Assembly.Text; }
        //    set { this.txt_Assembly.Text = value; }
        //}
        //public string Class
        //{
        //    get { return this.txt_Class.Text; }
        //    set { this.txt_Class.Text = value; }
        //}

        public void LoadData(TaskUtil task)
        {
            isNew = false;
            this.task = task;
            this.txt_taskname.Text = task.TaskName;
            this.txt_TaskParam.Text = task.TaskParam;
            this.txt_CronExpressionString.Text = task.CronExpressionString;
            this.txt_CronRemark.Text = task.CronRemark;
            this.txt_Assembly.Text = task.Assembly;
            this.txt_Class.Text = task.Class;
            this.radioButton1.Checked = task.Status == TaskUtil.TaskStatus.RUN;
            this.radioButton2.Checked = task.Status == TaskUtil.TaskStatus.STOP;

            this.txt_Remark.Text = task.Remark;
        }
        private bool Save()
        {
            try
            {
                if (this.txt_taskname.Text.Length == 0)
                {
                    MessageService.ShowWarning("请输入任务名称！");
                    return false;
                }
                if (this.txt_CronExpressionString.Text.Length == 0)
                {
                    MessageService.ShowWarning("请输入时间调度表达式！");
                    return false;
                }
                if (this.txt_Class.Text.Length == 0)
                {
                    MessageService.ShowWarning("请输入执行任务的类全名！");
                    return false;
                }
                //if (isNew == true)
                //{
                //    task = new TaskUtil();
                //}
                if (task == null)
                {
                    task = new TaskUtil();
                }
                task.TaskName = this.txt_taskname.Text;
                task.TaskParam = this.txt_TaskParam.Text;
                task.CronExpressionString = this.txt_CronExpressionString.Text;
                task.CronRemark = this.txt_CronRemark.Text;
                task.Assembly = this.txt_Assembly.Text;
                task.Class = this.txt_Class.Text;
                task.Status = this.radioButton1.Checked ? TaskUtil.TaskStatus.RUN : TaskUtil.TaskStatus.STOP;
                task.Remark = this.txt_Remark.Text;
                //if (isNew == true)
                //{
                //    task.Insert();
                //}
                //else
                //    task.SaveChanges();
                return true;

            }
            catch (Exception er)
            {
                MessageService.ShowError(er.Message);
                return false;
            }


        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
        private void txt_CronExpressionString_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Fm_QuartzCron fm = new Fm_QuartzCron();
            if (fm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.txt_CronExpressionString.Text = fm.QuartzCronString;
            }
        }

        private void txt_TaskParam_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            AutoExeSql AutoExeSql = new AutoExeSql();
            AutoExeSql.Owner = this.txt_TaskParam;
            AutoExeSql.Run();
        }
    }
}

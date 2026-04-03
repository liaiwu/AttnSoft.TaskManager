using AttnSoft.DevControls;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManager.BaseObjects;
using TaskManager.TaskCommon;

namespace TaskManager
{
    public partial class UC_TaskList : UserControl, ICloseWindow
    {
        object rootlock = new object();

        public UC_TaskList()
        {
            InitializeComponent();
            QuartzHelper.OnVetoJobExecution += QuartzHelper_OnVetoJobExecution;
            QuartzHelper.OnTriggerComplete += QuartzHelper_OnTriggerComplete;

            LoadData();
        }

        static UC_TaskList()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("zh-CN", true)
            {
                DateTimeFormat =
                {
                    DateSeparator = "-",
                    TimeSeparator = ":",
                    LongDatePattern = "yyyy-MM-dd HH:mm:ss",
                    ShortDatePattern = "yyyy-MM-dd",
                    FullDateTimePattern = "yyyy-MM-dd HH:mm:ss",
                    LongTimePattern = "HH:mm:ss"
                }
            };
        }

        void QuartzHelper_OnTriggerComplete(Quartz.ITrigger trigger, Quartz.IJobExecutionContext context, Quartz.SchedulerInstruction triggerInstructionCode)
        {
            string taskid = trigger.JobKey.Name;
            DateTime nextRuntime = GetNextRuntime(context);

            if (this.InvokeRequired)
            {
                MethodInvoker deleg = delegate { UpdateItem(taskid, 0, nextRuntime); };
                this.BeginInvoke(deleg);
            }
            else
            {
                UpdateItem(taskid, 0, nextRuntime);
            }
        }

        bool QuartzHelper_OnVetoJobExecution(Quartz.ITrigger trigger, Quartz.IJobExecutionContext context)
        {
            string taskid = trigger.JobKey.Name;
            DateTime nextRuntime = GetNextRuntime(context);

            if (this.InvokeRequired)
            {
                MethodInvoker deleg = delegate { UpdateItem(taskid, 1, nextRuntime); };
                this.BeginInvoke(deleg);
            }
            else
            {
                UpdateItem(taskid, 1, nextRuntime);
            }

            return false;
        }

        private static DateTime GetNextRuntime(Quartz.IJobExecutionContext context)
        {
            DateTimeOffset fireTime = context.NextFireTimeUtc ?? context.FireTimeUtc;
            return TimeZoneInfo.ConvertTime(fireTime, TimeZoneInfo.Local).DateTime;
        }

        public void LoadData()
        {
            lock (rootlock)
            {
                this.listView1.Items.Clear();

                foreach (TaskUtil task in TaskUtilService.Instance.GetAll())
                {
                    AddItem(task);
                }
            }
        }

        public void AddItem(TaskUtil task)
        {
            ListViewItem item = new ListViewItem(task.TaskName);
            item.SubItems.Add(task.CronRemark);
            item.SubItems.Add(task.RecentRunTime.ToString("yyyy-MM-dd hh:mm:ss"));
            item.SubItems.Add("");
            item.SubItems.Add(task.CreatedOn.ToString("yyyy-MM-dd hh:mm:ss"));
            item.SubItems.Add(task.Remark);
            item.ImageIndex = task.Status == TaskUtil.TaskStatus.RUN ? 0 : 1;
            item.StateImageIndex = 0;
            item.Tag = task;
            this.listView1.Items.Add(item);
        }

        private async void tlbtn_New_Click(object sender, EventArgs e)
        {
            try
            {
                Fm_Task fmtask = new Fm_Task();
                if (fmtask.ShowDialog() == DialogResult.OK)
                {
                    TaskUtilService.Instance.Insert(fmtask.Task);
                    lock (rootlock)
                    {
                        AddItem(fmtask.Task);
                    }

                    await QuartzHelper.ScheduleJob(fmtask.Task);
                }
            }
            catch (Exception er)
            {
                MessageService.ShowError(er.Message);
            }
        }

        private async void tlbtn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = this.listView1.SelectedItems[0];
                    TaskUtil task = item.Tag as TaskUtil;
                    if (task != null)
                    {
                        Fm_Task fmtask = new Fm_Task();
                        fmtask.LoadData(task);
                        if (fmtask.ShowDialog() == DialogResult.OK)
                        {
                            TaskUtilService.Instance.SaveChanges(fmtask.Task);
                            UpdateItem(item, task, item.StateImageIndex);
                            await QuartzHelper.ScheduleJob(task, true);
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageService.ShowError(er.Message);
            }
        }

        private void UpdateItem(string taskid, int stateIndex, DateTime nextRunTime)
        {
            ListViewItem item = FindItem(taskid);
            if (item != null)
            {
                if (stateIndex == 0)
                {
                    TaskUtilService.Instance.UpdateRecentRunTime(taskid, DateTime.Now);
                }

                TaskUtilService.Instance.UpdateLastRunTime(taskid, nextRunTime);

                TaskUtil task = TaskUtilService.Instance.GetById(taskid);
                UpdateItem(item, task, stateIndex);
            }
        }

        private void UpdateItem(ListViewItem item, TaskUtil task, int stateIndex)
        {
            lock (rootlock)
            {
                item.Text = task.TaskName;
                item.SubItems[1].Text = task.CronRemark;
                item.SubItems[2].Text = task.RecentRunTime.ToString("yyyy-MM-dd HH:mm:ss");
                item.SubItems[3].Text = task.Status == TaskUtil.TaskStatus.RUN ? task.LastRunTime.ToString("yyyy-MM-dd HH:mm:ss") : "";
                item.SubItems[4].Text = task.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss");
                item.SubItems[5].Text = task.Remark;
                item.ImageIndex = task.Status == TaskUtil.TaskStatus.RUN ? 0 : 1;
                item.StateImageIndex = stateIndex;
                item.Tag = task;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tlbtn_Edit_Click(this.listView1, e);
        }

        private ListViewItem FindItem(string taskid)
        {
            foreach (ListViewItem item in this.listView1.Items)
            {
                TaskUtil task = item.Tag as TaskUtil;
                if (task != null && task.TaskID.ToString().Equals(taskid, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }

            return null;
        }

        private async void tlbtn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = this.listView1.SelectedItems[0];
                    TaskUtil task = item.Tag as TaskUtil;
                    if (task != null && MessageService.AskQuestion("您要删除此任务吗？"))
                    {
                        TaskUtilService.Instance.Delete(task);
                        await QuartzHelper.DeleteJob(task.TaskID.ToString());
                        lock (rootlock)
                        {
                            item.Remove();
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageService.ShowError(er.Message);
            }
        }

        private async void tlbtn_Run_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = this.listView1.SelectedItems[0];
                    TaskUtil task = item.Tag as TaskUtil;
                    if (task != null)
                    {
                        task.Status = TaskUtil.TaskStatus.RUN;
                        TaskUtilService.Instance.SaveChanges(task);
                        await QuartzHelper.ScheduleJob(task, true);
                        UpdateItem(item, task, 0);
                        MessageService.ShowMessage("任务已启动！");
                    }
                }
            }
            catch (Exception er)
            {
                MessageService.ShowError(er.Message);
            }
        }

        private async void tlbtn_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = this.listView1.SelectedItems[0];
                    TaskUtil task = item.Tag as TaskUtil;
                    if (task != null)
                    {
                        task.Status = TaskUtil.TaskStatus.STOP;
                        TaskUtilService.Instance.SaveChanges(task);
                        await QuartzHelper.ScheduleJob(task, true);
                        UpdateItem(item, task, 0);
                        MessageService.ShowMessage("任务已停止！");
                    }
                }
            }
            catch (Exception er)
            {
                MessageService.ShowError(er.Message);
            }
        }

        private async void tlbtn_Reload_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                await QuartzHelper.Refresh();
            }
            catch (Exception ex)
            {
                MessageService.ShowError(ex.Message);
            }
        }

        private async void tlstripbtn_EXENow_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = this.listView1.SelectedItems[0];
                    TaskUtil task = item.Tag as TaskUtil;
                    if (task != null)
                    {
                        using (WaitDialog wtdialog = new WaitDialog())
                        {
                            await QuartzHelper.ExeTask(task);
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageService.ShowError(er.Message);
            }
        }

        public async void Close()
        {
            QuartzHelper.OnVetoJobExecution -= QuartzHelper_OnVetoJobExecution;
            QuartzHelper.OnTriggerComplete -= QuartzHelper_OnTriggerComplete;
            try
            {
                await QuartzHelper.StopScheduleAsync(false);
            }
            catch (Exception ex)
            {
                MessageService.ShowError(ex.Message);
            }
        }
    }
}

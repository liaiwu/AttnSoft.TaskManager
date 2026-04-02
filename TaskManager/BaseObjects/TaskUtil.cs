using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using AttnSoft.SqlData;

namespace TaskManager.BaseObjects
{

    public class TaskUtil : Entity
    {

        public TaskUtil()
        {
            _TableName = "ZZ_Task";
            _ColumnId = "TaskID";
            TaskID = Guid.NewGuid();
            this["CreatedOn"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 任务状态枚举
        /// </summary>
        public enum TaskStatus
        {
            /// <summary>
            /// 运行状态
            /// </summary>
            RUN = 0,

            /// <summary>
            /// 停止状态
            /// </summary>
            STOP = 1
        }
        #region 属性

        public Guid TaskID
        {
            get
            {
                return base.GetProperty("TaskID", Guid.NewGuid());
            }
            set
            {
                base.SetValue("TaskID", value);
            }
        }
        public string TaskName
        {
            get
            {
                return base.GetProperty("TaskName", "");
            }
            set
            {
                base.SetValue("TaskName", value);
            }
        }
        public string TaskParam
        {
            get
            {
                return base.GetProperty("TaskParam", "");
            }
            set
            {
                base.SetValue("TaskParam", value);
            }
        }
        public string CronExpressionString
        {
            get
            {
                return base.GetProperty("CronExpressionString", "");
            }
            set
            {
                base.SetValue("CronExpressionString", value);
            }
        }
        public string Assembly
        {
            get
            {
                return base.GetProperty("Assembly", "");
            }
            set
            {
                base.SetValue("Assembly", value);
            }
        }
        public string Class
        {
            get
            {
                return base.GetProperty("Class", "");
            }
            set
            {
                base.SetValue("Class", value);
            }
        }
        public TaskStatus Status
        {
            get
            {
                return (TaskStatus)base.GetProperty("Status", (TaskStatus)TaskStatus.STOP);
            }
            set
            {
                base.SetValue("Status", value);
            }
        }
        public DateTime CreatedOn
        {
            get
            {
                return base.GetProperty("CreatedOn", System.DateTime.MinValue);
            }
            set
            {
                base.SetValue("CreatedOn", value);
            }
        }
        public DateTime ModifyOn
        {
            get
            {
                return base.GetProperty("ModifyOn", System.DateTime.MinValue);
            }
            set
            {
                base.SetValue("ModifyOn", value);
            }
        }
        public DateTime RecentRunTime
        {
            get
            {
                return base.GetProperty("RecentRunTime", System.DateTime.MinValue);
            }
            set
            {
                base.SetValue("RecentRunTime", value);
            }
        }
        public DateTime LastRunTime
        {
            get
            {
                return base.GetProperty("LastRunTime", System.DateTime.MinValue);
            }
            set
            {
                base.SetValue("LastRunTime", value);
            }
        }
        public string CronRemark
        {
            get
            {
                return base.GetProperty("CronRemark", "");
            }
            set
            {
                base.SetValue("CronRemark", value);
            }
        }
        public string Remark
        {
            get
            {
                return base.GetProperty("Remark", "");
            }
            set
            {
                base.SetValue("Remark", value);
            }
        }
        #endregion
    }

}

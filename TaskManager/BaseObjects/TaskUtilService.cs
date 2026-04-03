using AttnSoft.SqlData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskManager.BaseObjects
{
    public class TaskUtilService:EntityService<TaskUtil>
    {
        public override IDBServer DbServer
        {
            get
            {
                if (_DbServer == null)
                {
                    _DbServer = ServerFactory.GetServer(ServerType.Sqlite);
                }
                return _DbServer;
            }
            set
            {
                _DbServer = value;
            }
        }
        public static TaskUtilService Instance=new TaskUtilService();

        //public static List<TaskUtil> GetAll()
        //{
        //    string sql = "select * from ZZ_Task order by TaskName";
        //    return GetEntityList<TaskUtil>(sql);
        //}

        /// <summary>
        /// 更新任务下次运行时间
        /// </summary>
        /// <param name="TaskID">任务id</param>
        /// <param name="LastRunTime">下次运行时间</param>
        public void UpdateLastRunTime(string TaskID, DateTime LastRunTime)
        {
            TaskUtil task = new TaskUtil();
            task["TaskID"] = TaskID;
            task["LastRunTime"] = LastRunTime.ToString("yyyy-MM-dd HH:mm:ss");

            DbServer.ExecuteSQL("UPDATE ZZ_Task SET LastRunTime=@LastRunTime WHERE TaskID=@TaskID", task);
        }
        /// <summary>
        /// 更新任务最近运行时间
        /// </summary>
        /// <param name="TaskID">任务id</param>
        public void UpdateRecentRunTime(string TaskID, DateTime LastRunTime)
        {
            TaskUtil task = new TaskUtil();
            task["TaskID"] = TaskID;
            task["LastRunTime"] = LastRunTime;
            task["RecentRunTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            DbServer.ExecuteSQL("UPDATE ZZ_Task SET RecentRunTime=@RecentRunTime,LastRunTime=@LastRunTime WHERE TaskID=@TaskID", task);
        }

        //public override DbParams Insert()
        //{
        //    DbParams dbp = new DbParams();
        //    IDbCommand insertCommand = DbServer.GetInsertDbCommand("ZZ_Task");
        //    this["CreatedOn"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        //    dbp.ResultCount = DbServer.ExecuteSQL(insertCommand, this);
        //    return dbp;
        //}
        //public override void SaveChanges()
        //{
        //    IDbCommand updateCommand = DbServer.GetUpdateDbCommand("ZZ_Task");
        //    DbServer.ExecuteSQL(updateCommand, this);
        //    //base.SaveChanges();
        //}
        //public override int Delete()
        //{
        //    return DbServer.ExecuteSQL("Delete FROM  ZZ_Task Where TaskID=@TaskID", this);
        //}
    }
}

using AttnSoft.SqlData;
using Quartz;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManager;

namespace TaskManager.Jobs
{
    public class AutoExeSql : IJob
    {
        public static LogHelper Loger = new LogHelper("AutoExeSql", "info");
        public static LogHelper LogerError = new LogHelper("AutoExeSql", "error");

        #region IJob 成员

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var taskName = context.JobDetail.Description ?? context.JobDetail.Key.Name;
                Loger.WriteLogE($"任务 [{taskName}] 开始执行");

                object objParam = context.JobDetail.JobDataMap.Get("TaskParam");
                if (objParam != null)
                {
                    string constr = string.Empty;
                    string sql = string.Empty;

                    XElement root = XElement.Parse(objParam.ToString());
                    XAttribute attrbute = root.Attribute("Connection");
                    if (attrbute != null)
                    {
                        constr = attrbute.Value;
                    }
                    attrbute = root.Attribute("SQL");
                    if (attrbute != null)
                    {
                        sql = attrbute.Value;
                    }
                    if (!string.IsNullOrWhiteSpace(constr) && !string.IsNullOrWhiteSpace(sql))
                    {
                        Loger.WriteLogE($"开始执行 SQL:{sql}");
                        using (IDBServer server = ServerFactory.GetServer(ConnectionType.MasterDB))
                        {
                            server.ConnectionString = constr;
                            server.Open();
                            server.ExecuteSQL(sql, null);
                            server.Close();
                        }
                        Loger.WriteLogE($"SQL 执行成功");
                    }
                    else
                    {
                        LogerError.WriteLogE($"SQL 参数无效：constr='{constr}', sql='{sql}'");
                    }
                }
                else
                {
                    LogerError.WriteLogE("TaskParam 为空");
                }
            }
            catch (Exception ex)
            {
                LogerError.WriteLogE($"任务执行异常：", ex);
                // 重新抛出异常，让 Quartz 的 JobListener 也能捕获
                throw;
            }
        }

        #endregion
    }
}

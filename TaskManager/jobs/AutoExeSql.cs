using AttnSoft.SqlData;
using Quartz;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskManager.Jobs
{
    public class AutoExeSql : IJob
    {
        public static LogHelper Loger = new LogHelper("AutoExeSql", "info");
        public static LogHelper LogerError = new LogHelper("AutoExeSql", "error");

        #region IJob 成员

        Task IJob.Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                try
                {
                    object objParam = context.JobDetail.JobDataMap.Get("TaskParam");
                    if (objParam != null)
                    {

                        string constr, sql;
                        constr = sql = string.Empty;

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
                            Loger.WriteLogE("开始执行SQL:" + sql);
                            using (IDBServer server = ServerFactory.GetServer( ConnectionType.MasterDB))
                            {
                                server.ConnectionString = constr;
                                server.Open();
                                server.ExecuteSQL(sql, null);
                                server.Close();
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    LogerError.WriteLogE("任务执行异常：", ex);
                }
            });
        }

        #endregion
    }
}

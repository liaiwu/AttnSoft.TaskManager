using Quartz;
using System;
using System.IO;
using System.Threading.Tasks;
using TaskManager;

namespace TaskManager.Jobs
{
    public class TestJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var taskName = context.JobDetail.Description ?? context.JobDetail.Key.Name;
                LogHelper.WriteLog($"任务 [{taskName}] 开始执行");

                string dateString = DateTime.Now.ToString();
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.txt");
                File.WriteAllText(filePath, dateString);

                LogHelper.WriteLog($"任务执行成功，文件：{filePath}, 内容：{dateString}");

                // 测试：抛出 JobExecutionException
                //throw new JobExecutionException("测试异常 - 任务执行失败", new InvalidOperationException("这是测试异常"));
            }
            catch (JobExecutionException)
            {
                // 重新抛出 JobExecutionException
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog($"任务执行异常：", ex);
                throw;
            }
        }
    }
}

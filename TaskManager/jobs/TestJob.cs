using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Jobs
{
    public class TestJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                string dateString = DateTime.Now.ToString();
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.txt"), dateString);
            });
        }
    }
}

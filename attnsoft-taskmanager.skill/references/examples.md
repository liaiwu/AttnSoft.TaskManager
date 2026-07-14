# AttnSoft.TaskManager 示例

## 目录

- [示例 1：创建简单 Job](#示例 1创建简单 job)
- [示例 2：带参数的 Job](#示例 2带参数的 job)
- [示例 3：数据库清理任务](#示例 3数据库清理任务)
- [示例 4：TestJob](#示例 4testjob)

---

## 示例 1：创建简单 Job

```csharp
using Quartz;

namespace MyApp.Jobs
{
    public class SimpleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"任务执行时间：{DateTime.Now}");
            // 执行你的业务逻辑
        }
    }
}

// 注册任务
var task = new TaskUtil
{
    TaskName = "简单任务",
    Assembly = "MyApp",
    Class = "MyApp.Jobs.SimpleJob",
    CronExpressionString = "0 0/5 * * * ?", // 每 5 分钟
    Status = TaskUtil.TaskStatus.RUN
};

TaskUtilService.Instance.Insert(task);
await QuartzHelper.ScheduleJob(task);
```

---

## 示例 2：带参数的 Job

```csharp
using Quartz;
using System.Text.Json;

public class DataSyncJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        // 获取任务参数
        var param = context.JobDetail.JobDataMap.GetString("TaskParam");
        
        // 解析 JSON 参数
        var config = JsonSerializer.Deserialize<SyncConfig>(param);
        
        await SyncDataAsync(config.SourceDb, config.TargetDb);
    }
    
    private async Task SyncDataAsync(string source, string target)
    {
        // 实现数据同步逻辑
    }
}

public class SyncConfig
{
    public string SourceDb { get; set; }
    public string TargetDb { get; set; }
}

// 注册任务（带 JSON 参数）
var task = new TaskUtil
{
    TaskName = "数据同步",
    Assembly = "MyApp",
    Class = "MyApp.Jobs.DataSyncJob",
    CronExpressionString = "0 0 */2 * * ?", // 每 2 小时
    TaskParam = @"{""SourceDb"":""DB_A"",""TargetDb"":""DB_B""}",
    Status = TaskUtil.TaskStatus.RUN
};

TaskUtilService.Instance.Insert(task);
await QuartzHelper.ScheduleJob(task);
```

---

## 示例 3：数据库清理任务

```csharp
using Quartz;
using AttnSoft.SqlData;

public class CleanupOldDataJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var server = ServerFactory.GetServer();
        
        // 删除 30 天前的日志
        await server.ExecuteSQLAsync(
            "DELETE FROM LogTable WHERE CreateTime < DATEADD(day, -30, GETDATE())", 
            null);
    }
}

// 注册任务
var task = new TaskUtil
{
    TaskName = "清理历史日志",
    Assembly = "MyApp",
    Class = "MyApp.Jobs.CleanupOldDataJob",
    CronExpressionString = "0 0 2 * * ?", // 每天凌晨 2 点
    Status = TaskUtil.TaskStatus.RUN
};

TaskUtilService.Instance.Insert(task);
await QuartzHelper.ScheduleJob(task);
```

---

## 示例 4：TestJob

```csharp
using Quartz;
using System.IO;

namespace TaskManager.Jobs
{
    public class TestJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // 创建 test.txt 文件，写入当前时间
            var testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.txt");
            await File.WriteAllTextAsync(testFile, DateTime.Now.ToString("yyyy/M/d HH:mm:ss"));
        }
    }
}
```

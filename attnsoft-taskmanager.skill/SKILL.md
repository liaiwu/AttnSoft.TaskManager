---
name: attnsoft-taskmanager
description: 使用 AttnSoft.TaskManager 进行定时任务管理。基于 Quartz.NET 3.16.1 实现，支持任务的添加、暂停、恢复、删除、立即执行，支持 Cron 表达式调度。使用场景：创建定时 Job、配置任务调度、管理任务生命周期、Cron 表达式配置、Quartz 事件监听
---

# AttnSoft.TaskManager 使用指南

基于 Quartz.NET 的 .NET Framework 定时任务管理库。

## 快速开始

### 初始化调度器

```csharp
using TaskManager.TaskCommon;
using TaskManager;

// 初始化 log4net（可选）
LogHelper.SetConfig();

// 初始化并启动调度器
await QuartzHelper.InitScheduler();
await QuartzHelper.StartScheduler(); // 自动加载数据库中 RUN 状态的任务
```

### 创建并添加任务

```csharp
using TaskManager.BaseObjects;

var task = new TaskUtil
{
    TaskName = "我的定时任务",
    Assembly = "MyApp",           // 程序集名（不含.dll）
    Class = "MyApp.Jobs.MyJob",   // Job 类全限定名
    CronExpressionString = "0 0/5 * * * ?", // 每 5 分钟
    TaskParam = "参数内容",
    Status = TaskUtil.TaskStatus.RUN
};

TaskUtilService.Instance.Insert(task);  // 保存到数据库
await QuartzHelper.ScheduleJob(task);   // 添加到调度器
```

### 创建自定义 Job

```csharp
using Quartz;

public class MyJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var taskParam = context.JobDetail.JobDataMap.GetString("TaskParam");
        // 执行任务逻辑
    }
}
```

## 核心 API

### 调度器控制

```csharp
await QuartzHelper.InitScheduler();        // 初始化
await QuartzHelper.StartScheduler();       // 启动
await QuartzHelper.Refresh();              // 刷新（重新加载）
await QuartzHelper.StopScheduleAsync(true); // 停止
```

### 任务操作

```csharp
await QuartzHelper.ScheduleJob(task);      // 添加/更新任务
await QuartzHelper.PauseJob(taskId);       // 暂停
await QuartzHelper.ResumeJob(taskId);      // 恢复
await QuartzHelper.DeleteJob(taskId);      // 删除
await QuartzHelper.ExeTask(task);          // 立即执行
```

## 参考文档

- [API 参考](references/api-reference.md) - 完整的 API 文档
- [Cron 表达式](references/cron-expressions.md) - Cron 格式和常用表达式
- [示例代码](references/examples.md) - 常见使用场景示例
-[Quartz 本地源码] (E:\APP\AttnSoft\OpenSource\quartznet)
## 注意事项

1. **TaskID 类型**: 是 `Guid`，不是 int
2. **Job 要求**: 实现 `IJob` 接口，有公共无参构造函数
3. **线程安全**: QuartzHelper 已实现线程安全
4. **异常处理**: `CustomJobListener` 自动捕获并记录 Job 异常
5. **日志输出**: `./Logs/app.log`

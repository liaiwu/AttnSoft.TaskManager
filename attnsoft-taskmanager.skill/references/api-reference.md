# AttnSoft.TaskManager API 参考

## 目录

- [QuartzHelper API](#quartzhelper-api)
- [TaskUtilService API](#taskutilservice-api)
- [TaskUtil 实体](#taskutil-实体)
- [事件监听](#事件监听)
- [LogHelper API](#loghelper-api)

---

## QuartzHelper API

命名空间：`TaskManager.TaskCommon`

### 调度器控制

| 方法 | 说明 |
|------|------|
| `InitScheduler()` | 初始化调度器 |
| `StartScheduler()` | 启动调度器（自动加载 RUN 状态任务） |
| `Refresh()` | 清空并重新加载所有 RUN 状态任务 |
| `StopScheduleAsync(bool waitForJobComplete)` | 停止调度器 |

```csharp
// 初始化调度器
await QuartzHelper.InitScheduler();

// 启动调度器
await QuartzHelper.StartScheduler();

// 刷新任务
await QuartzHelper.Refresh();

// 停止调度器
await QuartzHelper.StopScheduleAsync(waitForJobComplete: true);
```

### 任务操作

| 方法 | 说明 |
|------|------|
| `ScheduleJob(TaskUtil task)` | 添加/更新任务 |
| `PauseJob(string taskID)` | 暂停任务 |
| `ResumeJob(string taskID)` | 恢复任务 |
| `DeleteJob(string taskID)` | 删除任务 |
| `ExeTask(TaskUtil task)` | 立即执行任务 |
| `ValidExpression(string cron)` | 验证 Cron 表达式 |

```csharp
// 添加任务
await QuartzHelper.ScheduleJob(task);

// 暂停/恢复任务（使用 TaskID 字符串）
await QuartzHelper.PauseJob("guid-task-id");
await QuartzHelper.ResumeJob("guid-task-id");

// 删除任务
await QuartzHelper.DeleteJob("guid-task-id");

// 立即执行
await QuartzHelper.ExeTask(task);

// 验证 Cron
bool isValid = QuartzHelper.ValidExpression("0 0/5 * * * ?");
```

---

## TaskUtilService API

命名空间：`TaskManager.BaseObjects`

| 方法 | 说明 |
|------|------|
| `Instance.GetAll()` | 获取所有任务 |
| `Instance.GetById(Guid id)` | 根据 ID 获取任务 |
| `Instance.Insert(TaskUtil task)` | 插入新任务 |
| `Instance.SaveChanges(TaskUtil task)` | 更新任务 |
| `Instance.Delete(TaskUtil task)` | 删除任务 |
| `Instance.UpdateRecentRunTime(string taskId, DateTime time)` | 更新下次运行时间 |
| `Instance.UpdateLastRunTime(string taskId, DateTime time)` | 更新上次运行时间 |

```csharp
// 获取所有任务
List<TaskUtil> allTasks = TaskUtilService.Instance.GetAll();

// 根据 ID 获取
TaskUtil task = TaskUtilService.Instance.GetById(guid);

// CRUD 操作
TaskUtilService.Instance.Insert(task);
TaskUtilService.Instance.SaveChanges(task);
TaskUtilService.Instance.Delete(task);
```

---

## TaskUtil 实体

命名空间：`TaskManager.BaseObjects`

| 属性 | 类型 | 说明 |
|------|------|------|
| `TaskID` | Guid | 主键，自动生成 |
| `TaskName` | string | 任务名称 |
| `Assembly` | string | 程序集名（不含.dll） |
| `Class` | string | Job 类全限定名 |
| `CronExpressionString` | string | Cron 表达式 |
| `TaskParam` | string | 任务参数（建议 JSON） |
| `Status` | TaskStatus | RUN=0 / STOP=1 |
| `CronRemark` | string | Cron 说明 |
| `CreatedOn` | DateTime | 创建时间 |
| `ModifyOn` | DateTime | 修改时间 |
| `RecentRunTime` | DateTime | 下次运行时间 |
| `LastRunTime` | DateTime | 上次运行时间 |
| `Remark` | string | 备注 |

```csharp
public enum TaskStatus
{
    RUN = 0,   // 运行中
    STOP = 1   // 已停止
}
```

---

## 事件监听

### Trigger 事件（CustomTriggerListener）

```csharp
// 任务执行前
QuartzHelper.OnTriggerFired += (trigger, context) => { };

// 任务拦截（返回 true 阻止执行）
QuartzHelper.OnVetoJobExecution += (trigger, context) => { return false; };

// 任务完成
QuartzHelper.OnTriggerComplete += (trigger, context, instruction) => { };

// 错过触发
QuartzHelper.OnTriggerMisfired += (trigger) => { };
```

### Job 事件（CustomJobListener - 自动注册）

`CustomJobListener` 自动注册，会：
- Job 执行前记录日志
- Job 执行后记录日志
- 捕获 `JobExecutionException` 并输出到日志和 OutputPad

---

## LogHelper API

命名空间：`TaskManager`

```csharp
// 初始化 log4net（代码配置）
LogHelper.SetConfig();

// 记录普通日志
LogHelper.WriteLog("信息");

// 记录错误日志
LogHelper.WriteLog("错误信息", exception);
```

日志输出到：`./Logs/app.log`

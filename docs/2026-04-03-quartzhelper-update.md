# QuartzHelper 修改说明

## 1. 背景

本次修改目标是基于本地最新 Quartz.NET 源码：

- `E:\APP\AttnSoft\OpenSource\quartznet`

对项目中的旧实现进行校对和优化：

- `E:\APP\AttnSoft\AttnSoft.TaskManager\TaskManager\TaskCommon\QuartzHelper.cs`

项目当前实际引用的 Quartz 版本为：

- `Quartz 3.16.1`

因此本次处理不是“兼容旧版”的修补，而是按 Quartz 3.x 的实际 API 和线程模型，对旧代码进行收敛。

---

## 2. 涉及文件

本次实际修改文件：

- `TaskManager/TaskCommon/QuartzHelper.cs`
- `TaskManager/Commands/TaskCommands.cs`
- `TaskManager/UC_TaskList.cs`

新增文档：

- `docs/2026-04-03-quartzhelper-update.md`

---

## 3. 原实现主要问题

### 3.1 Quartz API 写法偏旧

原实现仍使用以下旧式写法：

- `JobDetailImpl`
- `CronTriggerImpl`
- 直接调用部分异步 API 但不等待结果

虽然在 Quartz 3.x 下未必立即失效，但不属于当前推荐用法，可维护性较差。

### 3.2 async void 使用过多

原 `QuartzHelper` 中多个核心调度方法为 `async void`：

- `StartScheduler`
- `DeleteJob`
- `PauseJob`
- `ResumeJob`

问题：

- 调用方无法等待完成
- 异常容易丢失
- 时序不可控

### 3.3 调度器初始化与关闭存在并发风险

旧实现对静态字段 `scheduler` 的访问没有统一同步策略，存在以下风险：

- 初始化与关闭并发交错
- 读写共享字段时出现不一致视图
- `scheduler != null` 与后续继续访问之间存在竞态

### 3.4 监听器注册状态缺少内存可见性保证

`triggerListenerRegistered` 原先只是普通布尔字段读写，在并发下可能出现：

- 误判已注册，导致跳过注册
- 误判未注册，导致多余竞争

### 3.5 UI 回调存在空值风险

`UC_TaskList` 监听器中直接访问：

- `context.NextFireTimeUtc.Value`

当任务没有下一次触发时间时，存在空引用风险。

### 3.6 停止调度存在同步阻塞问题

旧实现中存在同步包装异步停止的模式：

- `GetAwaiter().GetResult()`

问题：

- 阻塞调用线程
- 在 `waitForJobComplete = true` 场景下存在互等风险

---

## 4. 本次修改内容

### 4.1 QuartzHelper 改为真正异步模型

已将关键方法统一为 `Task` 返回：

- `InitScheduler`
- `StartScheduler`
- `Refresh`
- `DeleteJob`
- `ScheduleJob`
- `PauseJob`
- `ResumeJob`
- `ExeTask`
- `StopScheduleAsync`

效果：

- 调用链可等待
- 异常可向上抛出
- 调度行为更可预测

### 4.2 Job / Trigger 创建改为 Quartz 3.x 推荐方式

已由旧式实现类切换为 Builder 风格：

- `JobBuilder.Create(...)`
- `TriggerBuilder.Create(...)`
- `WithCronSchedule(...)`

效果：

- 更符合 Quartz 3.x 习惯用法
- 可读性更好
- 后续升级更平滑

### 4.3 调度器初始化与关闭增加统一同步

引入：

- `SemaphoreSlim SchedulerLock`

用于收口以下关键路径：

- 调度器初始化
- 监听器注册
- 调度器关闭

效果：

- 避免初始化与关闭互相覆盖
- 降低调度器状态竞争风险

### 4.4 scheduler 访问补充并发可见性处理

新增辅助方法：

- `GetSchedulerSnapshot()`
- `IsSchedulerAvailable(...)`

并使用：

- `Volatile.Read(ref scheduler)`
- `Volatile.Write(ref scheduler, ...)`

效果：

- 避免对静态字段做多次无保护读取
- 降低空引用和状态不一致风险

### 4.5 监听器注册状态补充内存可见性处理

对 `triggerListenerRegistered` 已改为：

- 读取使用 `Volatile.Read`
- 写入使用 `Volatile.Write`

效果：

- 避免并发下错误跳过注册
- 保证状态变更能被其他线程及时观察到

### 4.6 删除同步包装停止方法

已移除同步包装的 `StopSchedule(...)`，统一改为：

- `await QuartzHelper.StopScheduleAsync(...)`

调用点已同步调整：

- `TaskManager/Commands/TaskCommands.cs`
- `TaskManager/UC_TaskList.cs`

效果：

- 不再同步阻塞 UI 线程
- 降低关停阶段互等风险

### 4.7 调整 UI 调用链为 await

`UC_TaskList` 中新增、编辑、删除、启动、停止、刷新、立即执行等入口均已改为等待调度操作完成后再继续。

效果：

- 行为顺序更稳定
- 错误更容易反馈到界面

### 4.8 修复 NextFireTimeUtc 空值风险

新增统一时间获取逻辑，优先使用：

- `context.NextFireTimeUtc`

为空时回退：

- `context.FireTimeUtc`

效果：

- 避免最后一次触发或无下一次触发时回调异常

---

## 5. 已处理的 review 点

本次已明确处理的 review / 风险点包括：

- `StopSchedule` 同步包装导致阻塞问题
- `EnsureSchedulerAsync` 中对 `scheduler` 的竞态访问
- `triggerListenerRegistered` 读写缺少 `Volatile.Read/Write`
- `Refresh` 旧版未正确等待异步调用

以下 review 经分析后认为不是必须修改的问题：

- `GetClassInfo` 中 `assembly.GetType(className, true, true)` 会返回 `null`

说明：

- 在 `throwOnError: true` 条件下，找不到类型时通常会直接抛异常，不属于当前主要行为缺陷。

---

## 6. 当前结果

当前 QuartzHelper 已从“旧式、半同步半异步实现”调整为：

- 基于 Quartz 3.x 的 Builder 用法
- 可等待的异步调度流程
- 带基础并发保护的静态调度器管理

从代码结构和线程安全性上，明显优于修改前版本。

---

## 7. 验证情况

已执行尝试：

```powershell
dotnet build TaskManager\TaskManager.csproj -c Debug --no-restore
```

预期会找到什么：

- 验证本次 Quartz 相关修改是否引入新的编译错误

实际结果：

- 构建未能完成
- 被项目当前已有的资源配置问题阻塞，而不是 Quartz 修改直接导致

当前已知阻塞错误：

- `MSB3822`
- `MSB3823`

问题指向：

- `TaskManager/TaskManager.csproj`

问题含义：

- 项目中存在非字符串资源，但当前构建配置缺少 `System.Resources.Extensions` 或对应资源预序列化配置

因此当前结论是：

- 本次 Quartz 修改已完成
- 调用链已静态对齐
- 但由于项目现有资源配置问题，尚未完成完整编译验证

---

## 8. 后续建议

建议下一步单独处理以下事项：

1. 修复 `TaskManager.csproj` 的资源构建配置问题
2. 在资源问题修复后重新执行完整编译验证
3. 如有需要，再补一轮 Quartz 相关运行时联调验证

---

## 9. 总结

本次修改不是简单语法替换，而是对 QuartzHelper 做了以下层面的系统性收敛：

- API 写法从旧实现类切换到 Quartz 3.x Builder 风格
- 异步流程从 `async void` 改为可等待 `Task`
- 静态调度器访问补充并发安全处理
- UI 调用链改为正确等待调度结果
- 关闭流程改为纯异步，去除阻塞式包装

整体上，这次修改已经把 `QuartzHelper` 从“历史遗留可用代码”提升到了“符合当前项目 Quartz 版本、行为更可控的实现”。

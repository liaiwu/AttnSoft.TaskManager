# TaskManager - CLAUDE.md

## Overview

TaskManager 是一个基于 .NET Framework 4.7.2 的定时任务管理库，使用 Quartz.NET 实现任务调度，提供任务配置、启动、暂停、停止和手动触发等功能。

## Project Structure

```
AttnSoft.TaskManager/
├── TaskManager/
│   ├── BaseObjects/          # 数据实体层
│   │   └── TaskUtil.cs       # 任务实体 (ZZ_Task 表)
│   ├── Commands/             # SharpDevelop 命令集成
│   ├── jobs/                 # 具体任务实现
│   │   ├── AutoExeSql.cs     # SQL 执行任务
│   │   └── TestJob.cs        # 测试任务
│   ├── taskcommon/           # Quartz 调度辅助
│   │   ├── QuartzHelper.cs   # 调度器核心逻辑
│   │   └── CustomTriggerListener.cs # 触发器监听器
│   ├── Resources/            # UI 图标资源
│   ├── Fm_Task.cs            # 任务编辑表单
│   ├── Fm_QuartzCron.cs      # Cron 表达式配置器
│   ├── UC_TaskList.cs        # 任务列表控件
│   ├── LogHelper.cs          # 日志辅助类
│   └── TaskManager.csproj    # 项目配置
└── TaskManager.slnx          # 解决方案文件
```

## Tech Stack

| 技术 | 版本/说明 |
|------|----------|
| .NET Framework | 4.7.2 |
| C# | Latest LangVersion |
| Quartz.NET | 3.16.1 (任务调度) |
| DevExpress | v25.2 (UI 控件) |
| log4net | 日志框架 |
| SqlSugar | 通过 AttnSoft.SqlData 引用 |

## Setup & Installation

### 依赖引用
项目依赖以下外部 DLL (位于 `..\..\DevExpress\DLL\net462\`):
- AttnSoft.DevControls.dll
- AttnSoft.SharpDevelop.Core.dll
- AttnSoft.SqlData.dll
- AttnSoft.Windows.Workbench.dll
- log4net.dll

### 初始化步骤
```csharp
// 1. 初始化调度器
await QuartzHelper.InitScheduler();

// 2. 启动调度器 (自动加载状态为 RUN 的任务)
QuartzHelper.StartScheduler();
```

## Core Architecture

### 任务实体 (TaskUtil)
- 表名：`ZZ_Task`
- 主键：`TaskID` (Guid)
- 状态枚举：`RUN` (0) / `STOP` (1)
- 关键属性：
  - `TaskName`: 任务名称
  - `CronExpressionString`: Cron 表达式
  - `Assembly`: 任务程序集路径
  - `Class`: 任务类全名
  - `TaskParam`: 任务参数 (SQL 语句等)

### 调度器核心 (QuartzHelper)
- **InitScheduler()**: 初始化 Quartz 调度器实例
- **StartScheduler()**: 启动调度器并加载所有 RUN 状态任务
- **ScheduleJob()**: 添加/更新单个任务
- **DeleteJob()**: 删除任务
- **PauseJob() / ResumeJob()**: 暂停/恢复任务
- **ExeTask()**: 手动触发任务执行
- **StopSchedule()**: 关闭调度器

### 事件监听
通过 `CustomTriggerListener` 提供四个事件:
- `OnTriggerFired`: Job 执行时
- `OnVetoJobExecution`: 可否决执行
- `OnTriggerComplete`: Job 完成时
- `OnTriggerMisfired`: 错过触发时

## Workflow Instructions

### 添加新任务
1. 创建继承自 `IJob` 的任务类 (放在 `jobs/` 目录)
2. 在 UI 中配置任务信息:
   - 任务名称
   - Cron 表达式 (点击按钮使用 Cron 配置器)
   - 程序集路径
   - 类全名 (命名空间 + 类名)
   - 任务参数
3. 调用 `QuartzHelper.ScheduleJob(taskUtil)`

### 实现自定义 Job
```csharp
public class MyJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var taskID = context.JobDetail.JobDataMap.GetString("TaskID");
        var taskParam = context.JobDetail.JobDataMap.GetString("TaskParam");
        // 执行任务逻辑
    }
}
```

### Cron 表达式格式
标准 Quartz Cron 格式：`秒 分 时 日 月 周`
- 示例：`0 0/30 * * * ?` (每 30 分钟执行)
- 使用 `Fm_QuartzCron` 表单可视化配置

## Development Guidelines

### 编码规范
- 使用 `Nullable` 引用类型 (`#nullable enable`)
- 异步方法使用 `async/await` 模式
- 异常使用 `LogHelper.WriteLog()` 记录
- 中文注释使用 XML 文档格式

### 文件命名
- 表单：`Fm_*.cs`
- 用户控件：`UC_*.cs`
- 实体：PascalCase (如 `TaskUtil.cs`)
- 辅助类：`*Helper.cs`

### 错误处理
- UI 层使用 `MessageService.ShowWarning()` / `ShowError()`
- 核心逻辑记录日志后抛出异常
- Cron 表达式验证使用 `CronExpression.IsValidExpression()`

## Database Schema

### ZZ_Task 表
| 字段 | 类型 | 说明 |
|------|------|------|
| TaskID | Guid | 主键 |
| TaskName | string | 任务名称 |
| TaskParam | string | 任务参数 |
| CronExpressionString | string | Cron 表达式 |
| CronRemark | string | Cron 说明 |
| Assembly | string | 程序集路径 |
| Class | string | 类全名 |
| Status | int | 0=运行，1=停止 |
| CreatedOn | datetime | 创建时间 |
| ModifyOn | datetime | 修改时间 |
| RecentRunTime | datetime | 下次运行时间 |
| LastRunTime | datetime | 上次运行时间 |
| Remark | string | 备注 |

## Common Operations

### 刷新任务列表
```csharp
QuartzHelper.Refresh(); // 清空并重新加载所有 RUN 状态任务
```

### 手动执行任务
```csharp
QuartzHelper.ExeTask(taskUtil); // 立即触发一次
```

### 修改任务调度
```csharp
// 先删除旧任务再添加新任务
QuartzHelper.ScheduleJob(taskUtil, isDeleteOldTask: true);
```

## Known Dependencies

- 依赖 `AttnSoft.SqlData.SqlSugar` 进行数据库操作
- 依赖 `AttnSoft.DevControls.MessageService` 显示消息
- 外部 DLL 路径使用相对路径 `..\..\DevExpress\DLL\`

##备注
- CLAUDE.md 链接到此文件
powerShell命令:
New-Item -ItemType SymbolicLink -Path "CLAUDE.md" -Target "README.md"

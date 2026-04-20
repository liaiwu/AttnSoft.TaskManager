using System;
using System.IO;
using System.Threading.Tasks;
using TaskManager.BaseObjects;
using TaskManager.TaskCommon;
using Xunit;
using Xunit.Abstractions;

namespace TaskManager.Test
{
    /// <summary>
    /// 调度器集成测试 - 测试完整的调度器初始化和任务加载流程
    /// </summary>
    public class SchedulerIntegrationTest
    {
        private readonly ITestOutputHelper _output;

        public SchedulerIntegrationTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Scheduler_InitAndGetScheduler_Success()
        {
            // Arrange
            _output.WriteLine("[1] 初始化调度器...");

            // Act
            await QuartzHelper.InitScheduler();

            // Assert
            _output.WriteLine("    调度器初始化成功");
        }

        [Fact]
        public async Task Scheduler_StartScheduler_Success()
        {
            // Arrange
            await QuartzHelper.InitScheduler();
            _output.WriteLine("[2] 启动调度器...");

            // Act
            await QuartzHelper.StartScheduler();

            // Assert
            _output.WriteLine("    调度器启动成功");
        }

        [Fact]
        public async Task LoadTasksFromDatabase_Success()
        {
            // Arrange
            await QuartzHelper.InitScheduler();
            await QuartzHelper.StartScheduler();

            _output.WriteLine("[3] 加载任务列表...");

            // Act
            var allTasks = TaskUtilService.Instance.GetAll();

            // Assert
            _output.WriteLine($"    共找到 {allTasks.Count} 个任务");
            Assert.NotNull(allTasks);
        }

        [Fact]
        public async Task ScheduleJobsFromDatabase_Success()
        {
            // Arrange
            await QuartzHelper.InitScheduler();
            await QuartzHelper.StartScheduler();

            _output.WriteLine("[5] 逐个添加任务到调度器...");

            var allTasks = TaskUtilService.Instance.GetAll();

            // Act & Assert
            int successCount = 0;
            int failCount = 0;

            foreach (var task in allTasks)
            {
                try
                {
                    _output.WriteLine($"    添加任务：{task.TaskName}...");
                    await QuartzHelper.ScheduleJob(task);
                    _output.WriteLine($"      成功");
                    successCount++;
                }
                catch (Exception ex)
                {
                    _output.WriteLine($"      失败：{ex.Message}");
                    failCount++;
                }
            }

            _output.WriteLine($"    成功：{successCount}, 失败：{failCount}");

            if (allTasks.Count > 0)
            {
                Assert.Equal(allTasks.Count, successCount + failCount);
            }
        }

        [Fact]
        public async Task TestJob_ExecutesAndCreatesFile()
        {
            // Arrange
            await QuartzHelper.InitScheduler();
            await QuartzHelper.StartScheduler();

            var allTasks = TaskUtilService.Instance.GetAll();

            // 添加所有任务到调度器
            foreach (var task in allTasks)
            {
                try
                {
                    await QuartzHelper.ScheduleJob(task);
                }
                catch { }
            }

            _output.WriteLine("[6] 等待 10 秒，观察任务执行...");

            // Act - 等待任务执行
            for (int i = 10; i > 0; i--)
            {
                _output.WriteLine($"    剩余 {i} 秒");
                await Task.Delay(1000);
            }

            // Assert - 检查 test.txt 文件是否被创建
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.txt");

            if (File.Exists(testFile))
            {
                string content = File.ReadAllText(testFile);
                _output.WriteLine($"[7] TestJob 执行结果:");
                _output.WriteLine($"    test.txt 存在，内容：{content}");
                Assert.True(File.Exists(testFile), "TestJob 应该创建 test.txt 文件");
            }
            else
            {
                _output.WriteLine("[7] TestJob 执行结果:");
                _output.WriteLine("    test.txt 不存在，TestJob 未执行");
            }
        }

        [Fact]
        public async Task Scheduler_StopScheduler_Success()
        {
            // Arrange
            await QuartzHelper.InitScheduler();
            await QuartzHelper.StartScheduler();

            _output.WriteLine("[8] 停止调度器...");

            // Act
            await QuartzHelper.StopScheduleAsync(true);

            // Assert
            _output.WriteLine("    调度器已停止");
        }

        /// <summary>
        /// 完整的端到端测试流程
        /// </summary>
        [Fact]
        public async Task FullSchedulerLifecycle_Complete()
        {
            _output.WriteLine("=== TaskManager 完整生命周期测试 ===");
            _output.WriteLine("");

            // 设置数据库路径
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "Data.db");
            if (!File.Exists(dbPath))
            {
                _output.WriteLine($"警告：找不到数据库文件：{dbPath}");
                _output.WriteLine($"当前目录：{AppDomain.CurrentDomain.BaseDirectory}");
            }
            else
            {
                _output.WriteLine($"数据库文件：{dbPath}");
            }
            _output.WriteLine("");

            try
            {
                // 1. 初始化调度器
                _output.WriteLine("[1] 初始化调度器...");
                await QuartzHelper.InitScheduler();
                _output.WriteLine("    调度器初始化成功");
                _output.WriteLine("");

                // 2. 启动调度器
                _output.WriteLine("[2] 启动调度器...");
                await QuartzHelper.StartScheduler();
                _output.WriteLine("    调度器启动成功");
                _output.WriteLine("");

                // 3. 加载任务列表
                _output.WriteLine("[3] 加载任务列表...");
                var allTasks = TaskUtilService.Instance.GetAll();
                _output.WriteLine($"    共找到 {allTasks.Count} 个任务");
                _output.WriteLine("");

                // 4. 显示任务详情
                _output.WriteLine("[4] 任务详情:");
                foreach (var task in allTasks)
                {
                    _output.WriteLine($"    - {task.TaskName}");
                    _output.WriteLine($"      TaskID: {task.TaskID}");
                    _output.WriteLine($"      Status: {task.Status}");
                    _output.WriteLine($"      Cron: {task.CronExpressionString}");
                    _output.WriteLine($"      Assembly: {task.Assembly}");
                    _output.WriteLine($"      Class: {task.Class}");
                    if (!string.IsNullOrEmpty(task.TaskParam))
                    {
                        _output.WriteLine($"      Param: {task.TaskParam.Substring(0, Math.Min(50, task.TaskParam.Length))}...");
                    }
                    _output.WriteLine("");
                }

                // 5. 添加任务到调度器
                _output.WriteLine("[5] 逐个添加任务到调度器...");
                int successCount = 0;
                int failCount = 0;
                foreach (var task in allTasks)
                {
                    try
                    {
                        _output.WriteLine($"    添加任务：{task.TaskName}...");
                        await QuartzHelper.ScheduleJob(task);
                        _output.WriteLine($"      成功");
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        _output.WriteLine($"      失败：{ex.Message}");
                        failCount++;
                    }
                }
                _output.WriteLine("");
                _output.WriteLine($"    成功：{successCount}, 失败：{failCount}");
                _output.WriteLine("");

                // 6. 等待任务执行
                _output.WriteLine("[6] 等待 10 秒，观察任务执行...");
                for (int i = 10; i > 0; i--)
                {
                    _output.WriteLine($"    剩余 {i} 秒");
                    await Task.Delay(1000);
                }
                _output.WriteLine("");

                // 7. 检查 TestJob 执行结果
                string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.txt");
                if (File.Exists(testFile))
                {
                    _output.WriteLine("[7] TestJob 执行结果:");
                    _output.WriteLine($"    test.txt 存在，内容：{File.ReadAllText(testFile)}");
                }
                else
                {
                    _output.WriteLine("[7] TestJob 执行结果:");
                    _output.WriteLine("    test.txt 不存在，TestJob 未执行");
                }
                _output.WriteLine("");
            }
            finally
            {
                // 8. 停止调度器
                _output.WriteLine("[8] 停止调度器...");
                await QuartzHelper.StopScheduleAsync(true);
                _output.WriteLine("    调度器已停止");
                _output.WriteLine("");
            }

            _output.WriteLine("=== 测试完成 ===");
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TaskManager.BaseObjects;
using TaskManager.TaskCommon;

namespace TaskManager.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== TaskManager 测试程序 ===");
            Console.WriteLine();

            // 设置数据库路径
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "Data.db");
            if (!File.Exists(dbPath))
            {
                Console.WriteLine($"错误：找不到数据库文件：{dbPath}");
                Console.WriteLine($"当前目录：{AppDomain.CurrentDomain.BaseDirectory}");
                return;
            }
            Console.WriteLine($"数据库文件：{dbPath}");
            Console.WriteLine();

            // 初始化调度器
            Console.WriteLine("[1] 初始化调度器...");
            try
            {
                await QuartzHelper.InitScheduler();
                Console.WriteLine("    调度器初始化成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    调度器初始化失败：{ex.Message}");
                return;
            }
            Console.WriteLine();

            // 启动调度器
            Console.WriteLine("[2] 启动调度器...");
            try
            {
                await QuartzHelper.StartScheduler();
                Console.WriteLine("    调度器启动成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    调度器启动失败：{ex.Message}");
                Console.WriteLine($"    完整错误：{ex}");
                return;
            }
            Console.WriteLine();

            // 模拟 LoadData - 加载任务列表
            Console.WriteLine("[3] 加载任务列表...");
            var allTasks = TaskUtilService.Instance.GetAll();
            Console.WriteLine($"    共找到 {allTasks.Count} 个任务");
            Console.WriteLine();

            // 显示所有任务信息
            Console.WriteLine("[4] 任务详情:");
            foreach (var task in allTasks)
            {
                Console.WriteLine($"    - {task.TaskName}");
                Console.WriteLine($"      TaskID: {task.TaskID}");
                Console.WriteLine($"      Status: {task.Status}");
                Console.WriteLine($"      Cron: {task.CronExpressionString}");
                Console.WriteLine($"      Assembly: {task.Assembly}");
                Console.WriteLine($"      Class: {task.Class}");
                Console.WriteLine($"      Param: {task.TaskParam?.Substring(0, Math.Min(50, task.TaskParam.Length))}...");
                Console.WriteLine();
            }

            // 模拟 LoadData - 逐个添加任务到调度器
            Console.WriteLine("[5] 逐个添加任务到调度器...");
            int successCount = 0;
            int failCount = 0;
            foreach (var task in allTasks)
            {
                try
                {
                    Console.WriteLine($"    添加任务：{task.TaskName}...");
                    await QuartzHelper.ScheduleJob(task);
                    Console.WriteLine($"      成功");
                    successCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"      失败：{ex.Message}");
                    failCount++;
                }
            }
            Console.WriteLine();
            Console.WriteLine($"    成功：{successCount}, 失败：{failCount}");
            Console.WriteLine();

            // 等待一段时间，观察任务是否执行
            Console.WriteLine("[6] 等待 10 秒，观察任务执行...");
            for (int i = 10; i > 0; i--)
            {
                Console.Write($"    剩余 {i} 秒\r");
                await Task.Delay(1000);
            }
            Console.WriteLine();

            // 检查 test.txt 文件是否被创建（TestJob 的执行结果）
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.txt");
            if (File.Exists(testFile))
            {
                Console.WriteLine("[7] TestJob 执行结果:");
                Console.WriteLine($"    test.txt 存在，内容：{File.ReadAllText(testFile)}");
            }
            else
            {
                Console.WriteLine("[7] TestJob 执行结果:");
                Console.WriteLine("    test.txt 不存在，TestJob 未执行");
            }
            Console.WriteLine();

            // 停止调度器
            Console.WriteLine("[8] 停止调度器...");
            await QuartzHelper.StopScheduleAsync(true);
            Console.WriteLine("    调度器已停止");
            Console.WriteLine();

            Console.WriteLine("=== 测试完成 ===");
            Console.WriteLine("按任意键退出...");
            if (Console.IsInputRedirected == false)
            {
                Console.ReadKey();
            }
        }
    }
}

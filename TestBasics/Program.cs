using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using Shell32;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Win32;

namespace TestBasics
{
    class Task1
    {
        public void TaskMethod1()
        {
            Task t = new Task(() =>
            {
                Console.WriteLine("任务开始...");
                Thread.Sleep(5000);
            });
            t.Start();
            t.ContinueWith((task) =>
            {
                Console.WriteLine("任务完成，完成时的状态为：");
                Console.WriteLine("IsCanceled={0}\tIsCompleted={1}\tIsFaulted={2}", task.IsCanceled, task.IsCompleted, task.IsFaulted);
            });
        }
    }

    class Task2
    {
        public void TaskMethod1(string param)
        {
            Console.WriteLine($"输入参数：{param}");
        }

        public void TaskMethod2()
        {
            //方式1
            var t1 = new Task(() => {
                TaskMethod1("无返回值方式1.1");
                Console.WriteLine($"无返回值方式1.1线程ID：{Thread.CurrentThread.ManagedThreadId}");
            });
            var t2 = new Task(() => {
                TaskMethod1("无返回值方式1.2");
                Console.WriteLine($"无返回值方式1.2线程ID：{Thread.CurrentThread.ManagedThreadId}");
            });
            t1.Start();
            t2.Start();
            Task.WaitAll(t1, t2);//会等待所有任务结束，主线程才会退出

            //方式2
            Task.Run(() =>
            {
                TaskMethod1("无返回值方式2");
                Console.WriteLine($"无返回值方式2线程ID：{Thread.CurrentThread.ManagedThreadId}");
            });

            //方式3
            Task.Factory.StartNew(() =>
            {
                TaskMethod1("无返回值方式3(1)");
                Console.WriteLine($"无返回值方式3(1)：{Thread.CurrentThread.ManagedThreadId}");
            });//异步方法
            //or
            Task t3 = Task.Factory.StartNew(() =>
            {
                TaskMethod1("无返回值方式3(2)");
                Console.WriteLine($"无返回值方式3(2)：{Thread.CurrentThread.ManagedThreadId}");
            });
            t3.Wait();
        }


        /// <summary>
        /// async/await的实现方式:
        /// </summary>
        public async void TaskMethod3()
        {
            //Task.Delay方法只会延缓异步方法中后续部分执行时间，当程序执行到await表达时，一方面会立即返回调用方法，执行调用方法中的剩余部分，这一部分程序的执行不会延长。另一方面根据Delay()方法中的参数，延时对异步方法中后续部分的执行。
            await Task.Delay(5000);
            Console.WriteLine("执行异步方法");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }
    }

    public struct Point
    {
        public Int32 x, y;
    }

    class Program
    {
        const string formatter = "{0,5}{1,17}{2,10}";

        static SemaphoreSlim semLim = new SemaphoreSlim(3);

        /// <summary>
        /// 注: C#7.1或更高版本才支持 "主异步"
        /// </summary>
        /// <param name="args"></param>
        //public static async Task Main()
        //{
        //    await Task.Run(()=> {
        //    // Just loop.
        //    int ctr = 0;
        //    for (ctr = 0; ctr <= 1000000; ctr++)
        //    { }
        //    Console.WriteLine("Finished {0} loop iterations", ctr);
        //});

        static async Task<int> GetLengthAsync()
        {
            Console.WriteLine("GetLengthAsync Start");
            string str = await GetStringAsync();
            Console.WriteLine("GetLengthAsync End");
            return str.Length;

        }
        static Task<string> GetStringAsync()
        {
            return Task<string>.Run(() => { Thread.Sleep(2000); return "finished"; });
        }

        static void Main(string[] args)
        {
            #region byte字节

            //byte[] byteArray = {15,0,0,255,3,16,39,255,255,127 };

            //Console.WriteLine(BitConverter.ToString(byteArray));

            //BAToUInt16(byteArray, 1);
            //BAToUInt16(byteArray, 0);
            //BAToUInt16(byteArray, 3);
            //BAToUInt16(byteArray, 5);
            //BAToUInt16(byteArray, 8);
            //BAToUInt16(byteArray, 7);


            //Console.WriteLine(sizeof(Int16));
            //Console.WriteLine(sizeof(Int32));
            //Console.WriteLine(sizeof(ushort));
            //Console.WriteLine(sizeof(uint));
            //Console.WriteLine(sizeof(ushort));

            //Console.WriteLine(RuntimeInformation.FrameworkDescription);
            #endregion

            #region Task

            //Console.WriteLine("-------Start-------");
            //Task<int> task = GetLengthAsync();
            //Console.WriteLine("Main is doing other things");
            //Console.WriteLine("Task return: " + task.Result);
            //Console.WriteLine("-------over-------");

            //Console.WriteLine("Managerd: "+Thread.CurrentThread.ManagedThreadId.ToString());
            //Console.WriteLine("AppDomain: " + AppDomain.GetCurrentThreadId().ToString());

            //for (int i = 0; i < 10; i++)
            //{
            //    ThreadPool.QueueUserWorkItem(m =>
            //    {
            //        Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
            //    });
            //}
            //Console.Read();

            //ThreadPool相对于Thread来说可以减少线程的创建，有效减小系统开销；但是ThreadPool不能控制线程的执行顺序，
            //我们也不能获取线程池内线程取消 /异常/完成的通知，即我们不能有效监控和控制线程池中的线程
            //for (int i = 1; i <= 10; i++)
            //{
            //    //ThreadPool执行任务
            //    ThreadPool.QueueUserWorkItem(new WaitCallback((obj) => {
            //        Console.WriteLine($"第{obj}个执行任务");
            //    }), i);
            //}
            //Console.ReadKey();


            ///SemaphoreSlim类的简单使用
            //for(int i=0;i<10;i++)
            //{
            //    new Thread(SemaphoreTest).Start();
            //}

            ////1.new方式实例化一个Task，需要通过Start方法启动
            //Task task = new Task(() =>
            //{
            //    Thread.Sleep(100);
            //    Console.WriteLine($"hello, task1的线程ID为{Thread.CurrentThread.ManagedThreadId}");
            //});
            //task.Start();

            ////2.Task.Factory.StartNew(Action action)创建和启动一个Task
            //Task task2 = Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(100);
            //    Console.WriteLine($"hello, task2的线程ID为{ Thread.CurrentThread.ManagedThreadId}");
            //});

            ////3.Task.Run(Action action)将任务放在线程池队列，返回并启动一个Task
            //Task task3 = Task.Run(() =>
            //{
            //    Thread.Sleep(100);
            //    Console.WriteLine($"hello, task3的线程ID为{ Thread.CurrentThread.ManagedThreadId}");
            //});
            //Console.WriteLine("执行主线程！");

            //修改一下上面的写法:
            ////1.new方式实例化一个Task，需要通过Start方法启动
            //Task<string> task = new Task<string>(() =>
            //{
            //    return $"hello, task1的ID为{Thread.CurrentThread.ManagedThreadId}";
            //});
            //task.Start();

            //////2.Task.Factory.StartNew(Func func)创建和启动一个Task
            //Task<string> task2 = Task.Factory.StartNew<string>(() =>
            //{
            //    return $"hello, task2的ID为{ Thread.CurrentThread.ManagedThreadId}";
            //});

            //////3.Task.Run(Func func)将任务放在线程池队列，返回并启动一个Task
            //Task<string> task3 = Task.Run<string>(() =>
            //{
            //    return $"hello, task3的ID为{ Thread.CurrentThread.ManagedThreadId}";
            //});
            //Console.WriteLine("执行主线程！");
            /////注意task.Resut获取结果时会阻塞线程，即如果task没有执行完成，会等待task执行完成获取到Result，然后再执行后边的代码，程序运行结果如下：   
            //Console.WriteLine(task.Result);
            //Console.WriteLine(task2.Result);
            //Console.WriteLine(task3.Result);


            ///上边的所有栗子中Task的执行都是异步的，不会阻塞主线程。有些场景下我们想让Task同步执行怎么办呢？Task提供了  task.RunSynchronously()用于同步执行Task任务
            //Task taskSync = new Task(() =>
            //{
            //    Thread.Sleep(100);
            //    Console.WriteLine("执行Task结束!");
            //});
            ////同步执行，task会阻塞主线程
            //taskSync.RunSynchronously();
            //Console.WriteLine("执行主线程结束！");

            //Task taskA = Task.Run(() => Thread.Sleep(2000));
            //try
            //{
            //    taskA.Wait(1000);       // Wait for 1 second.
            //    bool completed = taskA.IsCompleted;
            //    Console.WriteLine("Task A completed: {0}, Status: {1}",
            //                     completed, taskA.Status);
            //    if (!completed)
            //        Console.WriteLine("Timed out before task A completed.");
            //}
            //catch (AggregateException)
            //{
            //    Console.WriteLine("Exception in taskA.");
            //}


            //Action<object> action = (object obj) =>
            //{
            //    Console.WriteLine("Task={0}, obj={1}, Thread={2}",
            //    Task.CurrentId, obj,
            //    Thread.CurrentThread.ManagedThreadId);
            //};

            //// Create a task but do not start it.
            //Task t1 = new Task(action, "alpha");

            //// Construct a started task
            //Task t2 = Task.Factory.StartNew(action, "beta");
            //// Block the main thread to demonstrate that t2 is executing
            //t2.Wait();

            //// Launch t1 
            //t1.Start();
            //Console.WriteLine("t1 has been launched. (Main Thread={0})",
            //                  Thread.CurrentThread.ManagedThreadId);
            //// Wait for the task to finish.
            //t1.Wait();

            //// Construct a started task using Task.Run.
            //String taskData = "delta";
            //Task t3 = Task.Run(() => {
            //    Console.WriteLine("Task={0}, obj={1}, Thread={2}",
            //                      Task.CurrentId, taskData,
            //                       Thread.CurrentThread.ManagedThreadId);
            //});
            //// Wait for the task to finish.
            //t3.Wait();

            //// Construct an unstarted task
            //Task t4 = new Task(action, "gamma");
            //// Run it synchronously
            //t4.RunSynchronously();
            //// Although the task was run synchronously, it is a good practice
            //// to wait for it in the event exceptions were thrown by the task.
            //t4.Wait();

            //Task task = Display();
            //task.Wait();
            #endregion

            //Console.WriteLine(GetFileDuration());
            //Console.WriteLine(GetLocalIPV4());
            #region Queue 字符串 Stream MD5
            ///测试Queue队列
            //Queue<string> numbers = new Queue<string>();
            //numbers.Enqueue("one");
            //numbers.Enqueue("two");
            //numbers.Enqueue("three");
            //numbers.Enqueue("four");
            //numbers.Enqueue("five");

            //numbers.Dequeue();
            //numbers.Dequeue();
            //numbers.Dequeue();
            //numbers.Dequeue();
            //numbers.Dequeue();
            //Console.WriteLine(numbers.Count);
            ////numbers.Dequeue(); //必须要判断(不为空情况下)队列中元素的数量是否>0

            ///二维数组
            //int[,] a = new int[2, 3]
            //    {
            //        { 1,2,3},

            //        {7,8,9}
            //    };
            //Console.WriteLine(a.GetLength(0)); //arr.GetLength(xxx)指定维度上的长度
            //Console.WriteLine(a.GetLength(1));

            ///测试stream
            //TestStream();

            //TestBitConvert();

            ///测试字符串处理
            //String s = "aaa";
            //Console.WriteLine("The initial string: '{0}'", s);
            //string s1 = s.Replace("a", "b").Replace("b", "c").Replace("c", "d");
            //Console.WriteLine("The source string: '{0}'", s);
            //Console.WriteLine("The final string: '{0}'", s1);

            //string txt = @"项目中使用了很多圆角矩形的纯色的按钮，背景之类的图片，如果使用传统的九宫格的拉伸，那么不通的圆角半径必须使用不通的图片，而且拉伸后边缘容易出现狗牙（锯齿）。";
            //string[] arr = txt.Split(new char[] { '，', '。' },StringSplitOptions.RemoveEmptyEntries);
            //Console.WriteLine(arr.Length);
            //foreach(var str in arr)
            //{
            //    Console.WriteLine(str);
            //}

            //1、将枚举转换为字符串：
            //Console.WriteLine(DbProviderType.SqlServer.ToString());

            //byte[] result = Encoding.Default.GetBytes(txt);    //tbPass为输入密码的文本框
            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] output = md5.ComputeHash(result);
            //string txt1 = BitConverter.ToString(output).Replace("-", "");
            //Console.WriteLine(txt1);
            //Console.WriteLine(StringMD5Encode16(txt1));
            //Console.WriteLine(StringMD5Encode32(txt1));

            //Console.WriteLine(get2Pow(2));
            //Console.WriteLine(get2Pow(9));
            //Console.WriteLine(get2Pow(14));

            //string str1 = "D:\a.mp4";
            ////Regex reg = new Regex(@"\\s*\\.[mp4|rmvb|flv|mpeg|avi]", RegexOptions.IgnoreCase);
            ////Regex reg = new Regex(@"\\S*\\.[mp4|rmvb|flv|mpeg|avi]", RegexOptions.IgnoreCase);
            ////Console.WriteLine(reg.IsMatch(str1));
            //string pattern1 = @"(.*)(\.mp4)$";
            //Console.WriteLine(Regex.IsMatch(str1, pattern1));

            //Console.WriteLine(uint.MaxValue + ", " + uint.MinValue);
            // Console.WriteLine(12.335f.ToString("f2"));




            // Creates and initializes a new Queue.
            //Queue myQ = new Queue();
            //myQ.Enqueue("The");
            //myQ.Enqueue("quick");
            //myQ.Enqueue("brown");
            //myQ.Enqueue("fox");

            /////没有泛型的Queue可以这样创建线程同步的Queue
            //// Creates a synchronized wrapper around the Queue.
            //Queue mySyncdQ = Queue.Synchronized(myQ);


            //// Displays the sychronization status of both Queues.
            //Console.WriteLine("myQ is {0}.", myQ.IsSynchronized ? "synchronized" : "not synchronized");
            //Console.WriteLine("mySyncdQ is {0}.", mySyncdQ.IsSynchronized ? "synchronized" : "not synchronized");

            //int port = FindUnusedPort.FindNextAvailableTCPPort(30051);
            //Console.WriteLine($"available TPC Port: {port}");

            //DateTime dt = DateTime.Now;
            //Console.WriteLine(dt.ToString("yyyy-MM-dd HH:mm:ss"));
            #endregion
            //Console.WriteLine(123124234.ToString("NO"));
            #region 日志
            ///测试日志级别
            //level = Loglevel.Info;
            //W("AAAAA");
            ////W("AAAAA");


            //// Source must be array or IList.
            //var source = Enumerable.Range(0, 100000).ToArray();

            //// Partition the entire source array.
            //var rangePartitioner = Partitioner.Create(0, source.Length);

            //double[] results = new double[source.Length];

            //// Loop over the partitions in parallel.
            //Parallel.ForEach(rangePartitioner, (range, loopState) =>
            //{
            //    // Loop over each range element without a delegate invocation.
            //    for (int i = range.Item1; i < range.Item2; i++)
            //    {
            //        results[i] = source[i] * Math.PI;
            //    }
            //});

            //Console.WriteLine("Operation complete. Print results? y/n");
            //char input = Console.ReadKey().KeyChar;
            //if (input == 'y' || input == 'Y')
            //{
            //    foreach (double d in results)
            //    {
            //        Console.Write("{0} ", d);
            //    }
            //}
            #endregion

            #region Task的取消 Mutex Semaphore
            /*
                        var tasks = new List<Task<int>>();
                        var source = new CancellationTokenSource();
                        var token = source.Token;
                        int completedIterations = 0;

                        for (int n = 0; n <= 19; n++)
                            tasks.Add(Task.Run(() =>
                            {
                                int iterations = 0;
                                for (int ctr = 1; ctr <= 2000000; ctr++)  ///每个Task的工作区－－－Start :主要操作是从１累加到200000
                                {
                                    token.ThrowIfCancellationRequested();　//每次相加前设置可Cancel请求
                                    iterations++;
                                }                          ////每个线程的工作区－－－End
                                Interlocked.Increment(ref completedIterations);
                                if (completedIterations >= 10)
                                    source.Cancel();
                                return iterations;
                            }, token));

                        Console.WriteLine("Waiting for the first 10 tasks to complete...\n");
                        try
                        {
                            Task.WaitAll(tasks.ToArray());
                        }
                        catch (AggregateException)
                        {
                            Console.WriteLine("Status of tasks:\n");
                            Console.WriteLine("{0,10} {1,20} {2,14:N0}", "Task Id",
                                              "Status", "Iterations");
                            foreach (var t in tasks)
                                Console.WriteLine("{0,10} {1,20} {2,14}",
                                                  t.Id, t.Status,
                                                  t.Status != TaskStatus.Canceled ? t.Result.ToString("N0") : "n/a");
                        }
            */

            //Task1 task1 = new Task1();
            //task1.TaskMethod1();

            //Console.WriteLine($"Main 线程3的ID为{ Thread.CurrentThread.ManagedThreadId}");
            //Task2 task2 = new Task2();
            ////task2.TaskMethod2();
            //Console.WriteLine("主线程执行其他任务...");
            //task2.TaskMethod3();
            //Console.WriteLine("主线程执行其他处理...");
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine($"主线程{i}");
            //}


            //int[] nums = Enumerable.Range(0, 1000000).ToArray();
            //long total = 0;

            //// Use type parameter to make subtotal a long, not an int
            //Parallel.For<long>(0, nums.Length, () => 0, (j, loop, subtotal) =>
            //{
            //    subtotal += nums[j];
            //    return subtotal;
            //},
            //    (x) => Interlocked.Add(ref total, x)
            //);

            //Console.WriteLine("The total is {0:N0}", total);
            //Console.WriteLine("Press any key to exit");

            //MutexExample();
            //SemaphoreExample();
            #endregion

            #region Task.Yield(), Task.CompletedTask;
            //https://blog.csdn.net/java666668888/article/details/104007066
            //Console.WriteLine("Main thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());
            //Console.WriteLine("==============================================");
            //TaskYield().Wait(); //里面的方法在await Task.Yield()的前后使用两个线程来执行的

            //Console.WriteLine("==============================================");

            //TaskCompleted().Wait();

            //Console.WriteLine("==============================================");

            //TaskYieldSimulation().Wait();


            //Execute();
            //SlowLoop().Wait();
            //test().Wait();

            //Task taskA = Task.Run(() => Thread.Sleep(2000));
            //Console.WriteLine("taskA Status: {0}", taskA.Status);
            //try
            //{
            //    taskA.Wait();
            //    Console.WriteLine("taskA Status: {0}", taskA.Status);
            //}
            //catch (AggregateException)
            //{
            //    Console.WriteLine("Exception in taskA.");
            //}


            //var tasks = new Task[3];
            //var rnd = new Random();
            //for (int ctr = 0; ctr <= 2; ctr++)
            //    tasks[ctr] = Task.Run(() => Thread.Sleep(rnd.Next(500, 3000)));

            //try
            //{
            //    int index = Task.WaitAny(tasks);
            //    Console.WriteLine("Task #{0} completed first.\n", tasks[index].Id);
            //    Console.WriteLine("Status of all tasks:");
            //    foreach (var t in tasks)
            //        Console.WriteLine("   Task #{0}: {1}", t.Id, t.Status);
            //}
            //catch (AggregateException)
            //{
            //    Console.WriteLine("An exception occurred.");
            //}

            //var task1 = Task.Run(() => { throw new CustomException("This exception is expected!"); });

            //while (!task1.IsCompleted) { }

            //if (task1.Status == TaskStatus.Faulted)
            //{
            //    foreach (var e in task1.Exception.InnerExceptions)
            //    {
            //        // Handle the custom exception.
            //        if (e is CustomException)
            //        {
            //            Console.WriteLine(e.Message);
            //        }
            //        // Rethrow any other exception.
            //        else
            //        {
            //            throw e;
            //        }
            //    }
            //}

            /////函数的嵌套
            //Fun(2, 6);
            //void Fun(int x, int y)
            //{
            //    Console.WriteLine(x + y);
            //}

            ///结构体
            //OutMsg outMsg;
            //outMsg.info = new MsgInfo();
            //Console.WriteLine(outMsg.info.Equals(default(MsgInfo)));

            /////CountdownEvent的使用
            //CountdownEvent countdownEvent = new CountdownEvent(4);
            //for (int i = 1; i < 5; i++)
            //{
            //    int v = i;
            //    Task.Run(() => {
            //        Thread.Sleep(v * 1000);
            //        countdownEvent.Signal();
            //        Console.WriteLine(v);
            //    });
            //}
            //countdownEvent.Wait();//等待4个信号全部来袭后，继续往下执行
            //Console.WriteLine("完成咯");
            //countdownEvent.Dispose();


            /////前、后台线程
            //Thread t = new Thread(new ThreadStart(() => {
            //    Thread.Sleep(5000);
            //    Console.WriteLine("AAAAA"); }));

            //// Start ThreadProc.  Note that on a uniprocessor, the new
            //// thread does not get any processor time until the main thread
            //// is preempted or yields.  Uncomment the Thread.Sleep that
            //// follows t.Start() to see the difference.
            //t.Start();
            //Console.WriteLine($"t.IsBackground:{t.IsBackground}");


            ////新建一个Stopwatch变量用来统计程序运行时间
            //Stopwatch watch = Stopwatch.StartNew();
            ////获取本机运行的所有进程ID和进程名,并输出哥进程所使用的工作集和私有工作集
            //Process[] pss = Process.GetProcesses();
            //foreach (Process ps in pss)
            //{
            //    PerformanceCounter pf1 = new PerformanceCounter("Process", "Working Set - Private", ps.ProcessName);
            //    PerformanceCounter pf2 = new PerformanceCounter("Process", "Working Set", ps.ProcessName);
            //    Console.WriteLine("{0}:{1}  {2:N}KB", ps.ProcessName, "工作集(进程类)", ps.WorkingSet64 / 1024);
            //    Console.WriteLine("{0}:{1}  {2:N}KB", ps.ProcessName, "工作集        ", pf2.NextValue() / 1024);
            //    //私有工作集
            //    Console.WriteLine("{0}:{1}  {2:N}KB", ps.ProcessName, "私有工作集    ", pf1.NextValue() / 1024);

            //}
            //Console.WriteLine($"pss count:{pss.Length}");
            //watch.Stop();
            //Console.WriteLine(watch.Elapsed);
            #endregion

            /*
            //两个数组比较，找出不同的：有两种情况：第一个集合有而第二个集合没有的，第二个集合有而第一个集合没有的
            double[] numbers1 = { 2.0, 2.1, 2.2, 2.3, 2.4 };
            double[] numbers2 = { 2.1, 2.2, 2.3, 2.4, 2.5 };
            //double[] numbers2 = { 2.2 };

            IEnumerable<double> onlyInFirstSet = numbers1.Except(numbers2);//找出仅在numbers1中出现的元素 =>即关闭了的进程

            foreach (double number in onlyInFirstSet)
                Console.WriteLine( number );


            IEnumerable<double> onlyInSecondSet = numbers2.Except(numbers1);//找出仅在numbers1中出现的元素 =>即关闭了的进程

            foreach (double number in onlyInSecondSet)
                Console.WriteLine(number);
            */


            //#region 监控进程状态
            // const string processName = "viBot_Cloud"; //进程名
            // const int reqIntervals = 10000; //ms
            // Process[] processes = Process.GetProcessesByName(processName);//因为可以同时启动多个abc.exe，所以返回值是数组。
            // int count = processes.Length ;
            // Console.WriteLine($"当前开启了{count}个{processName}进程");

            // if (count > 0)
            // {
            //    //string processPath = processes[0].MainModule.FileName;
            //    int i;
            //    while (true)
            //    {
            //        for (i = 0; i < count; i++)
            //        {
            //            if (!processes[i].Responding)
            //            {
            //                Console.WriteLine("应用程序用户界面无反应，正在Kill掉自己…………");
            //                try
            //                {
            //                    processes[i].Kill();
            //                    Console.WriteLine("应用程序用户界面无反应，正在重新启动一个新的…………");
            //                    Thread.Sleep(3000);
            //                    Process.Start(processes[0].MainModule.FileName);
            //                }
            //                catch(Exception e)
            //                {
            //                    Console.WriteLine(e.Message);
            //                }
            //            }
            //        }
            //        Thread.Sleep(reqIntervals);
            //    }
            // }

            //     while (true)
            //     {
            //         var counters = PerformanceCounterCategory.GetCategories()
            //.SelectMany(x => x.GetCounters("")).Where(x => x.CounterName.Contains("GPU"));

            //         foreach (var counter in counters)
            //         {
            //             Console.WriteLine("{0} - {1}", counter.CategoryName, counter.CounterName);
            //         }
            //         Thread.Sleep(5000);
            //     }

            ////获取当前进程对象
            //Process cur = Process.GetCurrentProcess();

            //PerformanceCounter curpcp = new PerformanceCounter("Process", "Working Set - Private", cur.ProcessName);
            //PerformanceCounter curpc = new PerformanceCounter("Process", "Working Set", cur.ProcessName);
            //PerformanceCounter curtime = new PerformanceCounter("Process", "% Processor Time", cur.ProcessName);

            ////上次记录CPU的时间
            //TimeSpan prevCpuTime = TimeSpan.Zero;
            ////Sleep的时间间隔
            //int interval = 5000;

            //PerformanceCounter totalcpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            //SystemInfo sys = new SystemInfo();
            //const int KB_DIV = 1024;
            //const int MB_DIV = 1024 * 1024;
            //const int GB_DIV = 1024 * 1024 * 1024;
            //Console.WriteLine("进程名    ｜   PID    ｜  内存(KB)  ｜ 进程CPU使用率 | 系统CPU使用率 ｜ 系统内存使用率 | ");
            //while (true)
            //{
            //    #region
            //    ////第一种方法计算CPU使用率
            //    ////当前时间
            //    //TimeSpan curCpuTime = cur.TotalProcessorTime;
            //    ////计算
            //    //double value = (curCpuTime - prevCpuTime).TotalMilliseconds / interval / Environment.ProcessorCount * 100;
            //    //prevCpuTime = curCpuTime;

            //    //Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3:f3}", cur.ProcessName, "工作集(进程类)", cur.WorkingSet64 / 1024, value);//这个工作集只是在一开始初始化，后期不变
            //    //Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3:f3}", cur.ProcessName, "工作集        ", curpc.NextValue() / 1024, value);//这个工作集是动态更新的
            //    ////第二种计算CPU使用率的方法
            //    //Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3:f3}%", cur.ProcessName, "私有工作集    ", curpcp.NextValue() / 1024, curtime.NextValue() / Environment.ProcessorCount);
            //    ////Thread.Sleep(interval);

            //    ////第一种方法获取系统CPU使用情况
            //    //Console.Write("\r系统CPU使用率：{0}%", totalcpu.NextValue());
            //    ////Thread.Sleep(interval);

            //    ////第二章方法获取系统CPU和内存使用情况
            //    //Console.Write("\r系统CPU使用率：{0}%，系统内存使用大小：{1}MB({2}GB)", sys.CpuLoad, (sys.PhysicalMemory - sys.MemoryAvailable) / MB_DIV, (sys.PhysicalMemory - sys.MemoryAvailable) / (double)GB_DIV);
            //    #endregion

            //    Console.WriteLine("{0} ｜ {1} | {2:N}KB |   {3:f3}%   |    {4:f3}%     |  {5:f2}MB({6:f2}GB)", cur.ProcessName, cur.Id, curpc.NextValue(), curtime.NextValue() / Environment.ProcessorCount, sys.CpuLoad,
            //                      (sys.PhysicalMemory - sys.MemoryAvailable) / MB_DIV, (sys.PhysicalMemory - sys.MemoryAvailable) / (double)GB_DIV);

            //    Thread.Sleep(interval);
            //}
            //#endregion

            ////Marshal.SizeOf(Object)返回对象的非托管大小(字节), Marshal.SizeOf(Type)非托管类型的大小(字节)
            //// Demonstrate the use of public static fields of the Marshal class
            //Console.WriteLine($"SystemDefaultCharSize: {Marshal.SystemDefaultCharSize}, SystemMaxDBCSCharSize: {Marshal.SystemMaxDBCSCharSize}");
            //Console.WriteLine($"Number of bytes needed by Point class: {Marshal.SizeOf(typeof(Point))}");
            //// Demonstrate the use of the SizeOf method of the Marshal class.
            //Point p = new Point();
            //Console.WriteLine($"Number of bytes needed by a Point object: {Marshal.SizeOf(p)}");

            ////从进程的非托管内存中分配内存,指定字节数
            //IntPtr hglobal = Marshal.AllocHGlobal(100);
            ////还有一种是使用指向指定字节数的指针，从进程的非托管内存中分配内存:Marshar.AllocHGlobal(IntPtr)
            //Marshal.FreeHGlobal(hglobal);


            ////字符串的操作和嵌套
            //int inde = "{sdf:sd}|sdsd".LastIndexOf("}|");
            //Console.WriteLine(inde);
            //Console.WriteLine("{sdf:sd}|sdsd".Substring(0, inde + 1));
            //Console.WriteLine(string.Format("{0}{1}","AAA", "}"));

            ////在静态方法中获取类名
            //Console.WriteLine(MethodBase.GetCurrentMethod().ReflectedType.Name);
            //////在普通方法中获取类名
            //new Program().testGetClassByFunc();


            //RegistryKey hklm = Registry.LocalMachine;
            //RegistryKey hkSoftWare = hklm.CreateSubKey(@"SOFTWARE\\test",true);
            //hklm.Close();
            //hkSoftWare.Close();
            //Console.WriteLine("在@\"SOFTWARE\test\"下创子key成功");

            //RegistryKey curUserKey = Registry.CurrentUser;
            //curUserKey.DeleteSubKey("", true);
            //curUserKey.Close();

            ///C#结构体操作
            //StruA strua = new StruA();
            //Console.WriteLine($"{strua.a}, {strua.b}");

            //控制台输出的闪烁、转动
            //Console.Write("Working... ");
            //int spinIndex = 0;
            //while (true)
            //{
            //    // obfuscate FTW! Let's hope overflow is disabled or testers are impatient
            //    Console.Write("\b" + @"/-\|"[(spinIndex++) & 3]);
            //}

            #region Thread.Abort();//线程终止后，不能再Start()
            /*
                        Thread newThread = new Thread(new ThreadStart(TestMethod));
                        newThread.Start();
                        Thread.Sleep(1000);

                        // Abort newThread.
                        Console.WriteLine("Main aborting new thread.");
                        newThread.Abort("Information from Main.");　//不是马上终止的，

                        // Wait for the thread to terminate.
                        newThread.Join();
                        Console.WriteLine("New thread terminated - Main exiting.");
                */
            #endregion


            //#region 元组（Tuple）C# 7.0版本开始支持； 可以是不同类型的集合的轻型数据结构； 元组类型是值类型；元组元素是公共字段。 这使得元组为可变的值类型
            //////https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/builtin-types/value-tuples
            ////(double, int) t1 = (4.5, 3);
            ////Console.WriteLine($"Tuple with elements{t1.Item1} and {t1.Item2}");
            //#endregion

            //while (true)
            //{
            //    try
            //    {
            //        var gpuCounters = GetGPUCounters();
            //        var gpuUsage = GetGPUUsage(gpuCounters);
            //        Console.WriteLine($"CPU:{GetCPUTotalUse()},GPU usage(%):{gpuUsage}");
            //        continue;
            //    }
            //    catch { }

            //    Thread.Sleep(1000);
            //}

            string str = "AAAaxx";

            //Console.WriteLine("AAAAxx".TrimEnd("min".ToCharArray()));
            Console.WriteLine(str.Remove(str.LastIndexOf("xx")).Remove(str.LastIndexOf("cc")));

            Console.ReadKey();
        }


        public static float GetCPUTotalUse(int lDelay = 1)
        {
            PerformanceCounter processorTotal =
                new PerformanceCounter("Processor", "% Processor Time", "_Total");
            processorTotal.NextValue();
            Thread.Sleep(lDelay * 1000);
            return processorTotal.NextValue();
        }

        public static List<PerformanceCounter> GetGPUCounters()
        {
            var category = new PerformanceCounterCategory("GPU Engine");
            var counterNames = category.GetInstanceNames();

            var gpuCounters = counterNames
                                .Where(counterName => counterName.EndsWith("engtype_3D"))
                                .SelectMany(counterName => category.GetCounters(counterName))
                                .Where(counter => counter.CounterName.Equals("Utilization Percentage"))
                                .ToList();

            return gpuCounters;
        }

        public static float GetGPUUsage(List<PerformanceCounter> gpuCounters)
        {
            gpuCounters.ForEach(x => x.NextValue());

            Thread.Sleep(1000);

            var result = gpuCounters.Sum(x => x.NextValue());

            return result;
        }

        static void TestMethod()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("New thread running.");
                    Thread.Sleep(1000);
                }
            }
            catch (ThreadAbortException abortException)
            {
                Console.WriteLine((string)abortException.ExceptionState);
            }
        }



        struct StruA
        {
            public int a;
            public int b;
        }

        //在普通方法中获取类名
        void testGetClassByFunc()
        {
            Console.WriteLine(this.GetType().Name);
        }

        struct MsgInfo
        {
            public int nPkgIndex;
            public int nSessinIndex;  //Session Index
        }
        struct OutMsg
        {
            public PredictVideoOut msg;
            public MsgInfo info;
        }
        public sealed class PredictVideoOut { }



        static bool isWork = false;
        static IEnumerator LoopWork()
        {
            while (isWork) {

            }
            yield return 1;
        }

        public static async Task test()
        {
            await SlowLoop();
            Console.WriteLine("BBB");
        }

        public static async Task SlowLoop()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(5000);
                    Console.WriteLine(DateTime.Now.Millisecond);
                }
            });


            Console.WriteLine($"AAA{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"AAA");
        }

        public static async void Execute()
        {
            int t = await Task.Run(() => Calculate());
            Console.WriteLine("Result: " + t);
        }

        static int Calculate()
        {
            // calculate total count of digits in strings.
            int size = 0;
            for (int z = 0; z < 100; z++)
            {
                for (int i = 0; i < 100000; i++)
                {
                    string value = i.ToString();
                    size += value.Length;
                }
            }
            return size;
        }

        /// TaskYield使用await Task.Yield()，是真正的异步执行，await关键字之前和之后的代码使用不同的线程执行
        static async Task TaskYield()
        {
            Console.WriteLine("TaskYield before await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

            await Task.Yield();//执行到await Task.Yield()时，调用TaskYield()方法的线程（主线程）立即就返回了，await关键字后面的代码实际上是由另一个线程池线程执行的
            //注意Task.Yield()方法返回的不是Task类的对象，而是System.Runtime.CompilerServices.YieldAwaitable类的对象

            Console.WriteLine("TaskYield after await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

            Thread.Sleep(3000);//阻塞线程3秒钟，模拟耗时的操作

            Console.WriteLine("TaskYield finished!");
        }

        static async Task TaskCompleted()
        {
            Console.WriteLine("TaskCompleted before await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

            await Task.CompletedTask;//执行到await Task.CompletedTask时，由于await的Task.CompletedTask已经处于完成状态，所以.NET Core判定await关键字后面的代码还是由调用TaskCompleted()方法的线程（主线程）来执行，所以实际上整个TaskCompleted()方法是单线程同步执行的

            Console.WriteLine("TaskCompleted after await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

            Thread.Sleep(3000);//阻塞线程3秒钟，模拟耗时的操作

            Console.WriteLine("TaskCompleted finished!");
        }

        // 模拟TaskYield的异步执行
        static Task TaskYieldSimulation()
        {
            //模拟TaskYield()方法中，await关键字之前的代码，由调用TaskYieldSimulation()方法的线程（主线程）执行
            Console.WriteLine("TaskYieldSimulation before await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

            return Task.Run(() =>
            {
                //使用Task.Run启动一个新的线程什么都不做，立即完成，相当于就是Task.Yield()
            }).ContinueWith(t =>
            {
                //下面模拟的是TaskYield()方法中，await关键字之后的代码，由另一个线程池线程执行

                Console.WriteLine("TaskYieldSimulation after await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

                Thread.Sleep(3000);//阻塞线程3秒钟，模拟耗时的操作

                Console.WriteLine("TaskYieldSimulation finished!");
            });
        }

        //test Mutex
        private static Mutex mutex = new Mutex();
        static void MutexDemo()
        {
            try
            {
                bool isSafe = mutex.WaitOne();
                if (isSafe)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} has entered");
                    Thread.Sleep(500);
                    Console.WriteLine($"{Thread.CurrentThread.Name} Leaving");
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        public static void MutexExample()
        {
            for(int i = 0; i < 10; i++)
            {
                Thread t = new Thread(MutexDemo);
                t.Name = $"Thread {i+1} :";
                t.Start();
            }
        }


        //Semaphone
        static Semaphore _pool = new Semaphore(0, 4);
        static int _padding;
        public static void SemaphoreDemo()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} processing");
            try
            {
                _pool.WaitOne();
                int padding = Interlocked.Add(ref _padding, 100);
                Console.WriteLine($"{Thread.CurrentThread.Name} is in; padding:{padding}");
                Thread.Sleep(1000 + padding);
                Console.WriteLine($"{Thread.CurrentThread.Name} completed");
            }
            finally
            {
                _pool.Release();
            }
        }
        public static void SemaphoreExample()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread(new ThreadStart(SemaphoreDemo));
                t.Name = $"Thread-name: {i}";
                t.Start();
            }
            Thread.Sleep(500);
            _pool.Release(4);
        }


        private static Loglevel level;
        public enum Loglevel
        {
            Info,
            Warning,
            Error
        }
        public static void W(string msg)
        {
            if (level <= Loglevel.Warning)
            {
                Console.WriteLine($"W:{msg}");
            }
        }

        public enum DbProviderType
        {
            SqlServer,
            Oracle
        }

        private static int get2Pow(int into)
        {
            int outo = 1;
            for (int i = 0; i < 10; i++)
            {
                outo *= 2;
                if (outo > into)
                {
                    break;
                }
            }

            return outo;
        }

        static string GetLocalIPV4()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    break;
                }
            }
            return AddressIP;
        }

        public static string StringMD5Encode16(string content)
        {
            string md5Pwd = string.Empty;

            //使用加密服务提供程序
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //将指定的字节子数组的每个元素的数值转换为它的等效十六进制字符串表示形式。
            md5Pwd = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(content)), 4, 8);

            md5Pwd = md5Pwd.Replace("-", "");

            return md5Pwd;
        }

        public static string StringMD5Encode32(string content)
        {
            string pwd = string.Empty;

            //实例化一个md5对像
            MD5 md5 = MD5.Create();

            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(content));

            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }

            return pwd;
        }


    　　public static void BAToUInt16(byte[] bytes, int index)
        {
            ushort value = BitConverter.ToUInt16(bytes, index);
            Console.WriteLine(formatter, index,
            BitConverter.ToString(bytes, index, 2), value);
        }

        static  void SemaphoreTest()
        {
            semLim.Wait();
            Console.WriteLine($"线程{Thread.CurrentThread.ManagedThreadId.ToString()}开始执行");
            Thread.Sleep(2000);
            Console.WriteLine($"线程{Thread.CurrentThread.ManagedThreadId.ToString()}执行完毕");
            semLim.Release();
        }

        public static void  OpenFfmpegProcess()
        {
            //Process process = new Process();
            //process.StartInfo = new ProcessStartInfo(路径, 参数);
            //process.Start();
            //process.WaitForExit();
            //Console.WriteLine("ffmpeg处理已经完成！");
        }

        static void DoProcessing(IProgress<int> progress)
        {
            for (int i = 0; i <= 100; ++i)
            {
                Thread.Sleep(100);
                if (progress != null)
                {
                    progress.Report(i);
                }
            }
        }

        //static async Task Display()
        //{
        //    //当前线程
        //    var progress = new Progress<int>(percent =>
        //    {
        //        Console.Clear();
        //        Console.Write("{0}%", percent);
        //    });
        //    //线程池线程
        //    await Task.Run(() => DoProcessing(progress));
        //    Console.WriteLine("");
        //    Console.WriteLine("结束");
        //}

        //public static Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    var tcs = new TaskCompletionSource<object>();
        //    process.EnableRaisingEvents = true;
        //    process.Exited += (sender, args) => tcs.TrySetResult(null);
        //    if (cancellationToken != default(CancellationToken))
        //        cancellationToken.Register(tcs.SetCanceled);

        //    return tcs.Task;
        //}
        //public async void FfmpegProcessAsync()
        //{
        //    Process process = new Process();
        //    process.Start();
        //    await process.WaitForExitAsync();

        //    //Do some fun stuff here...
        //}

        public static string GetFileDuration()
        {
            ShellClass sh = new ShellClass();
            Folder dir = sh.NameSpace(Path.GetDirectoryName("D://GMF/素材/Videos/custom/out_10s.aac"));
            FolderItem item = dir.ParseName(Path.GetFileName("D://GMF/素材/Videos/custom/out_10s.aac"));
            String durationStr = dir.GetDetailsOf(item, 27);    //获取时长字符串(00:00:01)
            if (!durationStr.Equals(""))
            {
                try
                {
                    String[] durationArray = durationStr.Split(':');    //获取长度  iColumn:27
                    int duration = 0;    //时长(毫秒)
                    duration += int.Parse(durationArray[0]) * 60 * 60 * 1000;
                    duration += int.Parse(durationArray[1]) * 60 * 1000;
                    duration += int.Parse(durationArray[2]) * 1000;
                    return duration + "";
                }
                catch (Exception ex)
                {
                    //log
                    return "";
                }
            }
            return "";
        }

        //private static TimeSpan GetVideoDuration(string filePath)
        //{
            //using (var shell = ShellObject.FromParsingName(filePath))
            //{
            //    IShellProperty prop = shell.Properties.System.Media.Duration;
            //    var t = (ulong)prop.ValueAsObject;
            //    return TimeSpan.FromTicks((long)t);
            //}
        //}


        public static void TestStream()
        {
            byte[] buffer = null;
            string testString = "Stream!Hello world";
            char[] readCharArray = null;
            byte[] readBuffer = null;
            string readString = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                Console.WriteLine("初始字符串为: {0}", testString);
                if (stream.CanRead)
                {
                    buffer = Encoding.Default.GetBytes(testString);
                    stream.Write(buffer, 0, 3);
                    Console.WriteLine("现在Stream.Postion在第{0}位置", stream.Position + 1);
                    long newPositionInStream = stream.CanSeek ? stream.Seek(3, SeekOrigin.Current) : 0;
                    Console.WriteLine("重新定位后Stream.Position在第{0}位置", newPositionInStream + 1);
                    if(newPositionInStream < buffer.Length)
                    {
                        stream.Write(buffer, (int)newPositionInStream,buffer.Length -(int)newPositionInStream);
                    }

                    //写完后将stream的position设置成0,开始读取流中的数据
                    stream.Position = 0;
                    readBuffer = new byte[stream.Length];
                    int count = stream.CanRead ? stream.Read(readBuffer, 0, readBuffer.Length) : 0;

                    //由于刚开始时我们使用加密Encoding的方式,所以我们必须解密将readBuffer转化成Char数组，这样才能重新拼接成string

                    //首先通过流读出的readBuffer的数据求出从相应Char的数量
                    int charCount = Encoding.Default.GetCharCount(readBuffer, 0, count);
                    readCharArray = new char[charCount];
                    //Encoding 类的强悍之处就是不仅包含加密的方法，甚至将解密者都能创建出来（GetDecoder()），
                    //解密者便会将readCharArray填充（通过GetChars方法，把readBuffer 逐个转化将byte转化成char，并且按一致顺序填充到readCharArray中）
                    Encoding.Default.GetDecoder().GetChars(readBuffer, 0, count, readCharArray, 0);
                    for(int i=0;i<readCharArray.Length; i++)
                    {
                        readString += readCharArray[i];
                    }
                    Console.WriteLine("读取的字符串为：{0}",readString);
                }
                stream.Close();
            }
        }


        //C#中byte的范围是0~255; 而java中是-128~127
        public static void TestBitConvert()
        {
            const string formatter = "{0,25}{1,30}";
            double aDoubl = 0.1111111111111111111;
            float aSingl = 0.1111111111111111111F;
            long aLong = 1111111111111111111;
            int anInt = 1111111111;
            short aShort = 11111;
            char aChar = '*';
            bool aBool = true;

            Console.WriteLine(
                "This example of methods of the BitConverter class" +
                "\ngenerates the following output.\n");
            Console.WriteLine(formatter, "argument", "byte array");
            Console.WriteLine(formatter, "--------", "----------");

            // Convert values to Byte arrays and display them(使用十六进制显示)
            Console.WriteLine(formatter, aDoubl,
                BitConverter.ToString(BitConverter.GetBytes(aDoubl)));
            Console.WriteLine(formatter, aSingl,
                BitConverter.ToString(BitConverter.GetBytes(aSingl)));
            Console.WriteLine(formatter, aLong,
                BitConverter.ToString(BitConverter.GetBytes(aLong)));
            Console.WriteLine(formatter, anInt,
                BitConverter.ToString(BitConverter.GetBytes(anInt)));
            Console.WriteLine(formatter, aShort,
                BitConverter.ToString(BitConverter.GetBytes(aShort)));
            Console.WriteLine(formatter, aChar,
                BitConverter.ToString(BitConverter.GetBytes(aChar)));
            Console.WriteLine(formatter, aBool,
                BitConverter.ToString(BitConverter.GetBytes(aBool)));
        }
    }

    public class CustomException : Exception
    {
        public CustomException(String message) : base(message)
        { }
    }


}

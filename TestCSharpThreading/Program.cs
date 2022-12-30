using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestCSharpThreading
{
    struct Student
    {
        private string name;
        private int age;
        private bool gentle;
        public void SetValues(string nam, int ag, bool gent)
        {
            name = nam;
            age = ag;
            gentle = gent;
        }
        public int GetAge() { return age; }
    }

    public class ServerClass
    {
        public static void StaticMethod(object obj)
        {
            CancellationToken ct = (CancellationToken)obj;
            Console.WriteLine("ServerClass.StaticMethod is running on another thread.");

            // Simulate work that can be canceled.
            while (!ct.IsCancellationRequested)
            {
                Thread.SpinWait(50000);
            }
            Console.WriteLine("The worker thread has been canceled. Press any key to exit.");
            Console.ReadKey(true);
        }
    }

    ///互斥锁 Mutex 样例
    class ShareRes
    {
        public static int count = 0;
        public static Mutex mutex = new Mutex(); //无参数，表示创建一个处于未获取状态的互斥锁
    }

    class IncThread
    {
        int number;
        public Thread thrd;
        public IncThread(string name, int n)
        {
            thrd = new Thread(this.run);
            number = n;
            thrd.Name = name;
            thrd.Start();
        }

        void run()
        {
            Console.WriteLine("{0}正在等待 the mutex", thrd.Name);
            ShareRes.mutex.WaitOne();
            Console.WriteLine("{0}申请到 the mutex", thrd.Name);
            do
            {
                Thread.Sleep(1000);
                ShareRes.count++;
                Console.WriteLine("In {0} ShareRes.count is {1}", thrd.Name, ShareRes.count);
                number--;
            } while (number > 0);

            Console.WriteLine("{0}释放 the mutex", thrd.Name);
            ShareRes.mutex.ReleaseMutex();
        }
    }

    class DecThread
    {
        int number;
        public Thread thrd;
        public DecThread(string name, int n)
        {
            thrd = new Thread(this.run);
            number = n;
            thrd.Name = name;
            thrd.Start();
        }
        void run()
        {
            Console.WriteLine(thrd.Name + "正在等待 the mutex");
            ShareRes.mutex.WaitOne(); //申请
            Console.WriteLine(thrd.Name + "申请到 the mutex");
            do
            {
                Thread.Sleep(1000);
                ShareRes.count--;
                Console.WriteLine("In " + thrd.Name + "ShareRes.count is " + ShareRes.count);
                number--;
            } while (number > 0);
            Console.WriteLine(thrd.Name + "释放 the nmutex");
            ShareRes.mutex.ReleaseMutex(); //释放
        }
    }

    class Program
    {
        static WaitHandle[] waitHandles = new WaitHandle[] {
            new AutoResetEvent(false),
            new AutoResetEvent(false)
        };


        public class TryEnter
        {
            public TryEnter()
            {
            }

            public void CriticalSection()
            {
                bool b = Monitor.TryEnter(this, 1000);
                //Console.WriteLine("Thread " + Thread.CurrentThread.GetHashCode() + " TryEnter Value结果：" + b);
                Console.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " TryEnter Value结果：" + b);

                if (b)
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        Thread.Sleep(1000);
                        //Console.WriteLine(i + " " + Thread.CurrentThread.GetHashCode() + " ");
                        Console.WriteLine(i + " " + Thread.CurrentThread.ManagedThreadId + " ");
                    }
                }
                if (b)
                {
                    Monitor.Exit(this);
                    Console.WriteLine("释放并退出锁");
                }
            }
        }


        static void Main(string[] args)
        {
            //Console.WriteLine("Test CSharpt Threading...");

            //Thread t = new Thread(threadFunc) { IsBackground = true };
            //t.Start();

            //Console.WriteLine("主线程做其他事情！");

            ////throw new Exception("异常呀~~~异常呀~~~异常呀~~~异常呀~~~异常呀~~~异常呀");

            //Thread.Sleep(300);
            //Console.WriteLine("主线程结束");


            //代码2
            //try
            //{
            //    throw new Exception("参数越界");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //Console.WriteLine("异常后");//可以执⾏
            //                          //代码3
            //if (true)
            //{
            //    throw new Exception("参数越界");
            //}
            //Console.WriteLine("异常后"); //抛出异常，不会执⾏


            //Student stu1 = new Student();
            //Student stu2;
            //stu1.SetValues("Nicholas", 12, true);

            //stu2 = stu1;

            //Console.WriteLine(stu2.GetAge());


            //CancellationTokenSource cts = new CancellationTokenSource();
            //Console.WriteLine("Press 'C' to terminate the application...\n");
            //Thread t1 = new Thread(() => {
            //    if (Console.ReadKey(true).KeyChar.ToString().ToUpperInvariant() == "C")
            //    {
            //        cts.Cancel();
            //    }
            //});

            //Thread t2 = new Thread(new ParameterizedThreadStart(ServerClass.StaticMethod));
            //t1.Start();
            //t2.Start(cts.Token);
            //t2.Join();
            //cts.Dispose();


            //IncThread mthrd1 = new IncThread("IncThread thread ", 5);
            //DecThread mthrd2 = new DecThread("DecThread thread ", 5);
            //mthrd1.thrd.Join();  //让当前线程睡眠，等待mthrd1.thrd线程执行完，然后继续运行下面的代码
            //mthrd2.thrd.Join();

            //Console.WriteLine($"Main线程 id: {Thread.CurrentThread.ManagedThreadId}");

            ////ThreadPool.QueueUserWorkItem使用的三种方式：
            //ThreadPool.QueueUserWorkItem(AsyncOperation); //直接将方法传递给线程池，AsyncOperation为要异步执行的方法
            //ThreadPool.QueueUserWorkItem(AsyncOperation, "async state"); //直接将方法传递给线程池 并且通过state传递参数
            //ThreadPool.QueueUserWorkItem(state =>  //使用Lambda表达式将任务传递给线程池 并且通过 state传递参数
            //{
            //    Console.WriteLine($"Operation state: {state}");
            //    Console.WriteLine($"工作线程 id: {Thread.CurrentThread.ManagedThreadId}");
            //}, "lambda state");


            
            //测试Monitor.TryEnter(lockObj, timeout)
            TryEnter a = new TryEnter();
            Thread t1 = new Thread(new ThreadStart(a.CriticalSection));
            Thread t2 = new Thread(new ThreadStart(a.CriticalSection));
            t1.Start();
            t2.Start();
            



            Console.ReadKey();
        }

        private static void AsyncOperation(object state)
        {
            Console.WriteLine($"Operation state: {state ?? "(null)"}");
            Console.WriteLine($"工作线程 id: {Thread.CurrentThread.ManagedThreadId}");
        }

        static void threadFunc()
        {
            Console.WriteLine("后台线程-threadFunc----Start");
            Thread.Sleep(700);
            Console.WriteLine("后台线程-threadFunc----End");
        }
    }
}

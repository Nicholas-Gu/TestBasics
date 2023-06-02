using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestCSharpThreading
{
    //class TestInterLock
    //{
    //    //0 for false, 1 for true.
    //    private static int usingResource = 0;

    //    private const int numThreadIterations = 5;
    //    private const int numThreads = 10;

    //    static void Main()
    //    {
    //        Thread myThread;
    //        Random rnd = new Random();

    //        for (int i = 0; i < numThreads; i++)
    //        {
    //            myThread = new Thread(new ThreadStart(MyThreadProc));
    //            myThread.Name = String.Format("Thread{0}", i + 1);

    //            //Wait a random amount of time before starting next thread.
    //            Thread.Sleep(rnd.Next(0, 1000));
    //            myThread.Start();
    //        }

    //        Console.ReadKey();
    //    }

    //    private static void MyThreadProc()
    //    {
    //        for (int i = 0; i < numThreadIterations; i++)
    //        {
    //            UseResource();

    //            //Wait 1 second before next attempt.
    //            Thread.Sleep(1000);
    //        }
    //    }

    //    //A simple method that denies reentrancy.
    //    static bool UseResource()
    //    {
    //        //0 indicates that the method is not in use.
    //        if (0 == Interlocked.Exchange(ref usingResource, 1))
    //        {
    //            Console.WriteLine("{0} acquired the lock", Thread.CurrentThread.Name);

    //            //Code to access a resource that is not thread safe would go here.

    //            //Simulate some work
    //            Thread.Sleep(500);

    //            Console.WriteLine("{0} exiting lock", Thread.CurrentThread.Name);

    //            //Release the lock
    //            Interlocked.Exchange(ref usingResource, 0);
    //            return true;
    //        }
    //        else
    //        {
    //            Console.WriteLine("   {0} was denied the lock", Thread.CurrentThread.Name);
    //            return false;
    //        }
    //    }
    //}



    //refer by https://blog.csdn.net/SmillCool/article/details/127118858

    ////****************** 1.不加锁的会有问题  *****************/
    //public class LudwigInterlocked
    //{

    //    string persion;

    //    public LudwigInterlocked()
    //    {
    //        persion = "没人啊？";
    //    }
    //    public void AddPersion(string name)
    //    {
    //        persion = name;
    //    }

    //    public string SubPersion()
    //    {
    //        return persion;
    //    }
    //}
    //class TestInterLock
    //{
    //    static string[] names = new string[] { "小明", "小话", "小雅", "小斌", "小头" };
    //    static void Main(string[] args)
    //    {
    //        LudwigInterlocked ludwigInterlocked = new LudwigInterlocked();
    //        ThreadStart threadStart = new ThreadStart(() => {
    //            for (int i = 0; i < names.Length; i++)
    //            {
    //                ludwigInterlocked.AddPersion(names[i]);
    //                Console.WriteLine($"--{names[i]}---");

    //                Thread.Sleep(10);
    //            }

    //        });
    //        Thread thread = new Thread(threadStart);
    //        thread.Start();

    //        Thread.Sleep(10);
    //        Thread thread1 = new Thread(() => {
    //            for (int i = 0; i < names.Length; i++)
    //            {
    //                string name = ludwigInterlocked.SubPersion();
    //                if (!string.IsNullOrEmpty(name))
    //                {
    //                    Console.WriteLine(name);
    //                    Thread.Sleep(10);
    //                }
    //            }
    //        });
    //        thread1.Start();

    //        Console.Read();
    //    }
    //}

    //***************   使用InterLock的版本   *******************/
    //public class LudwigInterlocked
    //{

    //    string persion;
    //    long index;
    //    public LudwigInterlocked()
    //    {
    //        persion = "没人啊？";
    //    }
    //    public void AddPersion(string name)
    //    {
    //        while (Interlocked.Read(ref index) == 1)
    //        {
    //            Thread.Sleep(10);
    //        }
    //        Interlocked.Increment(ref index);
    //        persion = name;
    //    }

    //    public string SubPersion()
    //    {
    //        while (Interlocked.Read(ref index) == 0)
    //        {
    //            Thread.Sleep(10);
    //        }
    //        Interlocked.Decrement(ref index);
    //        return persion;
    //    }
    //}
    //class TestInterLock
    //{
    //    static string[] names = new string[] { "小明", "小话", "小雅", "小斌", "小头" };
    //    static void Main(string[] args)
    //    {
    //        LudwigInterlocked ludwigInterlocked = new LudwigInterlocked();
    //        ThreadStart threadStart = new ThreadStart(() => {
    //            for (int i = 0; i < names.Length; i++)
    //            {
    //                ludwigInterlocked.AddPersion(names[i]);
    //                Thread.Sleep(10);
    //            }
    //        });
    //        Thread thread = new Thread(threadStart);
    //        thread.Start();

    //        Thread.Sleep(10);
    //        Thread thread1 = new Thread(() => {
    //            for (int i = 0; i < names.Length; i++)
    //            {
    //                string name = ludwigInterlocked.SubPersion();
    //                if (!string.IsNullOrEmpty(name))
    //                {
    //                    Console.WriteLine(name);
    //                    Thread.Sleep(10);
    //                }
    //            }
    //        });
    //        thread1.Start();

    //        Console.Read();
    //    }
    //}

    //class TestInterLock
    //{
    //    //全局变量
    //    private static int _result;
    //    //Main方法
    //    static void Main(string[] args)
    //    {
    //        //运行后按住Enter键数秒，对比使用Interlocked.Increment(ref _result);与 _result++;的不同
    //        while (true)
    //        {
    //            Task[] _tasks = new Task[100];
    //            int i = 0;
    //            for (i = 0; i < _tasks.Length; i++)
    //            {
    //                _tasks[i] = Task.Factory.StartNew((num) =>
    //                {
    //                    var taskid = (int)num;
    //                    Work(taskid);
    //                }, i);
    //            }
    //            Task.WaitAll(_tasks);
    //            Console.WriteLine(_result);
    //            Console.ReadKey();
    //        }
    //    }
    //    //线程调用方法
    //    private static void Work(int TaskID)
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            _result++;
    //            //Interlocked.Increment(ref _result);
    //        }
    //    }
    //}

    ///***************  InterLock示例2 **********************、
    class TestInterLock
    {
        private int value1 = 0;
        public void TestIncrementUnSafe()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread t = new Thread(IncrementValue1);
                t.Name = "t1 " + i;
                t.Start();
            }
            Thread.Sleep(2000);
            //value maybe 500000
            Console.WriteLine("value1 = " + value1);
        }
        private void IncrementValue1()
        {
            for (int i = 0; i < 1000000; i++)
            {
                value1++;
            }
        }

        private int value2 = 0;
        public void TestIncrementSafe()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread t = new Thread(IncrementValue2);
                t.Name = "t2 " + i;
                t.Start();
            }
            Thread.Sleep(2000);
            //value should be 500000
            Console.WriteLine("value2 = " + value2);
        }

        private void IncrementValue2()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Interlocked.Increment(ref value2);
            }
        }

        private int value3 = 0;
        public void TestExchangeSafe()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread t = new Thread(ExchangeValue3);
                t.Name = "t2 " + i;
                t.Start();
            }
            Thread.Sleep(2000);
            //value should be 83
            Console.WriteLine("value3 = " + value3);
        }
        private void ExchangeValue3()
        {
            Interlocked.Exchange(ref value3, 83); //交换：把值2(83)赋给值1(value3)；返回新值
        }


        private int value4 = 0;
        public void TestCompareExchangeSafe()
        {
            for (int i = 0; i < 30; i++)
            {
                Thread t = new Thread(ExchangeValue3);
                t.Name = "t2 " + i;
                t.Start();
            }
            Thread.Sleep(2000);
            //value should be 99 or 0
            Console.WriteLine("value4 = " + value4);
        }
        private void ExchangeValue4()
        {
            //if value4=0, set value4=99
            Interlocked.CompareExchange(ref value4, 99, 0); //实现比较和交换两种功能：值1和值3比较，如果相同，把值2给值1，不相同则不作任何操作；返回原值（多用于判断条件
            //if(value4 == 0) { value4 = 99; }

        }

        static void Main(string[] args)
        {
            TestInterLock til = new TestInterLock();
            //运行后按Enter来继续执行
            //til.TestIncrementUnSafe(); //每次不一样
            til.TestIncrementSafe(); //每次运行结果一样

            //til.TestExchangeSafe();
            //til.TestCompareExchangeSafe();

            Console.ReadKey();
            //Console.ReadLine();
        }
    }
    
}

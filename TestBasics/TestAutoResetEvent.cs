using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestBasics
{
    class TestAutoResetEvent
    {
        private static AutoResetEvent myResetEvent = new AutoResetEvent(false);

        private static int number;

        static void MyReadThreadProc()
        {
            while (true)
            {
                myResetEvent.WaitOne(); //阻止当前线程，直到当前 WaitHandle 收到信号。  等待接收信号然后读出.  
                Console.WriteLine($"{Thread.CurrentThread.Name}读到的值是:{number}");
            }
        }

        static void Main()
        {
            Thread myReadThread = new Thread(new ThreadStart(MyReadThreadProc));
            myReadThread.Name = "读线程(子线程)";
            myReadThread.Start();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"写线程写的是{i}");
                number = i;
                myResetEvent.Set();  //将事件状态设置为终止状态，允许一个或多个等待线程继续
                Thread.Sleep(1);
            }

            Console.ReadKey();
        }

    }



    ////测试waitAny和waitAll
    //class TestAutoResetEvent
    //{
    //    private AutoResetEvent[] autoResetEvent;

    //    public TestAutoResetEvent()
    //    {
    //        autoResetEvent = new AutoResetEvent[]
    //        {
    //            new AutoResetEvent(false),
    //            new AutoResetEvent(false),
    //            new AutoResetEvent(false),
    //        };
    //    }


    //    / 定义线程处理事件
    //    public void GetCar()
    //    {
    //        Console.WriteLine("我捡到宝马了");
    //        autoResetEvent[0].Set();
    //        Console.WriteLine("我捡到宝马后的处理....");
    //    }

    //    public void GetMoney()
    //    {
    //        Console.WriteLine("我赚到钱啦");
    //        autoResetEvent[1].Set();
    //        Console.WriteLine("我赚到钱后的处理....");
    //    }

    //    public void GetWife()
    //    {
    //        Console.WriteLine("我骗到老婆啦");
    //        autoResetEvent[2].Set();
    //        Console.WriteLine("我骗到老婆后的处理....");
    //    }

    //    static void Main(string[] args)
    //    {
    //        / 设置三个线程
    //        TestAutoResetEvent p = new TestAutoResetEvent();
    //        Thread threadA = new Thread(new ThreadStart(p.GetCar));
    //        threadA.Start();

    //        Thread threadB = new Thread(new ThreadStart(p.GetMoney));
    //        threadB.Start();

    //        Thread threadC = new Thread(new ThreadStart(p.GetWife));
    //        threadC.Start();

    //        AutoResetEvent.WaitAll(p.autoResetEvent);
    //        Console.WriteLine("生活如此美好");


    //        Console.ReadLine();
    //    }
    //}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestLog4Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AAAA");

            //Debug.LogInfo("1.服务器启动监听{0}成功", "Nicholas");
            Debug.Log("1.服务器启动监听成功");
            Debug.LogInfo("2.服务器启动监听成功");
            Debug.LogWarn("3.服务器启动监听成功");
            Debug.LogError("4.服务器启动监听成功");
            Debug.LogFatle("5.服务器启动监听成功");

            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Thread.Sleep(500);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Thread.Sleep(100);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Thread.Sleep(200);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Console.WriteLine(DateTime.UtcNow.Millisecond);
            Thread.Sleep(10);


            Console.ReadKey();
        }
    }
}

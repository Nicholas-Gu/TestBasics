using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
//using Newtonsoft.Json;

namespace CloudServerMonitor
{
  
    class Program
    {
        //const string jsonFileName = "config.json";

        static void Main(string[] args)
        {
            ///默认值
            string processName = "viBot_Cloud"; //进程名
            int reqIntervals = 10000; //ms
            int startCount = 2;

            int len = args.Length;
            if (len >= 2)
            {
                if (!string.IsNullOrEmpty(args[0]))
                {
                    processName = args[0];
                }
                if (!string.IsNullOrEmpty(args[1]))
                {
                    int newReq;
                    if(int.TryParse(args[1], out newReq))
                    {
                        reqIntervals = newReq;
                    }
                    else
                    {
                        Console.WriteLine("传参错误：第二个参数必须是数值或数字………………");
                    }
                }
                Console.WriteLine($"Main传参设置后：processName={processName}，reqIntervals={reqIntervals}");
            }
            else
            {
                Console.WriteLine("注：监控程序启动时并没有添加启动参数,所以使用默认的………………");
            }
           
            ////已切换成使用Main方法的args[]来传参
            //Config _config = null;
            //try
            //{
            //    string jsonStr = File.ReadAllText(jsonFileName);
            //    _config = JsonConvert.DeserializeObject<Config>(jsonStr);
            //    processName = _config.processName;
            //    reqIntervals = _config.reqIntervals;
            //    Console.WriteLine($"process:{processName},reqIntervals:{reqIntervals}");
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine($"{GetCurrentTimeStr()}解释json配置文件失败:{e.Message}");
            //}


            string logPath = $"{Environment.CurrentDirectory.ToString()}/log.txt";
            if(!File.Exists(logPath))
            {
                File.Create(logPath);
            }
            StreamWriter sw = new StreamWriter(logPath,true);
            Console.SetOut(sw);
            Process[] processes = Process.GetProcessesByName(processName);//因为可以同时启动多个进程，所以返回值是数组。
            Console.WriteLine($"-------------------启动监控程序：{GetCurrentTimeStr()}， 当前有{processes.Length}个{processName}进程-------------------------");


            List<Process> currTitleList = new List<Process>();
            List<Process> lastTitleList = new List<Process>();

            while (true)
            {
                currTitleList.Clear();
                processes = Process.GetProcessesByName(processName);
                currTitleList.AddRange(processes);

                //被关闭了的进程
                var exitProcEnum = lastTitleList.Where(a => !currTitleList.Exists(t => a.Id==t.Id)).ToList();
                foreach (var item in exitProcEnum)
                {
                    //Console.WriteLine($"/*{item.ExitTime.ToString("yyyy-MM-dd HH:mm:ss:fff")}*/ 进程{item.MainWindowTitle}被关闭");
                    Console.WriteLine($"{GetCurrentTimeStr()} 进程{item.MainWindowTitle}被关闭");
                }

                //新打开/增加的进程
                var newAddProcEnum = currTitleList.Where(a => !lastTitleList.Exists(t => a.Id == t.Id)).ToList();
                foreach (var item in newAddProcEnum)
                {
                    Console.WriteLine($"{item.StartTime.ToString("yyyy-MM-dd HH:mm:ss:fff")} 打开进程：{item.MainWindowTitle}"); //注：此处打印的有可能是窗囗标题还没修改的
                }

                int count = processes.Length;
                if (count > 0)
                {
                    Console.WriteLine($"{GetCurrentTimeStr()} 当前有{count}个{processName}进程");

                    if (count < startCount)
                    {

                    }

                    for (int i = 0; i < count; i++)
                    {
                        if ((!processes[i].HasExited) && (!processes[i].Responding))
                        {
                            Console.WriteLine("－－－-->进入无反应的进程处理..........................");
                            Console.WriteLine($"{GetCurrentTimeStr()} {processes[i].MainWindowTitle}进程用户界面无反应，准备Kill掉自己…………");
                            try
                            {
                                string procFileName = processes[i].MainModule.FileName;
                                string procName = processes[i].MainWindowTitle;

                                processes[i].Kill();                                
                                Console.WriteLine($"{GetCurrentTimeStr()} {procName}进程已被Kill，准备重新启动一个新的…………");
                                Thread.Sleep(3000);
                                Process newProc = Process.Start(procFileName);
                                Console.WriteLine($"{GetCurrentTimeStr()} {newProc.MainWindowTitle}进程启动成功…………");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            Console.WriteLine("－－－-->退出无反应的进程处理..........................");
                        }
                    }
                }

                lastTitleList.Clear();
                lastTitleList.AddRange(currTitleList);

                sw.Flush();
                Thread.Sleep(reqIntervals);
            }

           
            sw.Close();
            Console.ReadKey();
        }

        public static string GetCurrentTimeStr()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
        }
    }

    //[Serializable]
    //public class Config
    //{
    //    public string processName;
    //    public int reqIntervals;
    //    public Config() { }
    //}
}

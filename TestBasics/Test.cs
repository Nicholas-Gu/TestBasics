using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestBasics
{
    [Serializable]
    public class ZhuiYiMessage
    {
        public string cmd;
        public string taskId;
        public dynamic data;

        public ZhuiYiMessage() { }
    }

    [Serializable] //某个商品的文本，绑定了某个音色
    public class ProductTextTone
    {
        public string audioTone; //音色
        public List<ProductTextSection> contents;
    }
    [Serializable]
    public class ProductTextSection
    {
        public string textContent; //（商品的一段）文本内容
        public string ttsAudioUrl; //是一个网络音频url
        public List<ProductMotionItem> refMotions;
    }
    [Serializable]
    public class ProductMotionItem
    {
        public string motion;
        public uint refTime; //相对当前文本开始时间
    }


    //class Test
    //{
        

    //    // public static async Task Main()
    //    //public static  int Main()
    //    //{
    //    //    Console.WriteLine("AAAA");

    //    //    //TestLanguageBasics();

    //    //    //TestDynamicObject();

    //    //    //TestLinkQ();

    //    //    // await DisplayPrimesCountAsync();

    //    //    Console.ReadKey();
    //    //    return 0;


    //    //}

    //    static void TestLanguageBasics()
    //    {
    //        bool res1 = false;
    //        bool res2 = true;
    //        bool res3 = res1 & res2;
    //        Console.WriteLine(res3);
    //        Console.WriteLine(UInt16.MaxValue);
    //        Console.WriteLine(UInt32.MaxValue);
    //        Console.WriteLine(UInt64.MaxValue);
    //        Console.WriteLine(uint.MaxValue);
    //        Console.WriteLine(Convert.ToInt32(3.14f));
    //        Console.WriteLine(Convert.ToInt32(3.46f));
    //        Console.WriteLine(Convert.ToInt32(3.5f));
    //    }

    //    static void TestDynamicObject()
    //    {
    //        //动态对象１
    //        dynamic content1 = @"[
    //                            {name:'cjt',age:10},
    //                            {name:'cjt2',age:21},
    //                            {name:'cjtc',age:16}
    //                        ]";
    //        var res = JsonConvert.DeserializeObject<dynamic>(content1);
    //        Console.WriteLine(res[0].name);

    //        //动态对象２
    //        ZhuiYiMessage msg = new ZhuiYiMessage
    //        {
    //            cmd = "111",
    //            taskId = "111",
    //            data = "{\"liveId\":\"直播Id_1\"," +
    //             "{\"showProductName\":true," +
    //             "{\"productName\":\"完美日记眼影\"," +
    //             "{\"showPrice\":true," +
    //             "{\"originPrice\":766.23," +
    //             "{\"discountPrice\":556.99，" +
    //             "{\"productMedias\":[\"aaa.jpg\",\"D:/GMF/video.mp4\"]," +
    //             "{\"imageStayTime\":3," +
    //            "\"ttsAudioList\":[{\"audio\":\"inneraaval\",\"url\":\"http:\\\\aa.wav\"},{\"audio\":\"inneraaval2\",\"url\":\"http:\\\\aa2.wav\"}]}"
    //        };
    //        if (SerializeProductParamsToFile(msg, "out.data"))
    //        {
    //            Console.WriteLine("把msg写到out.data文件成功");
    //        }

    //        foreach (var item in msg.data.ttsAudioList)
    //        {
    //            string auTone = item.Value<string>("audio");
    //            string url = item.Value<string>("url");
    //            Console.WriteLine($"{auTone }->{url}");

    //        }
    //    }

    //    static void TestLinkQ()
    //    {
    //        string[] arrays = { "asd", "abc", "bbb", "ccc" };
    //        //var results = arrays.Select(a => a); //a=>a表示没有条件，返回所有对象
    //        //var results = arrays.Select(a => a.Contains("b")); //加条件查询时，返回bool型结果
    //        var results = arrays.Where(a => a.Contains("b")); //加条件查询时，返回对象
    //        foreach (var da in results)
    //        {
    //            Console.WriteLine(da);
    //        }
    //    }

    //    public static bool SerializeProductParamsToFile(ZhuiYiMessage save, string saveFilePath)
    //    {
    //        ///4.二进制保存到文件
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream fileStream = null;
    //        try
    //        {
    //            fileStream = File.Create(saveFilePath);
    //            bf.Serialize(fileStream, save);
    //            fileStream.Close();
    //            Console.WriteLine($"写入商品数据成功:{saveFilePath}");
    //            return true;
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine($"创建文件或序列化错误,e:{e.Message}");
    //            return false;
    //        }
    //        finally
    //        {
    //            if (fileStream != null) fileStream.Close();
    //        }
    //    }

    //    static async Task DisplayPrimesCountAsync()
    //    {
    //        int result = await GetPrimesCountAsync(2, 1000000);
    //        Console.WriteLine(result);
    //    }

    //    static Task<int> GetPrimesCountAsync(int start, int count)
    //    {
    //        return Task.Run(() =>
    //            ParallelEnumerable.Range(start, count).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
    //    }

    //}
}

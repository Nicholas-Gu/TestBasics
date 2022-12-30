using CefSharp;
using CefSharp.SchemeHandler;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestEmbbedExe
{
    /// <summary>
    /// 注意：该方法只适用于com的exe（如word,Excel之类），.net的编的exe就不能用这用方法嵌入到窗体中。
    /// </summary>
    public partial class Form1 : Form
    {
        public ExeToWinform fr = null;

        public Form1()
        {
            InitializeComponent();

            CefSettings setting = new CefSettings();

            setting.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "localfolder",
                DomainName = "cefsharp",
                SchemeHandlerFactory = new FolderSchemeHandlerFactory(rootFolder: @"..\..\..\html",
                            hostName: "cefsharp", //Optional param no hostname/domain checking if null
                            defaultPage: "index.html") //Optional param will default to index.html
            });

            CefSharp.Cef.Initialize(setting);

            //String page = string.Format(@"{0}\..\..\..\html\index.html", Application.StartupPath);
            String page =  @"..\..\..\html\index.html";
            Console.WriteLine(page);
            //实例化控件
            //ChromiumWebBrowser wb = new ChromiumWebBrowser("http://www.baidu.com");
            ChromiumWebBrowser chromeBrowser = new ChromiumWebBrowser();
            //设置停靠方式
            chromeBrowser.Dock = DockStyle.Fill;

            //加入到当前窗体中
            this.Controls.Add(chromeBrowser);

            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chromeBrowser.BrowserSettings = browserSettings;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fr != null)
            {
                panel1.Visible = !panel1.Visible;
            }
            else
            {

                OpenFileDialog Oppf = new OpenFileDialog();
                Oppf.ShowDialog();
                if (Oppf.FileName != "")
                {
                    panel1.Controls.Clear();
                    fr = new ExeToWinform(panel1, "");
                    fr.Start(Oppf.FileName);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (fr != null)
            {
                panel1.Visible = !panel1.Visible;
            }
            else
            {

                OpenFileDialog Oppf = new OpenFileDialog();
                Oppf.ShowDialog();
                if (Oppf.FileName != "")
                {
                    panel1.Controls.Clear();
                    fr = new ExeToWinform(panel1, "");
                    fr.Start(Oppf.FileName);
                }
            }
        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(fr!=null)
                fr.Stop();
        }

        //    private void Form1_Resize(object sender, EventArgs e)
        //    {
        //        if (this.appWin != IntPtr.Zero)
        //        {
        //            MoveWindow(appWin, 0, 0, this.Width, this.Height, true);
        //        }
        //        //base.OnResize(e);
        //    }

        //    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        //    {
        //        try
        //        {
        //            process.Kill();
        //        }
        //        catch { }
        //    }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp
{
    public partial class Form1 : Form
    {
        public UnityControl uc = null;

        public Form1()
        {
            InitializeComponent();

            //fr = new ExeToWinform(panel1, "");
            //fr.Start(Oppf.FileName);
            uc = new UnityControl();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void unityControl1_Load(object sender, EventArgs e)
        {

        }
    }
}

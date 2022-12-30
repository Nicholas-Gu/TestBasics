namespace MainExe
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.applicationControl1 = new AppControl.ApplicationControl();
            this.SuspendLayout();
            // 
            // applicationControl1
            // 
            this.applicationControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.applicationControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.applicationControl1.ExeName = "notepad";
            //this.applicationControl1.ExeName = @"D:\\GMF\\doc\\数字人_PC_0831\\viBot.exe";
            this.applicationControl1.ExeName = @"D:\\GMF\\doc\\others\\模型3\\viBot.exe";
            this.applicationControl1.Location = new System.Drawing.Point(48, 39);
            this.applicationControl1.Name = "applicationControl1";
            this.applicationControl1.Size = new System.Drawing.Size(188, 159);
            this.applicationControl1.TabIndex = 0;
            this.applicationControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.applicationControl1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(10, 21);
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.applicationControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
    }
}


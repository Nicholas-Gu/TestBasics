﻿namespace MainApp
{
    partial class UnityControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.unityHWNDLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.unityHWNDLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 900);
            this.panel1.TabIndex = 0;
            // 
            // unityHWNDLabel
            // 
            this.unityHWNDLabel.AutoSize = true;
            this.unityHWNDLabel.Location = new System.Drawing.Point(6, 6);
            this.unityHWNDLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.unityHWNDLabel.Name = "unityHWNDLabel";
            this.unityHWNDLabel.Size = new System.Drawing.Size(62, 18);
            this.unityHWNDLabel.TabIndex = 0;
            this.unityHWNDLabel.Text = "label1";
            // 
            // UnityControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UnityControl";
            this.Size = new System.Drawing.Size(900, 900);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label unityHWNDLabel;

    }
}


namespace ArcSoftFace.Chart
{
    partial class FPieChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiPieChart1 = new Sunny.UI.UIPieChart();
            this.uiPieChart2 = new Sunny.UI.UIPieChart();
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiLine2 = new Sunny.UI.UILine();
            this.uiPieChart3 = new Sunny.UI.UIPieChart();
            this.uiLine3 = new Sunny.UI.UILine();
            this.btnflash = new Sunny.UI.UISymbolButton();
            this.SuspendLayout();
            // 
            // uiPieChart1
            // 
            this.uiPieChart1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.uiPieChart1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPieChart1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uiPieChart1.Location = new System.Drawing.Point(12, 47);
            this.uiPieChart1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPieChart1.Name = "uiPieChart1";
            this.uiPieChart1.Size = new System.Drawing.Size(400, 300);
            this.uiPieChart1.TabIndex = 0;
            this.uiPieChart1.Text = "uiPieChart1";
            // 
            // uiPieChart2
            // 
            this.uiPieChart2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.uiPieChart2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPieChart2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uiPieChart2.Location = new System.Drawing.Point(430, 47);
            this.uiPieChart2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPieChart2.Name = "uiPieChart2";
            this.uiPieChart2.Size = new System.Drawing.Size(400, 300);
            this.uiPieChart2.TabIndex = 1;
            this.uiPieChart2.Text = "uiPieChart2";
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine1.Location = new System.Drawing.Point(12, 12);
            this.uiLine1.MinimumSize = new System.Drawing.Size(2, 2);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(408, 29);
            this.uiLine1.TabIndex = 2;
            this.uiLine1.Text = "年龄段";
            // 
            // uiLine2
            // 
            this.uiLine2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine2.Location = new System.Drawing.Point(418, 12);
            this.uiLine2.MinimumSize = new System.Drawing.Size(2, 2);
            this.uiLine2.Name = "uiLine2";
            this.uiLine2.Size = new System.Drawing.Size(412, 29);
            this.uiLine2.TabIndex = 3;
            this.uiLine2.Text = "性别";
            // 
            // uiPieChart3
            // 
            this.uiPieChart3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.uiPieChart3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPieChart3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uiPieChart3.Location = new System.Drawing.Point(16, 388);
            this.uiPieChart3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPieChart3.Name = "uiPieChart3";
            this.uiPieChart3.Size = new System.Drawing.Size(814, 282);
            this.uiPieChart3.TabIndex = 4;
            this.uiPieChart3.Text = "uiPieChart3";
            // 
            // uiLine3
            // 
            this.uiLine3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine3.Location = new System.Drawing.Point(27, 353);
            this.uiLine3.MinimumSize = new System.Drawing.Size(2, 2);
            this.uiLine3.Name = "uiLine3";
            this.uiLine3.Size = new System.Drawing.Size(803, 29);
            this.uiLine3.TabIndex = 5;
            this.uiLine3.Text = "近一周不同年龄段消费次数";
            // 
            // btnflash
            // 
            this.btnflash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnflash.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnflash.Location = new System.Drawing.Point(836, 12);
            this.btnflash.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnflash.Name = "btnflash";
            this.btnflash.Size = new System.Drawing.Size(40, 35);
            this.btnflash.Symbol = 61473;
            this.btnflash.TabIndex = 6;
            this.btnflash.Click += new System.EventHandler(this.btnflash_Click);
            // 
            // FPieChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 693);
            this.Controls.Add(this.btnflash);
            this.Controls.Add(this.uiLine3);
            this.Controls.Add(this.uiPieChart3);
            this.Controls.Add(this.uiLine2);
            this.Controls.Add(this.uiLine1);
            this.Controls.Add(this.uiPieChart2);
            this.Controls.Add(this.uiPieChart1);
            this.Name = "FPieChart";
            this.Text = "具体分析";
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPieChart uiPieChart1;
        private Sunny.UI.UIPieChart uiPieChart2;
        private Sunny.UI.UILine uiLine1;
        private Sunny.UI.UILine uiLine2;
        private Sunny.UI.UIPieChart uiPieChart3;
        private Sunny.UI.UILine uiLine3;
        private Sunny.UI.UISymbolButton btnflash;
    }
}
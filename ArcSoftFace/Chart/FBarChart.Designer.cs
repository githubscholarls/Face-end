using Sunny.UI;

namespace FaceUI.Chart
{
    partial class FBarChart
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
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.uiImageButton3 = new Sunny.UI.UIImageButton();
            this.uiImageButton2 = new Sunny.UI.UIImageButton();
            this.uiImageButton1 = new Sunny.UI.UIImageButton();
            this.BarChart = new Sunny.UI.UIBarChart();
            this.PagePanel = new Sunny.UI.UIPanel();
            this.cbbChangeDate = new Sunny.UI.UIComboBox();
            this.uiSymbolButton2 = new Sunny.UI.UISymbolButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton1)).BeginInit();
            this.PagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton1.Location = new System.Drawing.Point(622, 438);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolButton1.Size = new System.Drawing.Size(100, 27);
            this.uiSymbolButton1.Symbol = 61473;
            this.uiSymbolButton1.TabIndex = 28;
            this.uiSymbolButton1.Text = "更新";
            this.uiSymbolButton1.Click += new System.EventHandler(this.uiSymbolButton1_Click);
            // 
            // uiImageButton3
            // 
            this.uiImageButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiImageButton3.Location = new System.Drawing.Point(242, 466);
            this.uiImageButton3.Name = "uiImageButton3";
            this.uiImageButton3.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton3.TabIndex = 27;
            this.uiImageButton3.TabStop = false;
            this.uiImageButton3.Text = "      Dark";
            this.uiImageButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton3.Visible = false;
            this.uiImageButton3.Click += new System.EventHandler(this.uiImageButton3_Click);
            // 
            // uiImageButton2
            // 
            this.uiImageButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiImageButton2.Location = new System.Drawing.Point(136, 466);
            this.uiImageButton2.Name = "uiImageButton2";
            this.uiImageButton2.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton2.TabIndex = 26;
            this.uiImageButton2.TabStop = false;
            this.uiImageButton2.Text = "      Plain";
            this.uiImageButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton2.Visible = false;
            this.uiImageButton2.Click += new System.EventHandler(this.uiImageButton2_Click);
            // 
            // uiImageButton1
            // 
            this.uiImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiImageButton1.Location = new System.Drawing.Point(30, 466);
            this.uiImageButton1.Name = "uiImageButton1";
            this.uiImageButton1.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton1.TabIndex = 25;
            this.uiImageButton1.TabStop = false;
            this.uiImageButton1.Text = "      Default";
            this.uiImageButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton1.Visible = false;
            this.uiImageButton1.Click += new System.EventHandler(this.uiImageButton1_Click);
            // 
            // BarChart
            // 
            this.BarChart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.BarChart.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.BarChart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.BarChart.Location = new System.Drawing.Point(30, 48);
            this.BarChart.MinimumSize = new System.Drawing.Size(1, 1);
            this.BarChart.Name = "BarChart";
            this.BarChart.Size = new System.Drawing.Size(692, 384);
            this.BarChart.TabIndex = 0;
            this.BarChart.Text = "uiBarChart1";
            // 
            // PagePanel
            // 
            this.PagePanel.Controls.Add(this.cbbChangeDate);
            this.PagePanel.Controls.Add(this.uiSymbolButton2);
            this.PagePanel.Controls.Add(this.uiSymbolButton1);
            this.PagePanel.Controls.Add(this.uiImageButton3);
            this.PagePanel.Controls.Add(this.uiImageButton2);
            this.PagePanel.Controls.Add(this.uiImageButton1);
            this.PagePanel.Controls.Add(this.BarChart);
            this.PagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PagePanel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.PagePanel.Location = new System.Drawing.Point(0, 0);
            this.PagePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PagePanel.MinimumSize = new System.Drawing.Size(1, 1);
            this.PagePanel.Name = "PagePanel";
            this.PagePanel.Size = new System.Drawing.Size(800, 563);
            this.PagePanel.TabIndex = 0;
            this.PagePanel.Text = "uiPanel1";
            // 
            // cbbChangeDate
            // 
            this.cbbChangeDate.FillColor = System.Drawing.Color.White;
            this.cbbChangeDate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbChangeDate.Items.AddRange(new object[] {
            "近一周",
            "月",
            "季度",
            "年"});
            this.cbbChangeDate.Location = new System.Drawing.Point(495, 434);
            this.cbbChangeDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbChangeDate.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbbChangeDate.Name = "cbbChangeDate";
            this.cbbChangeDate.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbbChangeDate.Size = new System.Drawing.Size(120, 35);
            this.cbbChangeDate.TabIndex = 31;
            this.cbbChangeDate.Text = "近一周";
            this.cbbChangeDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbbChangeDate.SelectedIndexChanged += new System.EventHandler(this.cbbChangeDate_SelectedIndexChanged);
            // 
            // uiSymbolButton2
            // 
            this.uiSymbolButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton2.Location = new System.Drawing.Point(516, 438);
            this.uiSymbolButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton2.Name = "uiSymbolButton2";
            this.uiSymbolButton2.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolButton2.Size = new System.Drawing.Size(100, 27);
            this.uiSymbolButton2.Symbol = 61952;
            this.uiSymbolButton2.TabIndex = 29;
            this.uiSymbolButton2.Text = "主题";
            this.uiSymbolButton2.Visible = false;
            this.uiSymbolButton2.Click += new System.EventHandler(this.uiSymbolButton2_Click);
            // 
            // FBarChart
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 563);
            this.Controls.Add(this.PagePanel);
            this.Name = "FBarChart";
            this.Symbol = 61568;
            this.Text = "客流量分析";
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton1)).EndInit();
            this.PagePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private UIBarChart BarChart;
        private UISymbolButton uiSymbolButton1;
        private UIImageButton uiImageButton3;
        private UIImageButton uiImageButton2;
        private UIImageButton uiImageButton1;
        private UIPanel PagePanel;
        private UISymbolButton uiSymbolButton2;
        private UIComboBox cbbChangeDate;
    }
}
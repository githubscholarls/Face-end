
namespace ArcSoftFace.Chart
{
    partial class SystemService
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
            this.components = new System.ComponentModel.Container();
            this.imageLists = new System.Windows.Forms.ImageList(this.components);
            this.btnImgRegister = new Sunny.UI.UIButton();
            this.btnStartVideo = new Sunny.UI.UIButton();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.btnClearFaceList = new Sunny.UI.UIButton();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.uiTabControl1 = new Sunny.UI.UITabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.btnFlash = new Sunny.UI.UISymbolButton();
            this.btnJCRS = new Sunny.UI.UIButton();
            this.btnDDS = new Sunny.UI.UIButton();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.btnMoney = new Sunny.UI.UISymbolButton();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnTimeSave = new Sunny.UI.UIButton();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.fen = new Sunny.UI.UIIntegerUpDown();
            this.labExpire = new Sunny.UI.UISymbolLabel();
            this.shi = new Sunny.UI.UIIntegerUpDown();
            this.uiToolTip1 = new Sunny.UI.UIToolTip(this.components);
            this.uiPanel1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.uiTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.uiPanel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageLists
            // 
            this.imageLists.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageLists.ImageSize = new System.Drawing.Size(16, 16);
            this.imageLists.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnImgRegister
            // 
            this.btnImgRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImgRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImgRegister.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnImgRegister.Location = new System.Drawing.Point(6, 536);
            this.btnImgRegister.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnImgRegister.Name = "btnImgRegister";
            this.btnImgRegister.Size = new System.Drawing.Size(140, 35);
            this.btnImgRegister.TabIndex = 1;
            this.btnImgRegister.Text = "人脸注册";
            this.btnImgRegister.Click += new System.EventHandler(this.btnImgRegister_Click);
            // 
            // btnStartVideo
            // 
            this.btnStartVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartVideo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartVideo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnStartVideo.Location = new System.Drawing.Point(12, 12);
            this.btnStartVideo.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnStartVideo.Name = "btnStartVideo";
            this.btnStartVideo.Size = new System.Drawing.Size(140, 35);
            this.btnStartVideo.TabIndex = 43;
            this.btnStartVideo.Text = "启动摄像头";
            this.btnStartVideo.Click += new System.EventHandler(this.btnStartVideo_Click);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.btnClearFaceList);
            this.uiPanel1.Controls.Add(this.btnStartVideo);
            this.uiPanel1.Controls.Add(this.btnImgRegister);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(1005, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(165, 646);
            this.uiPanel1.TabIndex = 45;
            this.uiPanel1.Text = null;
            // 
            // btnClearFaceList
            // 
            this.btnClearFaceList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFaceList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearFaceList.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnClearFaceList.Location = new System.Drawing.Point(7, 600);
            this.btnClearFaceList.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnClearFaceList.Name = "btnClearFaceList";
            this.btnClearFaceList.Size = new System.Drawing.Size(140, 34);
            this.btnClearFaceList.TabIndex = 47;
            this.btnClearFaceList.Text = "清空人脸库";
            this.btnClearFaceList.Click += new System.EventHandler(this.btnClearFaceList_Click);
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.uiTabControl1);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPanel2.Location = new System.Drawing.Point(0, 0);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(1005, 646);
            this.uiPanel2.TabIndex = 46;
            this.uiPanel2.Text = null;
            // 
            // uiTabControl1
            // 
            this.uiTabControl1.Controls.Add(this.tabPage1);
            this.uiTabControl1.Controls.Add(this.tabPage2);
            this.uiTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.uiTabControl1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiTabControl1.ItemSize = new System.Drawing.Size(150, 40);
            this.uiTabControl1.Location = new System.Drawing.Point(0, 0);
            this.uiTabControl1.MainPage = "";
            this.uiTabControl1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiTabControl1.Name = "uiTabControl1";
            this.uiTabControl1.SelectedIndex = 0;
            this.uiTabControl1.Size = new System.Drawing.Size(1005, 646);
            this.uiTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.uiTabControl1.TabBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.uiTabControl1.TabIndex = 0;
            this.uiTabControl1.TabSelectedColor = System.Drawing.Color.White;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(232)))));
            this.tabPage1.Controls.Add(this.uiPanel3);
            this.tabPage1.Location = new System.Drawing.Point(0, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1005, 606);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "实时概览";
            // 
            // uiPanel3
            // 
            this.uiPanel3.BackColor = System.Drawing.Color.White;
            this.uiPanel3.Controls.Add(this.btnFlash);
            this.uiPanel3.Controls.Add(this.btnJCRS);
            this.uiPanel3.Controls.Add(this.btnDDS);
            this.uiPanel3.Controls.Add(this.uiLabel5);
            this.uiPanel3.Controls.Add(this.uiLabel3);
            this.uiPanel3.Controls.Add(this.btnMoney);
            this.uiPanel3.Controls.Add(this.uiLabel1);
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPanel3.Location = new System.Drawing.Point(0, 0);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.Size = new System.Drawing.Size(1005, 606);
            this.uiPanel3.TabIndex = 4;
            this.uiPanel3.Text = null;
            // 
            // btnFlash
            // 
            this.btnFlash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFlash.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnFlash.Location = new System.Drawing.Point(927, 3);
            this.btnFlash.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnFlash.Name = "btnFlash";
            this.btnFlash.Size = new System.Drawing.Size(71, 35);
            this.btnFlash.Symbol = 61666;
            this.btnFlash.TabIndex = 13;
            this.btnFlash.Click += new System.EventHandler(this.btnFlash_Click);
            // 
            // btnJCRS
            // 
            this.btnJCRS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJCRS.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnJCRS.Location = new System.Drawing.Point(205, 200);
            this.btnJCRS.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnJCRS.Name = "btnJCRS";
            this.btnJCRS.Size = new System.Drawing.Size(100, 35);
            this.btnJCRS.TabIndex = 12;
            this.btnJCRS.Text = "0";
            // 
            // btnDDS
            // 
            this.btnDDS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDDS.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDDS.Location = new System.Drawing.Point(44, 200);
            this.btnDDS.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnDDS.Name = "btnDDS";
            this.btnDDS.Size = new System.Drawing.Size(100, 35);
            this.btnDDS.TabIndex = 10;
            this.btnDDS.Text = "0";
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.Location = new System.Drawing.Point(201, 258);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(121, 34);
            this.uiLabel5.TabIndex = 8;
            this.uiLabel5.Text = "当天就餐人数";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(64, 258);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(121, 34);
            this.uiLabel3.TabIndex = 6;
            this.uiLabel3.Text = "订单数";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMoney
            // 
            this.btnMoney.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoney.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnMoney.Location = new System.Drawing.Point(70, 41);
            this.btnMoney.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnMoney.Name = "btnMoney";
            this.btnMoney.Size = new System.Drawing.Size(209, 64);
            this.btnMoney.Symbol = 61783;
            this.btnMoney.SymbolSize = 40;
            this.btnMoney.TabIndex = 2;
            this.btnMoney.Text = "0.00";
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(134, 118);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(121, 34);
            this.uiLabel1.TabIndex = 3;
            this.uiLabel1.Text = "实收金额";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.btnTimeSave);
            this.tabPage2.Controls.Add(this.uiLabel8);
            this.tabPage2.Controls.Add(this.uiLabel7);
            this.tabPage2.Controls.Add(this.fen);
            this.tabPage2.Controls.Add(this.labExpire);
            this.tabPage2.Controls.Add(this.shi);
            this.tabPage2.Location = new System.Drawing.Point(0, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1005, 606);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "参数";
            // 
            // btnTimeSave
            // 
            this.btnTimeSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimeSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimeSave.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnTimeSave.Location = new System.Drawing.Point(322, 559);
            this.btnTimeSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnTimeSave.Name = "btnTimeSave";
            this.btnTimeSave.Size = new System.Drawing.Size(100, 35);
            this.btnTimeSave.TabIndex = 12;
            this.btnTimeSave.Text = "保存";
            this.btnTimeSave.Click += new System.EventHandler(this.btnTimeSave_Click);
            // 
            // uiLabel8
            // 
            this.uiLabel8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel8.Location = new System.Drawing.Point(532, 32);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(38, 29);
            this.uiLabel8.TabIndex = 5;
            this.uiLabel8.Text = "分";
            this.uiLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel7
            // 
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel7.Location = new System.Drawing.Point(342, 30);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(38, 32);
            this.uiLabel7.TabIndex = 4;
            this.uiLabel7.Text = "时";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fen
            // 
            this.fen.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.fen.Location = new System.Drawing.Point(425, 33);
            this.fen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fen.Maximum = 60;
            this.fen.Minimum = 0;
            this.fen.MinimumSize = new System.Drawing.Size(100, 0);
            this.fen.Name = "fen";
            this.fen.Size = new System.Drawing.Size(100, 29);
            this.fen.TabIndex = 3;
            this.fen.Text = "uiIntegerUpDown2";
            // 
            // labExpire
            // 
            this.labExpire.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.labExpire.Location = new System.Drawing.Point(18, 29);
            this.labExpire.MinimumSize = new System.Drawing.Size(1, 1);
            this.labExpire.Name = "labExpire";
            this.labExpire.Padding = new System.Windows.Forms.Padding(34, 0, 0, 0);
            this.labExpire.Size = new System.Drawing.Size(192, 35);
            this.labExpire.Symbol = 62108;
            this.labExpire.SymbolSize = 30;
            this.labExpire.TabIndex = 2;
            this.labExpire.Text = "消费时间间隔";
            // 
            // shi
            // 
            this.shi.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.shi.Location = new System.Drawing.Point(235, 33);
            this.shi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.shi.Maximum = 5;
            this.shi.Minimum = 0;
            this.shi.MinimumSize = new System.Drawing.Size(100, 0);
            this.shi.Name = "shi";
            this.shi.Size = new System.Drawing.Size(100, 29);
            this.shi.TabIndex = 0;
            this.shi.Text = "uiIntegerUpDown1";
            // 
            // uiToolTip1
            // 
            this.uiToolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uiToolTip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.uiToolTip1.OwnerDraw = true;
            // 
            // SystemService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 646);
            this.Controls.Add(this.uiPanel2);
            this.Controls.Add(this.uiPanel1);
            this.Name = "SystemService";
            this.Text = "开始";
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.uiTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.uiPanel3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ImageList imageLists;
        private Sunny.UI.UIButton btnImgRegister;
        private Sunny.UI.UIButton btnStartVideo;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UIButton btnClearFaceList;
        private Sunny.UI.UITabControl uiTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UISymbolButton btnMoney;
        private Sunny.UI.UIPanel uiPanel3;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIButton btnDDS;
        private Sunny.UI.UIButton btnJCRS;
        private Sunny.UI.UIIntegerUpDown shi;
        private Sunny.UI.UISymbolLabel labExpire;
        private Sunny.UI.UIIntegerUpDown fen;
        private Sunny.UI.UILabel uiLabel8;
        private Sunny.UI.UILabel uiLabel7;
        private Sunny.UI.UIButton btnTimeSave;
        private Sunny.UI.UISymbolButton btnFlash;
        private Sunny.UI.UIToolTip uiToolTip1;
    }
}
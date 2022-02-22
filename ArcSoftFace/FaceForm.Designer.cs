namespace ArcSoftFace
{
    partial class FaceForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceForm));
            this.picImageCompare = new System.Windows.Forms.PictureBox();
            this.lblImageList = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.TextBox();
            this.chooseImgBtn = new System.Windows.Forms.Button();
            this.imageLists = new System.Windows.Forms.ImageList(this.components);
            this.imageList = new System.Windows.Forms.ListView();
            this.matchBtn = new System.Windows.Forms.Button();
            this.lblCompareImage = new System.Windows.Forms.Label();
            this.lblCompareInfo = new System.Windows.Forms.Label();
            this.rgbVideoSource = new AForge.Controls.VideoSourcePlayer();
            this.txtThreshold = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.irVideoSource = new AForge.Controls.VideoSourcePlayer();
            this.btnStartVideo = new Sunny.UI.UIButton();
            this.chooseMultiImgBtn = new Sunny.UI.UIButton();
            this.btnClearFaceList = new Sunny.UI.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.picImageCompare)).BeginInit();
            this.SuspendLayout();
            // 
            // picImageCompare
            // 
            this.picImageCompare.BackColor = System.Drawing.Color.White;
            this.picImageCompare.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picImageCompare.Location = new System.Drawing.Point(3, 5);
            this.picImageCompare.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picImageCompare.Name = "picImageCompare";
            this.picImageCompare.Size = new System.Drawing.Size(892, 502);
            this.picImageCompare.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picImageCompare.TabIndex = 1;
            this.picImageCompare.TabStop = false;
            // 
            // lblImageList
            // 
            this.lblImageList.AutoSize = true;
            this.lblImageList.ForeColor = System.Drawing.Color.Black;
            this.lblImageList.Location = new System.Drawing.Point(12, 532);
            this.lblImageList.Name = "lblImageList";
            this.lblImageList.Size = new System.Drawing.Size(92, 31);
            this.lblImageList.TabIndex = 7;
            this.lblImageList.Text = "人脸库:";
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.Color.White;
            this.logBox.Location = new System.Drawing.Point(30, 568);
            this.logBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(324, 41);
            this.logBox.TabIndex = 31;
            // 
            // chooseImgBtn
            // 
            this.chooseImgBtn.BackColor = System.Drawing.Color.Transparent;
            this.chooseImgBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chooseImgBtn.Location = new System.Drawing.Point(27, 613);
            this.chooseImgBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chooseImgBtn.Name = "chooseImgBtn";
            this.chooseImgBtn.Size = new System.Drawing.Size(105, 23);
            this.chooseImgBtn.TabIndex = 30;
            this.chooseImgBtn.Text = "选择识别图";
            this.chooseImgBtn.UseVisualStyleBackColor = false;
            this.chooseImgBtn.Click += new System.EventHandler(this.ChooseImg);
            // 
            // imageLists
            // 
            this.imageLists.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageLists.ImageStream")));
            this.imageLists.TransparentColor = System.Drawing.Color.Transparent;
            this.imageLists.Images.SetKeyName(0, "alai_152784032385984494.jpg");
            // 
            // imageList
            // 
            this.imageList.HideSelection = false;
            this.imageList.LargeImageList = this.imageLists;
            this.imageList.Location = new System.Drawing.Point(7, 511);
            this.imageList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imageList.Name = "imageList";
            this.imageList.Size = new System.Drawing.Size(888, 158);
            this.imageList.TabIndex = 33;
            this.imageList.UseCompatibleStateImageBehavior = false;
            this.imageList.SelectedIndexChanged += new System.EventHandler(this.imageList_SelectedIndexChanged);
            this.imageList.DoubleClick += new System.EventHandler(this.imageList_DoubleClick);
            // 
            // matchBtn
            // 
            this.matchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.matchBtn.Location = new System.Drawing.Point(137, 613);
            this.matchBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.matchBtn.Name = "matchBtn";
            this.matchBtn.Size = new System.Drawing.Size(100, 23);
            this.matchBtn.TabIndex = 34;
            this.matchBtn.Text = "开始匹配";
            this.matchBtn.UseVisualStyleBackColor = true;
            this.matchBtn.Click += new System.EventHandler(this.matchBtn_Click);
            // 
            // lblCompareImage
            // 
            this.lblCompareImage.AutoSize = true;
            this.lblCompareImage.Location = new System.Drawing.Point(8, 35);
            this.lblCompareImage.Name = "lblCompareImage";
            this.lblCompareImage.Size = new System.Drawing.Size(0, 31);
            this.lblCompareImage.TabIndex = 36;
            // 
            // lblCompareInfo
            // 
            this.lblCompareInfo.AutoSize = true;
            this.lblCompareInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCompareInfo.Location = new System.Drawing.Point(71, 35);
            this.lblCompareInfo.Name = "lblCompareInfo";
            this.lblCompareInfo.Size = new System.Drawing.Size(0, 24);
            this.lblCompareInfo.TabIndex = 37;
            // 
            // rgbVideoSource
            // 
            this.rgbVideoSource.Location = new System.Drawing.Point(3, 5);
            this.rgbVideoSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rgbVideoSource.Name = "rgbVideoSource";
            this.rgbVideoSource.Size = new System.Drawing.Size(892, 502);
            this.rgbVideoSource.TabIndex = 38;
            this.rgbVideoSource.Text = "videoSource";
            this.rgbVideoSource.VideoSource = null;
            this.rgbVideoSource.PlayingFinished += new AForge.Video.PlayingFinishedEventHandler(this.videoSource_PlayingFinished);
            this.rgbVideoSource.Paint += new System.Windows.Forms.PaintEventHandler(this.videoSource_Paint);
            // 
            // txtThreshold
            // 
            this.txtThreshold.BackColor = System.Drawing.SystemColors.Window;
            this.txtThreshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtThreshold.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtThreshold.Location = new System.Drawing.Point(292, 616);
            this.txtThreshold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Size = new System.Drawing.Size(58, 34);
            this.txtThreshold.TabIndex = 40;
            this.txtThreshold.Text = "0.8";
            this.txtThreshold.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtThreshold_KeyPress);
            this.txtThreshold.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtThreshold_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 621);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 31);
            this.label1.TabIndex = 41;
            this.label1.Text = "阈值：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // irVideoSource
            // 
            this.irVideoSource.BackColor = System.Drawing.SystemColors.Control;
            this.irVideoSource.Location = new System.Drawing.Point(598, 5);
            this.irVideoSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.irVideoSource.Name = "irVideoSource";
            this.irVideoSource.Size = new System.Drawing.Size(297, 184);
            this.irVideoSource.TabIndex = 38;
            this.irVideoSource.Text = "videoSource";
            this.irVideoSource.VideoSource = null;
            this.irVideoSource.Visible = false;
            this.irVideoSource.PlayingFinished += new AForge.Video.PlayingFinishedEventHandler(this.videoSource_PlayingFinished);
            this.irVideoSource.Paint += new System.Windows.Forms.PaintEventHandler(this.irVideoSource_Paint);
            // 
            // btnStartVideo
            // 
            this.btnStartVideo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartVideo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnStartVideo.Location = new System.Drawing.Point(717, 513);
            this.btnStartVideo.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnStartVideo.Name = "btnStartVideo";
            this.btnStartVideo.Size = new System.Drawing.Size(157, 35);
            this.btnStartVideo.Style = Sunny.UI.UIStyle.Custom;
            this.btnStartVideo.TabIndex = 42;
            this.btnStartVideo.Text = "启动";
            this.btnStartVideo.Visible = false;
            this.btnStartVideo.Click += new System.EventHandler(this.btnStartVideo_Click);
            // 
            // chooseMultiImgBtn
            // 
            this.chooseMultiImgBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chooseMultiImgBtn.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.chooseMultiImgBtn.Location = new System.Drawing.Point(717, 568);
            this.chooseMultiImgBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.chooseMultiImgBtn.Name = "chooseMultiImgBtn";
            this.chooseMultiImgBtn.Size = new System.Drawing.Size(157, 35);
            this.chooseMultiImgBtn.Style = Sunny.UI.UIStyle.Custom;
            this.chooseMultiImgBtn.TabIndex = 43;
            this.chooseMultiImgBtn.Text = "注册人脸";
            this.chooseMultiImgBtn.Visible = false;
            this.chooseMultiImgBtn.Click += new System.EventHandler(this.ChooseMultiImg);
            // 
            // btnClearFaceList
            // 
            this.btnClearFaceList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearFaceList.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnClearFaceList.Location = new System.Drawing.Point(717, 621);
            this.btnClearFaceList.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnClearFaceList.Name = "btnClearFaceList";
            this.btnClearFaceList.Size = new System.Drawing.Size(157, 34);
            this.btnClearFaceList.Style = Sunny.UI.UIStyle.Custom;
            this.btnClearFaceList.TabIndex = 44;
            this.btnClearFaceList.Text = "清空人脸库";
            this.btnClearFaceList.Visible = false;
            this.btnClearFaceList.Click += new System.EventHandler(this.btnClearFaceList_Click);
            // 
            // FaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(896, 680);
            this.Controls.Add(this.imageList);
            this.Controls.Add(this.irVideoSource);
            this.Controls.Add(this.rgbVideoSource);
            this.Controls.Add(this.picImageCompare);
            this.Controls.Add(this.btnClearFaceList);
            this.Controls.Add(this.chooseMultiImgBtn);
            this.Controls.Add(this.btnStartVideo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtThreshold);
            this.Controls.Add(this.lblCompareInfo);
            this.Controls.Add(this.lblCompareImage);
            this.Controls.Add(this.matchBtn);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.chooseImgBtn);
            this.Controls.Add(this.lblImageList);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "FaceForm";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "人脸识别";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Closed);
            ((System.ComponentModel.ISupportInitialize)(this.picImageCompare)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picImageCompare;
        private System.Windows.Forms.Label lblImageList;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button chooseImgBtn;
        /// <summary>
        /// imageList的key是根据这个字典查的
        /// </summary>
        public System.Windows.Forms.ImageList imageLists;
        public System.Windows.Forms.ListView imageList;
        private System.Windows.Forms.Button matchBtn;
        private System.Windows.Forms.Label lblCompareImage;
        private System.Windows.Forms.Label lblCompareInfo;
        private AForge.Controls.VideoSourcePlayer rgbVideoSource;
        private System.Windows.Forms.TextBox txtThreshold;
        private System.Windows.Forms.Label label1;
        private AForge.Controls.VideoSourcePlayer irVideoSource;
        private Sunny.UI.UIButton btnStartVideo;
        private Sunny.UI.UIButton chooseMultiImgBtn;
        private Sunny.UI.UIButton btnClearFaceList;
    }
}


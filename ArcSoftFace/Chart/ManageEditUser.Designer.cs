
namespace ArcSoftFace
{
    partial class ManageEditUser
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
            this.tboxAge = new Sunny.UI.UITextBox();
            this.tboxGuid = new System.Windows.Forms.TextBox();
            this.labAge = new System.Windows.Forms.Label();
            this.labSex = new System.Windows.Forms.Label();
            this.uiRadioMan = new Sunny.UI.UIRadioButton();
            this.uiRadioButtonGroup1 = new Sunny.UI.UIRadioButtonGroup();
            this.uiRadioWoman = new Sunny.UI.UIRadioButton();
            this.tboxName = new Sunny.UI.UITextBox();
            this.labGuid = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.imgbtn = new Sunny.UI.UIImageButton();
            this.tboxIphone = new Sunny.UI.UITextBox();
            this.btnPerOrder = new Sunny.UI.UISymbolButton();
            this.uiToolTip1 = new Sunny.UI.UIToolTip(this.components);
            this.labName = new Sunny.UI.UISymbolLabel();
            this.labIphone = new Sunny.UI.UISymbolLabel();
            this.uiRadioButtonGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgbtn)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.Location = new System.Drawing.Point(1, 454);
            this.pnlBtm.Size = new System.Drawing.Size(652, 55);
            // 
            // tboxAge
            // 
            this.tboxAge.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tboxAge.FillColor = System.Drawing.Color.White;
            this.tboxAge.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tboxAge.Location = new System.Drawing.Point(220, 292);
            this.tboxAge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tboxAge.Maximum = 2147483647D;
            this.tboxAge.Minimum = -2147483648D;
            this.tboxAge.MinimumSize = new System.Drawing.Size(1, 1);
            this.tboxAge.Name = "tboxAge";
            this.tboxAge.Padding = new System.Windows.Forms.Padding(5);
            this.tboxAge.Size = new System.Drawing.Size(128, 39);
            this.tboxAge.TabIndex = 20;
            this.tboxAge.Text = "0";
            this.tboxAge.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            // 
            // tboxGuid
            // 
            this.tboxGuid.Location = new System.Drawing.Point(75, 35);
            this.tboxGuid.Name = "tboxGuid";
            this.tboxGuid.Size = new System.Drawing.Size(31, 39);
            this.tboxGuid.TabIndex = 17;
            this.tboxGuid.Visible = false;
            this.tboxGuid.WordWrap = false;
            // 
            // labAge
            // 
            this.labAge.AutoSize = true;
            this.labAge.Location = new System.Drawing.Point(94, 292);
            this.labAge.Name = "labAge";
            this.labAge.Size = new System.Drawing.Size(62, 31);
            this.labAge.TabIndex = 16;
            this.labAge.Text = "年龄";
            // 
            // labSex
            // 
            this.labSex.AutoSize = true;
            this.labSex.Location = new System.Drawing.Point(94, 140);
            this.labSex.Name = "labSex";
            this.labSex.Size = new System.Drawing.Size(62, 31);
            this.labSex.TabIndex = 15;
            this.labSex.Text = "性别";
            // 
            // uiRadioMan
            // 
            this.uiRadioMan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiRadioMan.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRadioMan.Location = new System.Drawing.Point(26, 35);
            this.uiRadioMan.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRadioMan.Name = "uiRadioMan";
            this.uiRadioMan.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.uiRadioMan.Size = new System.Drawing.Size(66, 28);
            this.uiRadioMan.TabIndex = 1;
            this.uiRadioMan.Text = "男";
            // 
            // uiRadioButtonGroup1
            // 
            this.uiRadioButtonGroup1.Controls.Add(this.uiRadioMan);
            this.uiRadioButtonGroup1.Controls.Add(this.uiRadioWoman);
            this.uiRadioButtonGroup1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRadioButtonGroup1.Location = new System.Drawing.Point(219, 118);
            this.uiRadioButtonGroup1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiRadioButtonGroup1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRadioButtonGroup1.Name = "uiRadioButtonGroup1";
            this.uiRadioButtonGroup1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiRadioButtonGroup1.Size = new System.Drawing.Size(258, 76);
            this.uiRadioButtonGroup1.TabIndex = 18;
            this.uiRadioButtonGroup1.Text = null;
            // 
            // uiRadioWoman
            // 
            this.uiRadioWoman.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiRadioWoman.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRadioWoman.Location = new System.Drawing.Point(157, 34);
            this.uiRadioWoman.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRadioWoman.Name = "uiRadioWoman";
            this.uiRadioWoman.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.uiRadioWoman.Size = new System.Drawing.Size(62, 29);
            this.uiRadioWoman.TabIndex = 0;
            this.uiRadioWoman.Text = "女";
            // 
            // tboxName
            // 
            this.tboxName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tboxName.FillColor = System.Drawing.Color.White;
            this.tboxName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tboxName.Location = new System.Drawing.Point(220, 69);
            this.tboxName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tboxName.Maximum = 2147483647D;
            this.tboxName.Minimum = -2147483648D;
            this.tboxName.MinimumSize = new System.Drawing.Size(1, 1);
            this.tboxName.Name = "tboxName";
            this.tboxName.Padding = new System.Windows.Forms.Padding(5);
            this.tboxName.Size = new System.Drawing.Size(258, 39);
            this.tboxName.TabIndex = 19;
            // 
            // labGuid
            // 
            this.labGuid.AutoSize = true;
            this.labGuid.Location = new System.Drawing.Point(4, 35);
            this.labGuid.Name = "labGuid";
            this.labGuid.Size = new System.Drawing.Size(65, 31);
            this.labGuid.TabIndex = 21;
            this.labGuid.Text = "guid";
            this.labGuid.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 365);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 31);
            this.label1.TabIndex = 22;
            this.label1.Text = "照片";
            // 
            // imgbtn
            // 
            this.imgbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgbtn.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.imgbtn.Location = new System.Drawing.Point(220, 352);
            this.imgbtn.Name = "imgbtn";
            this.imgbtn.Size = new System.Drawing.Size(112, 94);
            this.imgbtn.TabIndex = 23;
            this.imgbtn.TabStop = false;
            this.imgbtn.Text = null;
            this.imgbtn.Click += new System.EventHandler(this.uiImageButton1_Click);
            // 
            // tboxIphone
            // 
            this.tboxIphone.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tboxIphone.FillColor = System.Drawing.Color.White;
            this.tboxIphone.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tboxIphone.Location = new System.Drawing.Point(219, 222);
            this.tboxIphone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tboxIphone.Maximum = 2147483647D;
            this.tboxIphone.Minimum = -2147483648D;
            this.tboxIphone.MinimumSize = new System.Drawing.Size(1, 1);
            this.tboxIphone.Name = "tboxIphone";
            this.tboxIphone.Padding = new System.Windows.Forms.Padding(5);
            this.tboxIphone.Size = new System.Drawing.Size(258, 39);
            this.tboxIphone.TabIndex = 20;
            // 
            // btnPerOrder
            // 
            this.btnPerOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPerOrder.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnPerOrder.Location = new System.Drawing.Point(377, 292);
            this.btnPerOrder.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnPerOrder.Name = "btnPerOrder";
            this.btnPerOrder.Size = new System.Drawing.Size(101, 35);
            this.btnPerOrder.Symbol = 57574;
            this.btnPerOrder.TabIndex = 26;
            this.btnPerOrder.Text = "订单";
            this.btnPerOrder.Click += new System.EventHandler(this.btnPerOrder_Click);
            // 
            // uiToolTip1
            // 
            this.uiToolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uiToolTip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.uiToolTip1.OwnerDraw = true;
            // 
            // labName
            // 
            this.labName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.labName.Location = new System.Drawing.Point(39, 69);
            this.labName.MinimumSize = new System.Drawing.Size(1, 1);
            this.labName.Name = "labName";
            this.labName.Padding = new System.Windows.Forms.Padding(34, 0, 0, 0);
            this.labName.Size = new System.Drawing.Size(137, 35);
            this.labName.Symbol = 62108;
            this.labName.SymbolSize = 30;
            this.labName.TabIndex = 29;
            this.labName.Text = "姓名";
            // 
            // labIphone
            // 
            this.labIphone.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.labIphone.Location = new System.Drawing.Point(62, 222);
            this.labIphone.MinimumSize = new System.Drawing.Size(1, 1);
            this.labIphone.Name = "labIphone";
            this.labIphone.Padding = new System.Windows.Forms.Padding(34, 0, 0, 0);
            this.labIphone.Size = new System.Drawing.Size(137, 35);
            this.labIphone.Symbol = 62108;
            this.labIphone.SymbolSize = 30;
            this.labIphone.TabIndex = 30;
            this.labIphone.Text = "联系方式";
            // 
            // ManageEditUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 512);
            this.Controls.Add(this.labIphone);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.btnPerOrder);
            this.Controls.Add(this.tboxIphone);
            this.Controls.Add(this.imgbtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labGuid);
            this.Controls.Add(this.tboxAge);
            this.Controls.Add(this.tboxGuid);
            this.Controls.Add(this.labAge);
            this.Controls.Add(this.labSex);
            this.Controls.Add(this.uiRadioButtonGroup1);
            this.Controls.Add(this.tboxName);
            this.Name = "ManageEditUser";
            this.Text = "个人信息";
            this.Controls.SetChildIndex(this.tboxName, 0);
            this.Controls.SetChildIndex(this.uiRadioButtonGroup1, 0);
            this.Controls.SetChildIndex(this.labSex, 0);
            this.Controls.SetChildIndex(this.labAge, 0);
            this.Controls.SetChildIndex(this.tboxGuid, 0);
            this.Controls.SetChildIndex(this.tboxAge, 0);
            this.Controls.SetChildIndex(this.labGuid, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.imgbtn, 0);
            this.Controls.SetChildIndex(this.tboxIphone, 0);
            this.Controls.SetChildIndex(this.btnPerOrder, 0);
            this.Controls.SetChildIndex(this.pnlBtm, 0);
            this.Controls.SetChildIndex(this.labName, 0);
            this.Controls.SetChildIndex(this.labIphone, 0);
            this.uiRadioButtonGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgbtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunny.UI.UITextBox tboxAge;
        private System.Windows.Forms.TextBox tboxGuid;
        private System.Windows.Forms.Label labAge;
        private System.Windows.Forms.Label labSex;
        private Sunny.UI.UIRadioButton uiRadioMan;
        private Sunny.UI.UIRadioButtonGroup uiRadioButtonGroup1;
        private Sunny.UI.UIRadioButton uiRadioWoman;
        private Sunny.UI.UITextBox tboxName;
        private System.Windows.Forms.Label labGuid;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UIImageButton imgbtn;
        private Sunny.UI.UITextBox tboxIphone;
        private Sunny.UI.UISymbolButton btnPerOrder;
        private Sunny.UI.UIToolTip uiToolTip1;
        private Sunny.UI.UISymbolLabel labName;
        private Sunny.UI.UISymbolLabel labIphone;
    }
}
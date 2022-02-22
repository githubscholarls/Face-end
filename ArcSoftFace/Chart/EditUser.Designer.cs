
namespace ArcSoftFace
{
    partial class EditUser
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
            this.labGuid = new System.Windows.Forms.Label();
            this.labName = new System.Windows.Forms.Label();
            this.labSex = new System.Windows.Forms.Label();
            this.labPath = new System.Windows.Forms.Label();
            this.labAge = new System.Windows.Forms.Label();
            this.tboxGuid = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.uiRadioButtonGroup1 = new Sunny.UI.UIRadioButtonGroup();
            this.uiRadioMan = new Sunny.UI.UIRadioButton();
            this.uiRadioWoman = new Sunny.UI.UIRadioButton();
            this.tboxName = new Sunny.UI.UITextBox();
            this.tboxAge = new Sunny.UI.UITextBox();
            this.btnFace = new Sunny.UI.UIButton();
            this.pnlBtm.SuspendLayout();
            this.uiRadioButtonGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.Controls.Add(this.btnSave);
            this.pnlBtm.Location = new System.Drawing.Point(1, 342);
            this.pnlBtm.Size = new System.Drawing.Size(563, 55);
            this.pnlBtm.Controls.SetChildIndex(this.btnSave, 0);
            // 
            // labGuid
            // 
            this.labGuid.AutoSize = true;
            this.labGuid.Location = new System.Drawing.Point(386, 229);
            this.labGuid.Name = "labGuid";
            this.labGuid.Size = new System.Drawing.Size(65, 31);
            this.labGuid.TabIndex = 0;
            this.labGuid.Text = "guid";
            this.labGuid.Visible = false;
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Location = new System.Drawing.Point(98, 62);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(62, 31);
            this.labName.TabIndex = 1;
            this.labName.Text = "姓名";
            // 
            // labSex
            // 
            this.labSex.AutoSize = true;
            this.labSex.Location = new System.Drawing.Point(98, 136);
            this.labSex.Name = "labSex";
            this.labSex.Size = new System.Drawing.Size(62, 31);
            this.labSex.TabIndex = 2;
            this.labSex.Text = "性别";
            // 
            // labPath
            // 
            this.labPath.AutoSize = true;
            this.labPath.Location = new System.Drawing.Point(98, 300);
            this.labPath.Name = "labPath";
            this.labPath.Size = new System.Drawing.Size(62, 31);
            this.labPath.TabIndex = 3;
            this.labPath.Text = "人脸";
            // 
            // labAge
            // 
            this.labAge.AutoSize = true;
            this.labAge.Location = new System.Drawing.Point(98, 226);
            this.labAge.Name = "labAge";
            this.labAge.Size = new System.Drawing.Size(62, 31);
            this.labAge.TabIndex = 4;
            this.labAge.Text = "年龄";
            // 
            // tboxGuid
            // 
            this.tboxGuid.Location = new System.Drawing.Point(457, 226);
            this.tboxGuid.Name = "tboxGuid";
            this.tboxGuid.Size = new System.Drawing.Size(100, 39);
            this.tboxGuid.TabIndex = 5;
            this.tboxGuid.Visible = false;
            this.tboxGuid.WordWrap = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(212, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 31);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // uiRadioButtonGroup1
            // 
            this.uiRadioButtonGroup1.Controls.Add(this.uiRadioMan);
            this.uiRadioButtonGroup1.Controls.Add(this.uiRadioWoman);
            this.uiRadioButtonGroup1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRadioButtonGroup1.Location = new System.Drawing.Point(213, 115);
            this.uiRadioButtonGroup1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiRadioButtonGroup1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRadioButtonGroup1.Name = "uiRadioButtonGroup1";
            this.uiRadioButtonGroup1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiRadioButtonGroup1.Size = new System.Drawing.Size(258, 76);
            this.uiRadioButtonGroup1.TabIndex = 11;
            this.uiRadioButtonGroup1.Text = null;
            // 
            // uiRadioMan
            // 
            this.uiRadioMan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiRadioMan.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiRadioMan.Location = new System.Drawing.Point(17, 35);
            this.uiRadioMan.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRadioMan.Name = "uiRadioMan";
            this.uiRadioMan.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.uiRadioMan.Size = new System.Drawing.Size(66, 28);
            this.uiRadioMan.TabIndex = 1;
            this.uiRadioMan.Text = "男";
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
            this.tboxName.Location = new System.Drawing.Point(213, 62);
            this.tboxName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tboxName.Maximum = 2147483647D;
            this.tboxName.Minimum = -2147483648D;
            this.tboxName.MinimumSize = new System.Drawing.Size(1, 1);
            this.tboxName.Name = "tboxName";
            this.tboxName.Padding = new System.Windows.Forms.Padding(5);
            this.tboxName.Size = new System.Drawing.Size(258, 39);
            this.tboxName.TabIndex = 12;
            // 
            // tboxAge
            // 
            this.tboxAge.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tboxAge.FillColor = System.Drawing.Color.White;
            this.tboxAge.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tboxAge.Location = new System.Drawing.Point(213, 226);
            this.tboxAge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tboxAge.Maximum = 2147483647D;
            this.tboxAge.Minimum = -2147483648D;
            this.tboxAge.MinimumSize = new System.Drawing.Size(1, 1);
            this.tboxAge.Name = "tboxAge";
            this.tboxAge.Padding = new System.Windows.Forms.Padding(5);
            this.tboxAge.Size = new System.Drawing.Size(160, 39);
            this.tboxAge.TabIndex = 13;
            this.tboxAge.Text = "0";
            this.tboxAge.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            // 
            // btnFace
            // 
            this.btnFace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFace.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnFace.Location = new System.Drawing.Point(213, 290);
            this.btnFace.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnFace.Name = "btnFace";
            this.btnFace.Size = new System.Drawing.Size(160, 41);
            this.btnFace.TabIndex = 15;
            this.btnFace.Text = "选择人脸";
            this.btnFace.Click += new System.EventHandler(this.btnFace_Click_1);
            // 
            // EditUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 400);
            this.Controls.Add(this.btnFace);
            this.Controls.Add(this.tboxAge);
            this.Controls.Add(this.tboxName);
            this.Controls.Add(this.uiRadioButtonGroup1);
            this.Controls.Add(this.tboxGuid);
            this.Controls.Add(this.labAge);
            this.Controls.Add(this.labPath);
            this.Controls.Add(this.labSex);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.labGuid);
            this.Name = "EditUser";
            this.Text = "编辑顾客信息";
            this.Controls.SetChildIndex(this.pnlBtm, 0);
            this.Controls.SetChildIndex(this.labGuid, 0);
            this.Controls.SetChildIndex(this.labName, 0);
            this.Controls.SetChildIndex(this.labSex, 0);
            this.Controls.SetChildIndex(this.labPath, 0);
            this.Controls.SetChildIndex(this.labAge, 0);
            this.Controls.SetChildIndex(this.tboxGuid, 0);
            this.Controls.SetChildIndex(this.uiRadioButtonGroup1, 0);
            this.Controls.SetChildIndex(this.tboxName, 0);
            this.Controls.SetChildIndex(this.tboxAge, 0);
            this.Controls.SetChildIndex(this.btnFace, 0);
            this.pnlBtm.ResumeLayout(false);
            this.uiRadioButtonGroup1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labGuid;
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Label labSex;
        private System.Windows.Forms.Label labPath;
        private System.Windows.Forms.Label labAge;
        private System.Windows.Forms.TextBox tboxGuid;
        private System.Windows.Forms.Button btnSave;
        private Sunny.UI.UIRadioButtonGroup uiRadioButtonGroup1;
        private Sunny.UI.UIRadioButton uiRadioMan;
        private Sunny.UI.UIRadioButton uiRadioWoman;
        private Sunny.UI.UITextBox tboxName;
        private Sunny.UI.UITextBox tboxAge;
        private Sunny.UI.UIButton btnFace;
    }
}
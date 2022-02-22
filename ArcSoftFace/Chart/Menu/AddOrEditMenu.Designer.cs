
namespace ArcSoftFace.Chart.Menu
{
    partial class AddOrEditMenu
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
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.cbbMenuClassify = new Sunny.UI.UIComboBox();
            this.txtMenuName = new Sunny.UI.UITextBox();
            this.txtMenuPrice = new Sunny.UI.UITextBox();
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.Location = new System.Drawing.Point(1, 298);
            this.pnlBtm.Size = new System.Drawing.Size(600, 55);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel1.Location = new System.Drawing.Point(73, 68);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(119, 40);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "菜品分类";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel2.Location = new System.Drawing.Point(73, 129);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(119, 40);
            this.uiLabel2.TabIndex = 1;
            this.uiLabel2.Text = "菜名";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel3.Location = new System.Drawing.Point(73, 185);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(119, 40);
            this.uiLabel3.TabIndex = 2;
            this.uiLabel3.Text = "单价";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbMenuClassify
            // 
            this.cbbMenuClassify.FillColor = System.Drawing.Color.White;
            this.cbbMenuClassify.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cbbMenuClassify.Items.AddRange(new object[] {
            "主食",
            "凉菜",
            "热菜",
            "汤类"});
            this.cbbMenuClassify.Location = new System.Drawing.Point(214, 69);
            this.cbbMenuClassify.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbMenuClassify.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbbMenuClassify.Name = "cbbMenuClassify";
            this.cbbMenuClassify.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbbMenuClassify.Size = new System.Drawing.Size(202, 39);
            this.cbbMenuClassify.TabIndex = 3;
            this.cbbMenuClassify.Text = "主食";
            this.cbbMenuClassify.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMenuName
            // 
            this.txtMenuName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMenuName.FillColor = System.Drawing.Color.White;
            this.txtMenuName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtMenuName.Location = new System.Drawing.Point(214, 130);
            this.txtMenuName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMenuName.Maximum = 2147483647D;
            this.txtMenuName.Minimum = -2147483648D;
            this.txtMenuName.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtMenuName.Name = "txtMenuName";
            this.txtMenuName.Padding = new System.Windows.Forms.Padding(5);
            this.txtMenuName.Size = new System.Drawing.Size(202, 39);
            this.txtMenuName.TabIndex = 4;
            // 
            // txtMenuPrice
            // 
            this.txtMenuPrice.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMenuPrice.FillColor = System.Drawing.Color.White;
            this.txtMenuPrice.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtMenuPrice.Location = new System.Drawing.Point(214, 186);
            this.txtMenuPrice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMenuPrice.Maximum = 2147483647D;
            this.txtMenuPrice.Minimum = -2147483648D;
            this.txtMenuPrice.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtMenuPrice.Name = "txtMenuPrice";
            this.txtMenuPrice.Padding = new System.Windows.Forms.Padding(5);
            this.txtMenuPrice.Size = new System.Drawing.Size(202, 39);
            this.txtMenuPrice.TabIndex = 5;
            // 
            // AddOrEditMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 356);
            this.Controls.Add(this.txtMenuPrice);
            this.Controls.Add(this.cbbMenuClassify);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.txtMenuName);
            this.Name = "AddOrEditMenu";
            this.Text = "菜单详情";
            this.Controls.SetChildIndex(this.txtMenuName, 0);
            this.Controls.SetChildIndex(this.uiLabel1, 0);
            this.Controls.SetChildIndex(this.uiLabel2, 0);
            this.Controls.SetChildIndex(this.uiLabel3, 0);
            this.Controls.SetChildIndex(this.cbbMenuClassify, 0);
            this.Controls.SetChildIndex(this.txtMenuPrice, 0);
            this.Controls.SetChildIndex(this.pnlBtm, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel3;
        public Sunny.UI.UIComboBox cbbMenuClassify;
        private Sunny.UI.UITextBox txtMenuName;
        private Sunny.UI.UITextBox txtMenuPrice;
    }
}
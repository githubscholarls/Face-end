
namespace ArcSoftFace
{
    partial class FaceUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceUI));
            this.SuspendLayout();
            // 
            // Aside
            // 
            this.Aside.BackColor = System.Drawing.Color.White;
            this.Aside.FillColor = System.Drawing.Color.White;
            this.Aside.ForeColor = System.Drawing.Color.Black;
            this.Aside.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.Aside.LineColor = System.Drawing.Color.LightGray;
            this.Aside.Location = new System.Drawing.Point(2, 35);
            this.Aside.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.Aside.SelectedForeColor = System.Drawing.Color.Black;
            this.Aside.Size = new System.Drawing.Size(250, 512);
            this.Aside.Style = Sunny.UI.UIStyle.Custom;
            // 
            // FaceUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(966, 549);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FaceUI";
            this.Padding = new System.Windows.Forms.Padding(2, 35, 2, 2);
            this.ShowDragStretch = true;
            this.ShowRadius = false;
            this.Text = "餐厅人脸识别系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceUI_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
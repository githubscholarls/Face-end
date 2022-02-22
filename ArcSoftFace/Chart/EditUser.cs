using ArcSoftFace.Entity;
using ArcSoftFace.SDKModels;
using ArcSoftFace.Utils;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace
{
    public partial class EditUser : UIEditForm
    {
        FaceForm face;
        private User user;
        public string newimagePath;
        public EditUser(FaceForm face)
        {
            InitializeComponent();
            this.face = face;
        }
        protected override bool CheckData()
        {
            return CheckEmpty(tboxName,"请输入姓名")&&CheckRange(tboxAge,0,100,"输入年龄范围0~100");
        }
        public User User { 
            get
            {
                if (user == null)
                    user = new User();
                user.Name = tboxName.Text.Trim();
                user.ImagePath = tboxGuid.Text.Trim();
                user.Sex = uiRadioMan.Checked == true;
                user.Age = Convert.ToInt32(tboxAge.Text.Trim());
                return user;
            }
            set 
            {
                user = value;
                tboxName.Text = value.Name==null?"": value.Name.ToString();
                tboxAge.Text = value.Age.ToString();
                tboxGuid.Text = value.ImagePath;
                
                if (value.Sex)
                    uiRadioMan.Checked = true;
                else
                    uiRadioWoman.Checked = true;

            } 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User();
                user.ImagePath = tboxGuid.Text.Trim();
                user.Name = tboxName.Text.Trim();
                user.Age = Convert.ToInt32(tboxAge.Text.Trim());
                user.Sex = uiRadioMan.Checked ? true : false;
                int err= face.cutEditSave(user, newimagePath);
                
                if(err==0)
                    MessageBox.Show("修改成功!");
                else if(err==4)
                    MessageBox.Show("请选择含有人脸的图片");
                else
                    MessageBox.Show("图片存在问题!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void btnFace_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择图片";
            openFileDialog.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog.Multiselect = false;
            openFileDialog.FileName = string.Empty;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                newimagePath = openFileDialog.FileName;
            }
        }

    }
}

using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace
{
    public partial class FLogin : UILoginForm
    {
        public FLogin()
        {
            InitializeComponent();
            base.ButtonLoginClick += FLogin_ButtonLoginClick;
        }
        private void FLogin_ButtonLoginClick(object sender, System.EventArgs e)
        {
            //UserName就是封装了界面里用户名输入框的值
            //Password就是封装了界面里密码输入框的值
            if (UserName == "admin" && Password == "admin")
            {
                IsLogin = true;
                Close();
            }
            else
            {
                ShowErrorTip("用户名或者密码错误。");
            }
        }
    }
}

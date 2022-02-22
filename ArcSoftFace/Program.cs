using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FaceForm());

            var login = new FLogin();
            login.ShowDialog();
            if(login.IsLogin)
            {
                UIMessageTip.ShowOk("登录成功!");
                Application.Run(new FaceUI());
            }
        }
    }
}

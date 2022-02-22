using ArcSoftFace.Utils;
using StackExchange.Redis;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.Chart
{
    public partial class SystemService : UIPage
    {
        private static bool isRunVideo = default;
        private readonly FaceForm face;
        private IDatabase db;
        DateTime lastTime = DateTime.Now.Date.AddDays(1);

        public SystemService(FaceForm face)
        {
            InitializeComponent();
            this.face = face;
            db=face.redis.GetDatabase();
            if (!db.KeyExists("orderNumber"))
            {
                db.StringSet("orderNumber", 0, expiry: lastTime - DateTime.Now);
            }
            if (!db.KeyExists("orderMoney"))
            {
                db.StringSet("orderMoney", 0, expiry: lastTime - DateTime.Now);
            }

            //help
            {
                uiToolTip1.SetToolTip(labExpire, "您到店时刻起时间间隔内再次到店不做数据统计！");
            }
            


            init();
        }
        public void init()
        {
            
            var num1 = db.StringGet("orderMoney").ToString();
            btnMoney.Text = num1;

            //订单数量
            //string sql2 = $"select count(*) from [UserOrder]";
            //var num2=SqlHelperUtil.ExecuteScalar(sql2, null)?.ToString();

            var num2 = db.StringGet("orderNumber").ToString();
            btnDDS.Text = num2;

            //就餐人数
            string sql3 = $"select count(*) from [UserHistory] where DateTime>='{DateTime.Now.Date}' and DateTime<='{DateTime.Now.AddDays(1).Date}' group by Guid";
            var num3= SqlHelperUtil.ExecuteScalar(sql3, null)?.ToString();
            if (string.IsNullOrWhiteSpace(num3))
                num3 = "0";
            btnJCRS.Text = num3; 

        }

        private void btnImgRegister_Click(object sender, EventArgs e)
        {
            face.ChooseMultiImg(null, null);
        }

        private void btnStartVideo_Click(object sender, EventArgs e)
        {
            if (!isRunVideo)
            {
                btnStartVideo.Text = "关闭摄像头";
                isRunVideo = true;
                face.btnStartVideo_Click(null, null);
                
            }
            else
            {
                btnStartVideo.Text = "开启摄像头";
                isRunVideo = false;
                face.btnStartVideo_Click(null, null);
            }
        }

        private void btnClearFaceList_Click(object sender, EventArgs e)
        {
            string value = "";
            if (this.InputPasswordDialog(ref value))
            {
                if (value == "lishuai123")
                {
                    //face.btnClearFaceList_Click(null, null);
                }
                else
                {
                    ShowErrorTip("密码错误！");
                }
            }
        }

        private void btnTimeSave_Click(object sender, EventArgs e)
        {
            var hour = shi.Value;
            if (hour < 0)
                hour = 0;
            var min = fen.Value;
            if (min < 0)
                min = 0;

            face.Expire = hour * 3600 + min * 60;

            
        }

        private void btnFlash_Click(object sender, EventArgs e)
        {
            init();
            ShowSuccessTip("刷新成功！");
        }
    }
}

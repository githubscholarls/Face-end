using ArcSoftFace.Chart;
using ArcSoftFace.Entity;
using ArcSoftFace.Model;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace
{
    public partial class ManageEditUser : UIEditForm
    {
        private User userDto;
        public User _user = new User();
        private string ImagePath;
        private FaceForm face;
        IDatabase db;
        DateTime lastTime = DateTime.Now.Date.AddDays(1);

        public ManageEditUser(FaceForm Face)
        {
            InitializeComponent();

            face = Face;
            db = face.redis.GetDatabase();
            ImagePath = AppDomain.CurrentDomain.BaseDirectory;
            ImagePath = ImagePath.Substring(0, ImagePath.LastIndexOf("\\bin")) + "\\Image\\";
            base.ButtonOkClick += ButtonOkClick;


            //help
            uiToolTip1.SetToolTip(labName, "重置名字，请重新输入，若含有“*”不会处理，让您的信息更加安全!");
            uiToolTip1.SetToolTip(labIphone, "重置联系方式，请重新输入，若含有“*”不会处理，让您的信息更加安全!");
        }
        protected override bool CheckData()
        {
            return /*CheckEmpty(tboxName, "请输入姓名") &&*/ CheckRange(tboxAge, 0, 100, "输入年龄范围0~100");
        }
        public User User
        {
            get
            {
                if (userDto == null)
                    userDto = new User();
                userDto.Name = tboxName.Text.Trim();
                userDto.Sex = uiRadioMan.Checked == true;
                userDto.Age = Convert.ToInt32(tboxAge.Text.Trim());
                userDto.Iphone = tboxIphone.Text.Trim().ToString();
                return userDto;
            }
            set
            {

                userDto = value;
                tboxGuid.Text = value.ImagePath;
                tboxName.Text = value.Name == null ? "" : value.Name.ToString();
                tboxAge.Text = value.Age.ToString();
                imgbtn.BackgroundImage = Image.FromFile($"{ImagePath}{value.ImagePath}.jpg");
                tboxIphone.Text = value.Iphone;
                if (value.Sex)
                    uiRadioMan.Checked = true;
                else
                    uiRadioWoman.Checked = true;

            }
        }


        public new void ButtonOkClick(object sender, EventArgs e)
        {
            try
            {
                _user.ImagePath = tboxGuid.Text.Trim();
                _user.Age = Convert.ToInt32(tboxAge.Text.Trim());
                _user.Sex = uiRadioMan.Checked ? true : false;
                var iphone = tboxIphone.Text.Trim().ToString();



                if (!tboxName.Text.Contains("*"))
                {
                    _user.Name = tboxName.Text.Trim();
                }
                if (!string.IsNullOrWhiteSpace(iphone) && iphone.Length != 11)
                {
                    ShowErrorTip("手机号格式错误！");
                    return;
                }
                if (!iphone.Contains("*"))
                    _user.Iphone = iphone;

                SqlHelperUtil.UpdateUserInfo(_user);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void uiImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnPerOrder_Click(object sender, EventArgs e)
        {
            string guid = tboxGuid.Text.Trim();
            var order = new PerOrder(guid);
            order.ShowDialog();
            if (order.IsOK)
            {
                if (order.curSumPri == 0)
                {
                    ShowSuccessTip("您未修改数据", 1500);
                    return;
                }
                string sql = $"update [User] set AmountOfMoney='{order.curSumPri}' where GuidPath='{guid}'";
                SqlHelperUtil.ExecuteScalar(sql, null);
                SqlHelperUtil.UpdateTableToUserMenu(order.dt, guid);
                //消费次数加1
                SqlHelperUtil.AddLoginTimes(guid);
                //添加用户订单
                string ordersql = "";
                foreach (var item in order.MenuIdToNumber)
                {
                    ordersql += $"insert into [UserOrder] values('{guid}','{item.Key}','{item.Value}');";
                }
                SqlHelperUtil.ExecuteNonQuery(ordersql, null);


                //开始页面记录用户订单数量，有效期一天
                if (!db.KeyExists("orderNumber"))
                {
                    db.StringSet("orderNumber", 0,lastTime-DateTime.Now);
                }
                if (db.KeyExists("orderNumber"))
                {
                    var old = db.StringGet("orderNumber").ToString();
                    if (int.TryParse(old, out int res))
                    {
                        db.StringSet("orderNumber", res + 1);
                    }
                }
                if (!db.KeyExists("orderMoney"))
                {
                    db.StringSet("orderMoney", 0, lastTime - DateTime.Now);
                }
                if (db.KeyExists("orderMoney"))
                {
                    var money = db.StringGet("orderMoney").ToString();
                    if (double.TryParse(money, out double resMoney))
                    {
                        db.StringSet("orderMoney", resMoney + order.curSumPri);
                    }
                }

                ShowSuccessTip("保存成功");
            }
            else
            {
                ShowSuccessTip("您未修改数据", 1500);
            }

        }

    }
}

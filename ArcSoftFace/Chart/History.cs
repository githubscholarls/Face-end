using ArcSoftFace.Entity;
using ArcSoftFace.Model;
using ArcSoftFace.Utils;
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

namespace ArcSoftFace.Chart
{
    public partial class History : UIPage

    {
        public History()
        {
            InitializeComponent();

            uiDataGridView1.AddColumn("姓名", "Name").SetFixedMode(100);
            uiDataGridView1.AddColumn("性别", "Sex").SetFixedMode(100);
            uiDataGridView1.AddColumn("年龄", "Age").SetFixedMode(100);
            uiDataGridView1.AddColumn("联系方式", "Iphone").SetFixedMode(150);
            uiDataGridView1.AddColumn("登录时间", "HistoryLoginTime").SetFixedMode(150);

            uiDataGridView1.ReadOnly = true;
        }
        public override void Init()
        {
            base.Init();
            UpdateDatas();
        }
        private void UpdateDatas()
        {
            //取history的guid和时间，同一个用户list中下标相同
            DataTable dt = SqlHelperUtil.ReadTableUserHistory();
            List<string> guidsHistory=new List<string>();
            List<DateTime> times=new List<DateTime>();
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                guidsHistory.Add(dt.Rows[i][0].ToString());
                times.Add(DateTime.Parse(dt.Rows[i][1].ToString()));
            }

            //把每个不同的guid信息取出来
            DataTable allUser = SqlHelperUtil.ReadTableUserAll();
            Dictionary<string, UserHistoryDto> guidUserHistory = new Dictionary<string, UserHistoryDto>();
            for (int i = 0; i < allUser.Rows.Count; i++)
            {
                Console.WriteLine(allUser.Rows[i][2].ToString());
                guidUserHistory.Add(allUser.Rows[i][4].ToString(), new UserHistoryDto()
                {
                    Name = allUser.Rows[i][1].ToString(),
                    Sex= allUser.Rows[i][2].ToString()=="True"?"男":"女",
                    Age = Convert.ToInt32(allUser.Rows[i][3].ToString()),
                    Iphone= allUser.Rows[i][9].ToString()
                });
            }

            //对list中guid做时间映射
            List<UserHistoryDto> datas = new List<UserHistoryDto>();
            for (int i = 0; i < guidsHistory.Count; i++)
            {
                //把相应时间放到datas
                if (guidUserHistory.ContainsKey(guidsHistory[i]))
                {
                    var per = new UserHistoryDto()
                    {
                        Name = guidUserHistory[guidsHistory[i]].Name,
                        HistoryLoginTime = times[i],
                        Age = guidUserHistory[guidsHistory[i]].Age,
                        Sex = guidUserHistory[guidsHistory[i]].Sex,
                        Iphone = guidUserHistory[guidsHistory[i]].Iphone
                    };
                    datas.Add(per);
                }
            }

            datas.Reverse();

            //设置数据源
            uiPagination1.DataSource = datas;
            uiPagination1.ActivePage = 1;
        }

        
        private void uiPagination1_PageChanged(object sender, object pagingSource, int pageIndex, int count)
        {
            uiDataGridView1.DataSource = pagingSource;
        }
    }
}

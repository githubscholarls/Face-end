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
    public partial class FPieChart : UIPage
    {
        private List<int> AgeNumber = new List<int>();
        public FPieChart()
        {
            InitializeComponent();
            uiSymbolButton1_Click(null, null);
            
        }



        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            #region 年龄段
            var option = new UIPieOption();

            //设置Title
            option.Title = new UITitle();
            option.Title.Text = "";
            option.Title.SubText = "";
            option.Title.Left = UILeftAlignment.Center;

            //设置ToolTip
            option.ToolTip.Visible = true;

            //设置Legend
            option.Legend = new UILegend
            {
                Orient = UIOrient.Vertical,
                Top = UITopAlignment.Top,
                Left = UILeftAlignment.Left
            };

            option.Legend.AddData("0~19");
            option.Legend.AddData("20~39");
            option.Legend.AddData("40~59");
            option.Legend.AddData("60~79");
            option.Legend.AddData("80~100+");

            //设置Series
            var series = new UIPieSeries
            {
                Name = "",
                Center = new UICenter(50, 55),
                Radius = 70
            };
            series.Label.Show = true;

            //增加数据
            List<int> res= SqlHelperUtil.GetTableUserAllAge();
            int age79=0, age59=0, age39=0, age19=0, age0=0;
            foreach (var item in res)
            {
                if (item > 79)
                    age79++;
                else if (item > 59)
                    age59++;
                else if (item > 39)
                    age39++;
                else if (item > 19)
                    age19++;
                else if(item>0)
                    age0++;

            }
            series.AddData("0~19", age0);
            series.AddData("20~39", age19);
            series.AddData("40~59", age39);
            series.AddData("60~79", age59);
            series.AddData("80~100+", age79);

            //增加Series
            option.Series.Add(series);


            //设置Option
            uiPieChart1.SetOption(option);
            #endregion

            #region 性别
            var optionSex = new UIPieOption();

            //设置Title
            optionSex.Title = new UITitle
            {
                Text = "",
                SubText = "",
                Left = UILeftAlignment.Center
            };

            //设置ToolTip
            optionSex.ToolTip.Visible = true;
            //设置Legend
            optionSex.Legend = new UILegend();
            optionSex.Legend.Orient = UIOrient.Vertical;
            optionSex.Legend.Top = UITopAlignment.Top;
            optionSex.Legend.Left = UILeftAlignment.Left;

            optionSex.Legend.AddData("男");
            optionSex.Legend.AddData("女");

            //设置Series
            var seriesSex = new UIPieSeries
            {
                Name = "",
                Center = new UICenter(50, 55),
                Radius = 70
            };
            seriesSex.Label.Show = true;

            //增加数据
            int man = 0;
            int woman = 0;
            SqlHelperUtil.GetTableUserSexNumber( out man, out woman);
            seriesSex.AddData("男", man);
            seriesSex.AddData("女", woman);

            //增加Series
            optionSex.Series.Add(seriesSex);
            //设置Option
            uiPieChart2.SetOption(optionSex);
            #endregion

            #region 近一周不同年龄段消费次数
            var option3 = new UIPieOption();

            //设置Title
            option3.Title = new UITitle();
            option3.Title.Text = "";
            option3.Title.SubText = "";
            option3.Title.Left = UILeftAlignment.Center;

            //设置ToolTip
            option3.ToolTip.Visible = true;

            //设置Legend
            option3.Legend = new UILegend
            {
                Orient = UIOrient.Vertical,
                Top = UITopAlignment.Top,
                Left = UILeftAlignment.Left
            };

            option3.Legend.AddData("0~19");
            option3.Legend.AddData("20~39");
            option3.Legend.AddData("40~59");
            option3.Legend.AddData("60~79");
            option3.Legend.AddData("80~100+");

            //设置Series
            var series3 = new UIPieSeries
            {
                Name = "",
                Center = new UICenter(50, 55),
                Radius = 70
            };
            series3.Label.Show = true;

            //增加数据
            List<int> res3 = SqlHelperUtil.GetDifferentAgeGroupsLastTime(DateTime.Now.AddDays(-5));
            series3.AddData("0~19", res3[0]);
            series3.AddData("20~39", res3[1]);
            series3.AddData("40~59", res3[2]);
            series3.AddData("60~79", res3[3]);
            series3.AddData("80~100+", res3[4]);

            //增加Series
            option3.Series.Add(series3);


            //设置Option
            uiPieChart3.SetOption(option3);
            #endregion
        }

        private void btnflash_Click(object sender, EventArgs e)
        {
            uiSymbolButton1_Click(null, null);
            ShowSuccessTip("刷新成功!");
        }
    }
}

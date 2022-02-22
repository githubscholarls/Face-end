using Sunny.UI;
using System.Drawing;
using System.Collections.Generic;
using ArcSoftFace.Utils;
using System;

namespace FaceUI.Chart
{
    public partial class FBarChart : UIPage
    {
        private List<int> RegisPer = new List<int>();
        private List<int> LoginPer = new List<int>();
        private bool isYear = false;
        private List<string> _cbbValue = new List<string>() { "近一周", "月", "季度", "年" };
        DateTime lastTime = DateTime.Now.AddDays(-6);


        //更新界面数据用
        static UIBarChart ulBar;
        //清空上次界面用
        static UIBarChart ulLastBar;
        public FBarChart()
        {
            InitializeComponent();
            init();
        }
        /// <summary>
        /// 数据初始化
        /// </summary>
        public void init()
        {
            UIBarOption option = new UIBarOption();
            option.Title = new UITitle();
            option.Title.Text = "近周人流量";
            option.Title.SubText = "";

            //设置Legend
            option.Legend = new UILegend();
            option.Legend.Orient = UIOrient.Horizontal;
            option.Legend.Top = UITopAlignment.Top;
            option.Legend.Left = UILeftAlignment.Left;
            option.Legend.AddData("新用户");
            option.Legend.AddData("老用户");

            int XNumber = DateTime.Now.Day - lastTime.Day;

            for (int i = XNumber; i >= 0; i--)
            {
                option.XAxis.Data.Add(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd"));
            }


            option.ToolTip.Visible = true;
            option.YAxis.Scale = true;

            option.XAxis.Name = "日期";
            option.YAxis.Name = "数值";

            option.YAxisScaleLines.Add(new UIScaleLine() { Color = Color.Red, Name = "上限", Value = 1000 });
            option.YAxisScaleLines.Add(new UIScaleLine() { Color = Color.Gold, Name = "下限", Value = 0 });
            ReadDataPerRegisterAndLogin();

            UIBarSeries seriesRegister = new UIBarSeries();
            seriesRegister.Name = "新用户";
            foreach (int item in RegisPer)
            {
                seriesRegister.AddData(item);
            }
            option.Series.Add(seriesRegister);


            UIBarSeries seriesLogin = new UIBarSeries();
            seriesLogin.Name = "老用户";
            foreach (int item in LoginPer)
            {
                seriesLogin.AddData(item);
            }
            option.Series.Add(seriesLogin);
            BarChart.SetOption(option);

        }

        /// <summary>
        /// 读取数据库更新界面人数
        /// </summary>
        private void UpdatePerData()
        {
            if (BarChart != null)
                BarChart.Dispose();

            RegisPer.Clear();
            LoginPer.Clear();

            ulBar = new UIBarChart();
            ReadDataPerRegisterAndLogin(isYear);

            UIBarOption option = new UIBarOption();
            option.Title = new UITitle();
            option.Title.Text = "近周人流量";
            option.Title.SubText = "";

            //设置Legend
            option.Legend = new UILegend();
            option.Legend.Orient = UIOrient.Horizontal;
            option.Legend.Top = UITopAlignment.Top;
            option.Legend.Left = UILeftAlignment.Left;
            option.Legend.AddData("新用户");
            option.Legend.AddData("老用户");

            int XNumber = (DateTime.Now.Date.AddDays(1) - lastTime.Date).Days;
            int month = DateTime.Now.Month;
            if (XNumber > 100)
            {
                option.Title.Text = "近一年人流量";
            }
            else if(XNumber>30)
            {
                option.Title.Text = "近一季度人流量";
            }
            else if(XNumber>7)
            {
                option.Title.Text = "近一月人流量";
            }
            for (int i = XNumber-1; i >= 0; i--)
            {
                //年
                if (XNumber > 100)
                {
                    while (lastTime<DateTime.Now.Date)
                    {
                        option.XAxis.Data.Add(lastTime.Date.AddMonths(1).ToString("yyyy-MM").Substring(2));
                        lastTime = lastTime.AddMonths(1);
                    }
                    break;
                }
                //季度
                else if (XNumber < 101 && XNumber > 30)
                {
                    if (XNumber == i)
                    {
                        option.XAxis.Data.Add(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd").Substring(5));
                        continue;
                    }
                    if (lastTime.AddDays(XNumber - i).Day == 1)
                    {
                        option.XAxis.Data.Add(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd").Substring(5));
                        continue;
                    }
                    option.XAxis.Data.Add(""/*DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd").Substring(8)*/);
                }
                //月 
                else if (XNumber > 7 && XNumber < 31)
                {
                    if (lastTime.AddDays(XNumber - i + 1).Day == 1)//每月最后一天
                    {
                        option.XAxis.Data.Add("");
                        continue;
                    }
                    else if (lastTime.AddDays(XNumber - i).Day == 1)//每月第一天
                    {
                        option.XAxis.Data.Add(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd").Substring(5));
                        continue;
                    }
                    else if(lastTime.AddDays(XNumber - i).Day == 2)//每月第二天
                    {
                        option.XAxis.Data.Add("");
                        continue;
                    }    
                    option.XAxis.Data.Add(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd").Substring(8));
                }
                else
                    option.XAxis.Data.Add(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd"));
            }


            option.ToolTip.Visible = true;
            option.YAxis.Scale = true;

            option.XAxis.Name = "日期";
            option.YAxis.Name = "数值";

            option.YAxisScaleLines.Add(new UIScaleLine() { Color = Color.Red, Name = "上限", Value = 1000 });
            option.YAxisScaleLines.Add(new UIScaleLine() { Color = Color.Gold, Name = "下限", Value = 0 });

            UIBarSeries seriesRegister = new UIBarSeries();
            seriesRegister.Name = "新用户";
            foreach (int item in RegisPer)
            {
                seriesRegister.AddData(item);
            }
            option.Series.Add(seriesRegister);

            UIBarSeries seriesLogin = new UIBarSeries();
            seriesLogin.Name = "老用户";
            foreach (int item in LoginPer)
            {
                seriesLogin.AddData(item);
            }
            option.Series.Add(seriesLogin);
            ulBar.SetOption(option);

            ulBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            ulBar.Font = new System.Drawing.Font("微软雅黑", 12F);
            ulBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            ulBar.Location = new System.Drawing.Point(30, 48);
            ulBar.MinimumSize = new System.Drawing.Size(1, 1);
            ulBar.Name = "ulBar";
            ulBar.Size = new System.Drawing.Size(692, 384);
            ulBar.TabIndex = 0;
            ulBar.Text = "ulBar";

            //清除上次界面
            if (this.PagePanel.Controls.Contains(ulLastBar))
                this.PagePanel.Controls.Remove(ulLastBar);


            ulLastBar = ulBar;
            this.PagePanel.Controls.Add(ulBar);


        }
        private void ReadDataPerRegisterAndLogin(bool isYeaer=false)
        {
            if (isYeaer)
            {
                RegisPer = SqlHelperUtil.GetRegisPerByLastYear(DateTime.Now.AddDays(1), lastTime);
                LoginPer = SqlHelperUtil.GetLoginPerByLastYear(DateTime.Now.AddDays(1), lastTime);
            }
            else
            {
                RegisPer = SqlHelperUtil.GetRegisPerByLastWeek(DateTime.Now.AddDays(1), lastTime);
                LoginPer = SqlHelperUtil.GetLoginPerByLastWeek(DateTime.Now.AddDays(1), lastTime);

            }

        }
        private void uiSymbolButton1_Click(object sender, System.EventArgs e)
        {
            UpdatePerData();
            ShowSuccessTip("更新成功!");
        }

        private void uiImageButton1_Click(object sender, System.EventArgs e)
        {
            BarChart.ChartStyleType = UIChartStyleType.Default;
        }

        private void uiImageButton2_Click(object sender, System.EventArgs e)
        {
            BarChart.ChartStyleType = UIChartStyleType.Plain;
        }

        private void uiImageButton3_Click(object sender, System.EventArgs e)
        {
            BarChart.ChartStyleType = UIChartStyleType.Dark;
        }

        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {
            int r = new Random().Next(-1, 5);
            switch (r)
            {
                case 0:
                    BarChart.ChartStyleType = UIChartStyleType.Default;
                    ulBar.ChartStyleType = UIChartStyleType.Default;
                    break;
                case 1:
                    BarChart.ChartStyleType = UIChartStyleType.Plain;
                    ulBar.ChartStyleType = UIChartStyleType.Plain;
                    break;
                case 2:
                    BarChart.ChartStyleType = UIChartStyleType.Dark;
                    ulBar.ChartStyleType = UIChartStyleType.Dark;
                    break;
                case 3:
                    BarChart.ChartStyleType = UIChartStyleType.LiveChart;
                    ulBar.ChartStyleType = UIChartStyleType.LiveChart;
                    break;
                default:
                    break;
            }

        }

        private void cbbChangeDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIComboBox box = (UIComboBox)sender;
            string selectedEmployee = (string)box.SelectedItem;
            switch (selectedEmployee)
            {
                case "近一周":
                    lastTime = DateTime.Now.AddDays(-6);
                    isYear = false;
                    break;
                case "月":
                    lastTime = DateTime.Now.AddMonths(-1).AddDays(1);
                    isYear = false;
                    break;
                case "季度":
                    lastTime = DateTime.Now.AddMonths(-3).AddDays(1);
                    isYear = false;
                    break;
                case "年":
                    lastTime = DateTime.Now.AddYears(-1).AddDays(1);
                    isYear = true;
                    break;
                default:
                    break;
            }
            Console.WriteLine();
        }
    }
}
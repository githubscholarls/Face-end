using ArcSoftFace;
using ArcSoftFace.Entity;
using ArcSoftFace.Model;
using ArcSoftFace.Utils;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace FaceUI.Chart
{
    public partial class FaceLibrary : UIPage
    {

        /// <summary>
        /// 根据guid对数据库用户信息进行修改
        /// </summary>
        private static List<string> _guid = new List<string>();
        /// <summary>
        /// 导出时候修改列明时候的映射
        /// </summary>
        private static Dictionary<string, string> _columnNames = new Dictionary<string, string>();
        /// <summary>
        /// 导出时候要删除的列名
        /// </summary>
        private static List<string> _removeColumns = new List<string>();
        private static Dictionary<string, Dictionary<string, string>> _modifyColumnValueDic = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 查询出来的数据
        /// </summary>
        DataTable dt;
        List<UserDto> datas = new List<UserDto>();

        private FaceForm _iFace;
        public FaceLibrary(FaceForm face)
        {
            _iFace = face;
            InitializeComponent();



            uiToolTip1.SetToolTip(btnChoose, "选择", "", 61670, 24, UIColor.RegularBlue);
            uiToolTip1.SetToolTip(btnEdit, "编辑", "", 61670, 24, UIColor.RegularGreen);
            uiToolTip1.SetToolTip(btnDel, "删除", "", 61670, 24, UIColor.RegularOrange);


            uiDataGridView1.AddColumn("姓名", "Name").SetFixedMode(100);
            uiDataGridView1.AddColumn("性别", "Sex").SetFixedMode(100);
            uiDataGridView1.AddColumn("年龄", "Age").SetFixedMode(100);
            uiDataGridView1.AddColumn("联系方式", "Iphone").SetFixedMode(150);
            uiDataGridView1.AddColumn("注册日期", "RegisterTime").SetFixedMode(150);
            uiDataGridView1.AddColumn("最近登录", "LastLoginTime").SetFixedMode(150);
            uiDataGridView1.AddColumn("总消费次数", "LoginTimes").SetFixedMode(150);
            uiDataGridView1.AddColumn("总消费金额", "AmountOfMoney").SetFixedMode(150);

            _columnNames.Add("Id", "编号");
            _columnNames.Add("Name", "姓名");
            _columnNames.Add("Sex", "性别");
            _columnNames.Add("Age", "年龄");
            _columnNames.Add("GuidPath", "人脸序列号");
            _columnNames.Add("RegisterTime", "注册日期");
            _columnNames.Add("LastLoginTime", "上次登录时间");
            _columnNames.Add("LoginTimes", "总消费次数");
            _columnNames.Add("state", "人员现状");
            _columnNames.Add("Iphone", "联系方式");

            _removeColumns.Add("编号");
            _removeColumns.Add("人脸序列号");
            //a_removeColumns.Add("人员现状");

            //导出信息所用字典
            var sexColumnValues = new Dictionary<string, string>();
            sexColumnValues.Add("True", "男");
            sexColumnValues.Add("False", "女");
            var stateColumnValues = new Dictionary<string, string>();
            stateColumnValues.Add("True", "已注册");
            stateColumnValues.Add("False", "未注册");
            _modifyColumnValueDic.Add("性别", sexColumnValues);
            _modifyColumnValueDic.Add("人员现状", stateColumnValues);

            uiDataGridView1.ReadOnly = true;
        }
        public override void Init()
        {
            base.Init();
            dt = SqlHelperUtil.ReadTableUser();
            fleashDGV();
        }
        private void fleashDGV()
        {
            _guid.Clear();
            datas.Clear();
            labPerSum.Text = $"共{dt.Rows.Count}人";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserDto u = new UserDto();
                var name = dt.Rows[i][1].ToString();
                if (name.Length == 2)
                    u.Name = name.Substring(0, 1) + "*";
                else if (name.Length == 3)
                    u.Name = name.Substring(0, 1) + "*" + name.Substring(name.Length-1, 1);
                u.Sex = (bool)dt.Rows[i][2] == true ? "男" : "女";
                u.Age = Convert.ToInt32(dt.Rows[i][3].ToString());
                u.RegisterTime = (DateTime)dt.Rows[i][5];
                u.LastLoginTime = (DateTime)dt.Rows[i][6];
                u.LoginTimes = Convert.ToInt32(dt.Rows[i][7]);
                //state=dt.Rows[i][8]
                //隐藏iphone
                var iphone = dt.Rows[i][9].ToString();
                if (string.IsNullOrWhiteSpace(iphone))
                    u.Iphone = "";
                else
                    u.Iphone = iphone.Substring(0, 3) + "****" + iphone.Substring(7);
                var aom = dt.Rows[i][10]?.ToString();
                if (string.IsNullOrEmpty(aom))
                    u.AmountOfMoney = 0;
                else
                {
                    u.AmountOfMoney = float.Parse(aom);
                }

                datas.Add(u);
                _guid.Add(dt.Rows[i][4].ToString());
            }

            uiPagination1.DataSource = datas;
            uiPagination1.ActivePage = 1;
        }

        private void uiPagination1_PageChanged(object sender, object pagingSource, int pageIndex, int count)
        {
            uiDataGridView1.DataSource = pagingSource;
        }

        private void uiDataGridView1_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (rowindex < 0)
                return;
            if (btnChoose.Selected)
                return;
            else if (btnEdit.Selected)
            {
                User dto = new User();
                dto.Name = uiDataGridView1.Rows[rowindex].Cells[0].Value?.ToString();
                dto.Sex = uiDataGridView1.Rows[rowindex].Cells[1].Value?.ToString() == "男" ? true : false;
                dto.Age = Convert.ToInt32(uiDataGridView1.Rows[rowindex].Cells[2].Value);
                
                dto.Iphone = uiDataGridView1.Rows[rowindex].Cells[6].Value?.ToString();
                dto.ImagePath = _guid[rowindex];
                ManageEditUser editUser = new ManageEditUser(_iFace);
                editUser.User = dto;
                editUser.ShowDialog();
                if (editUser.IsOK)
                {
                    ShowSuccessNotifier("修改成功");
                }
                editUser.Dispose();
                //刷新界面
                Init();
            }
            else if (btnDel.Selected)
            {
                var guid = _guid[rowindex];
                //删除本地guid

                if (ShowAskDialog($"您确定要删除{uiDataGridView1.Rows[rowindex].Cells[0].Value?.ToString()} 该用户吗?"))
                {
                    var k = SqlHelperUtil.DelUserByGuid(guid);
                    if (k > 0)
                    {

                        _guid.RemoveAt(rowindex);
                        //_iFace.imageLists.Images.RemoveByKey(guid);
                        ShowSuccessTip("删除成功！");
                        //刷新界面
                        Init();

                    }
                    else
                        ShowErrorTip("删除失败！！！");
                }

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tboxSearch.Text.Trim()))
                Init();
            string option = (string)cbbSelectOption.SelectedItem;
            string searchTxt = tboxSearch.Text.Trim();
            if (option == "联系方式")
            {
                dt = SqlHelperUtil.ReadTableUserByIphone(searchTxt);
            }
            else
            {
                dt = SqlHelperUtil.ReadTableUserByName(searchTxt);
            }
            fleashDGV();
        }

        private void OutPutDataReport_Click(object sender, EventArgs e)
        {
            string fileName = default;
            //处理dt

            //dt.Columns.Remove("Id");
            //dt.Columns.Remove("GuidPath");
            //dt.Columns.Remove("state");

            DataTable modifiedDt = dt.Copy();
            //该列名
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (_columnNames.ContainsKey(dt.Columns[i].ColumnName))
                    modifiedDt.Columns[i].ColumnName = _columnNames[dt.Columns[i].ColumnName];
            }
            //删除列
            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                if (_removeColumns.Contains(modifiedDt.Columns[i].ColumnName))
                    modifiedDt.Columns.Remove(modifiedDt.Columns[i].ColumnName);
            }
            //修改列位置
            modifiedDt.Columns["联系方式"].SetOrdinal(3);


            //修改表列数据类型
            DataTable res = new DataTable();
            res = modifiedDt.Clone();
            foreach (DataColumn col in res.Columns)
            {
                if (_modifyColumnValueDic.ContainsKey(col.ColumnName))
                {
                    //修改列类型
                    col.DataType = typeof(String);
                }
            }
            //修改表数据
            foreach (DataRow row in modifiedDt.Rows)
            {
                DataRow rowNew = res.NewRow();
                for (int i = 0; i < modifiedDt.Columns.Count; i++)
                {

                    var key = modifiedDt.Columns[i].ColumnName;
                    string value = row[key].ToString();
                    if (_modifyColumnValueDic.ContainsKey(key))
                    {

                        rowNew[i] = _modifyColumnValueDic[key][value];
                        continue;
                    }
                    rowNew[i] = row[i].ToString();
                }
                res.Rows.Add(rowNew);
            }



            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel 文件(*.xlsx)|*.xlsx;|xls 文件(*.xls)|*.xls|All files(*.*)|*.*";
                sfd.RestoreDirectory = true;
                sfd.DefaultExt = "xlsx";
                if (sfd.ShowDialog() != DialogResult.OK)
                    return;
                fileName = sfd.FileName;
                NpoiExcelHelper.ExportDtToExcel(res, "餐饮连锁店智能人脸识别系统人员信息", fileName);
                ShowSuccessNotifier("导出成功！");

            }
        }
        private void NoSelected()
        {
            btnChoose.Selected = false;
            btnDel.Selected = false;
            btnEdit.Selected = false;
        }
        
        private void btnChoose_Click(object sender, EventArgs e)
        {
            NoSelected();
            btnChoose.Selected = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            NoSelected();
            btnEdit.Selected = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            NoSelected();
            btnDel.Selected = true;
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            int res = 1;
            int.TryParse(txtExpire.Text.Trim(), out res);
            _iFace.Expire = 60 * res;
        }
    }
}

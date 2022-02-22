using ArcSoftFace.Chart.Menu;
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
    public partial class MenuLibrary : UIPage
    {
        private DataTable dt;
        /// <summary>
        /// 界面菜单分类名字对应数据库真实ID
        /// </summary>
        private Dictionary<string, int> displayToMenuClassifyId = new Dictionary<string, int>();

        private Dictionary<int, string> MenuIdToMenuName = new Dictionary<int, string>();
        List<MenuDto> datas = new List<MenuDto>();


        public MenuLibrary()
        {
            InitializeComponent();


            uiDataGridView1.AddColumn("序号", "Id");
            uiDataGridView1.AddColumn("菜名", "Name");
            uiDataGridView1.AddColumn("价格", "Price");
            uiDataGridView1.AddColumn("分类序号", "ClassifyId");
            uiDataGridView1.Columns[0].Visible = false;
            uiDataGridView1.Columns[3].Visible = false;
            uiDataGridView1.ReadOnly = true;
        }
        public override void Init()
        {
            base.Init();
            if (displayToMenuClassifyId.Count != 0)
                displayToMenuClassifyId.Clear();

            DataTable MenuClassify = SqlHelperUtil.ReadTableMenuClassify();
            for (int i = 0; i < MenuClassify.Rows.Count; i++)
            {
                displayToMenuClassifyId.Add(MenuClassify.Rows[i][1]?.ToString(), int.Parse(MenuClassify.Rows[i][0]?.ToString()));
            }
            //cbbSelectOption.DataSource = MenuClassify;
            //cbbSelectOption.DisplayMember = "ClassName";
            //cbbSelectOption.ValueMember = "Id";
            dt = SqlHelperUtil.ReadTableMenu();
            fleashDGV();

            //销量排行版刷新
             DataTable dtFirstMenu= SqlHelperUtil.ReadUserMenuFirst8();
            for (int i = 0; i < dtFirstMenu.Rows.Count; i++)
            {
                if (i == 8)
                    break;
                var id = int.Parse(dtFirstMenu.Rows[i][0].ToString());
                if (MenuIdToMenuName.ContainsKey(id))
                {
                    var con = uiPanel3.Controls.Find($"btnMenu{i + 1}", false);
                    var len = con.Length;
                    con[0].Text = MenuIdToMenuName[id];
                }
                uiPanel3.Controls.Find($"btnNumber{i + 1}", false)[0].Text = dtFirstMenu.Rows[i][1].ToString();
            }

        }
        private void fleashDGV()
        {
            datas.Clear();
            MenuIdToMenuName.Clear();

            labMenuSum.Text = $"共{dt.Rows.Count}菜品";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow r = dt.Rows[i];

                MenuDto menu = new MenuDto();
                menu.Id = int.Parse(r[0].ToString());
                menu.ClassifyId =int.Parse(r[1].ToString());
                menu.Name = r[2].ToString();
                menu.Price = double.Parse(r[3].ToString());
                datas.Add(menu);

                MenuIdToMenuName.Add(menu.Id, menu.Name);
            }

            uiPagination1.DataSource = datas;
            uiPagination1.ActivePage = 1;
        }
        private void uiPagination1_PageChanged(object sender, object pagingSource, int pageIndex, int count)
        {
            uiDataGridView1.DataSource = pagingSource;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tboxSearch.Text.Trim()))
                Init();
            string optionText = cbbSelectOption.SelectedItem?.ToString();
            string searchTxt = tboxSearch.Text.Trim();
            if(optionText!=null&& optionText == "全部")
            {
                dt = SqlHelperUtil.SelectMenuByClassifyOrName(MenuName: searchTxt);
                fleashDGV();
                return;
            }
            if (string.IsNullOrWhiteSpace(optionText))
            {
                dt = SqlHelperUtil.SelectMenuByClassifyOrName(MenuName:searchTxt);
                fleashDGV();
                return;
            }
            if (!displayToMenuClassifyId.ContainsKey(optionText))//非一般情况
                return;
            //正常查询
            dt = SqlHelperUtil.SelectMenuByClassifyOrName(displayToMenuClassifyId[optionText], searchTxt);
            fleashDGV();
        }
        

        private void btnFlash_Click(object sender, EventArgs e)
        {
            Init();
            ShowSuccessTip("刷新成功!");
        }

        private void uiDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (rowindex < 0)
                return;

            var id = uiDataGridView1.Rows[rowindex].Cells[0].Value?.ToString();
            var classifyId = uiDataGridView1.Rows[rowindex].Cells[1].Value?.ToString();
            if (btnChoose.Selected)
                return;
            else if (btnEdit.Selected)
            {
                var addMenu = new AddOrEditMenu();
                var name = uiDataGridView1.Rows[rowindex].Cells[2].Value?.ToString();
                DataTable menuDto = SqlHelperUtil.SelectMenuByClassifyOrName(MenuName: name);

                //简单点吧
                var classifyName = "";
                if (classifyId == "1")
                    classifyName = "主食";
                else if (classifyId == "2")
                {
                    classifyName = "凉菜";
                }
                else if (classifyId == "3")
                {
                    classifyName = "热菜";
                }
                else
                    classifyName = "汤类";
                addMenu.cbbMenuClassify.SelectedItem = classifyName;
                addMenu.Menu = new MenuDto()
                {
                    Id = int.Parse(menuDto.Rows[0][0].ToString()),
                    ClassifyId = int.Parse(menuDto.Rows[0][1].ToString()),
                    Name = name,
                    Price = double.Parse(menuDto.Rows[0][3].ToString())
                };
                addMenu.ShowDialog();
                if (addMenu.IsOK)
                {
                    if (SqlHelperUtil.UpdateMenu(addMenu.Menu) > 0)
                        ShowSuccessTip("修改成功！");
                    else
                        ShowErrorTip("修改失败！");
                }
                addMenu.Dispose();
            }
            else if (btnDel.Selected)
            {
                if (ShowAskDialog($"将要删除菜品{uiDataGridView1.Rows[rowindex].Cells[2].Value?.ToString()}吗？"))
                {

                    string sql = $"delete from [Menu] where Id='{id}'";
                    if (SqlHelperUtil.ExecuteNonQuery(sql, null) > 0)
                    {
                        ShowSuccessTip("删除成功！");
                    }
                    else
                    {
                        ShowErrorTip("删除失败！");
                    }
                }
            }
        }
        private void NoSelected()
        {
            btnChoose.Selected = false;
            btnAdd.Selected = false;
            btnDel.Selected = false;
            btnEdit.Selected = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            NoSelected();
            btnAdd.Selected = true;

            var addMenu = new AddOrEditMenu();
            addMenu.Menu = new MenuDto();
            addMenu.ShowDialog();
            if (addMenu.IsOK)
            {
                var classifyName = addMenu.cbbMenuClassify.SelectedItem.ToString();
                if (classifyName == "主食")
                    addMenu.Menu.ClassifyId = 1;
                else if (classifyName == "凉菜")
                    addMenu.Menu.ClassifyId = 2;
                else if (classifyName == "热菜")
                    addMenu.Menu.ClassifyId = 3;
                else if (classifyName == "汤类")
                    addMenu.Menu.ClassifyId = 4;
                if (SqlHelperUtil.AddMenu(addMenu.Menu) > 0)
                    ShowSuccessTip("添加成功！");
                else
                    ShowErrorTip("添加失败！");

            }
            addMenu.Dispose();
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

    }
}

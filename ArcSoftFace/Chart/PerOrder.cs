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
    public partial class PerOrder : UIEditForm
    {
        private string _iGuid;
        public float curSumPri = 0;
        public Dictionary<int, int> MenuIdToNumber = new Dictionary<int, int>();
        public DataTable dt=new DataTable();
        public DataTable res = new DataTable();

        public PerOrder(string guid)
        {
            this._iGuid = guid;
            InitializeComponent();

            dgvPerOrder.ReadOnly = true;
            dgvHistoryHeader.ReadOnly = true;
            LoadMenu();
            LoadHistoryHeader();

            dgvPerOrder.DataSource = dt;
            //菜单Id不显示
            dgvPerOrder.Columns[0].Visible = false;

            //不显示第一列菜单id
            dgvHistoryHeader.DataSource = res;
            dgvHistoryHeader.Columns[0].Visible = false;
            //dgvHistoryHeader.Columns[1].DisplayIndex = 1;
            //dgvHistoryHeader.Columns[2].DisplayIndex = 2;
        }
        public override void Init()
        {
            base.Init();
        }
        public void LoadMenu()
        {
            //MenuId ,classifyId,Name,Price
            DataTable menuDt = SqlHelperUtil.ReadTableMenu();
            DataTable perMenu = SqlHelperUtil.ReadTableUserMenu(_iGuid);
            DataColumn col = new DataColumn("MenuId", Type.GetType("System.Int32", false));
            DataColumn col1 = new DataColumn("菜名", Type.GetType("System.String"));
            DataColumn col2 = new DataColumn("价格", Type.GetType("System.String",false));
            DataColumn col3 = new DataColumn("总点菜次数", Type.GetType("System.Int32", false));
            DataColumn col4 = new DataColumn("添加数量", Type.GetType("System.Int32", false));
            dt.Columns.Add(col);
            dt.Columns.Add(col1);
            dt.Columns.Add(col2);
            dt.Columns.Add(col3);
            dt.Columns.Add(col4);
            for (int i = 0,j=0; i < menuDt.Rows.Count; i++)
            {
                var menuRow = menuDt.Rows[i];
                if (j >= perMenu.Rows.Count)
                {
                    var r = dt.NewRow();
                    r[0] = menuRow[0];
                    r[1] = menuRow[2];
                    r[2] = menuRow[3];
                    r[3] = 0;
                    r[4] = 0;
                    dt.Rows.Add(r);
                    continue;
                }
                int MenuId = int.Parse(menuDt.Rows[i][0].ToString());
                int PerMenuId = int.Parse(perMenu.Rows[j][0].ToString());
                if (MenuId <= PerMenuId)
                {
                    if (MenuId != PerMenuId)
                    {
                        var r = dt.NewRow();
                        r[0] = menuRow[0];
                        r[1] = menuRow[2];
                        r[2] = menuRow[3];
                        r[3] = 0;
                        r[4] = 0;
                        dt.Rows.Add(r);
                    }
                    else
                    {
                        var r = dt.NewRow();
                        r[0] = menuRow[0];
                        r[1] = menuRow[2];
                        r[2] = menuRow[3];
                        r[3] = perMenu.Rows[j++][1];
                        r[4] = 0;
                        dt.Rows.Add(r);
                    }
                }
                //当前个人菜单id已不存在
                else
                {
                    j++;
                    i--;
                }
                
            }
        }
        private void LoadHistoryHeader()
        {
            //列  菜单id  历史消费该菜总计(desc)
            DataTable dtHisMenu= SqlHelperUtil.SelectUserHistoryMenuHeader(_iGuid);

            DataColumn col1 = new DataColumn("菜名Id", Type.GetType("System.String"));
            DataColumn col2 = new DataColumn("推荐菜单",Type.GetType("System.String"));
            DataColumn col3 = new DataColumn("历史消费总价", Type.GetType("System.String"));
            res.Columns.Add(col1);
            res.Columns.Add(col2);
            res.Columns.Add(col3);
            for (int i = 0; i < dtHisMenu.Rows.Count; i++)
            {
                DataRow r = res.NewRow();
                int menuId = int.Parse(dtHisMenu.Rows[i][0].ToString());
                bool flag = false;
                r[0] = menuId;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (int.Parse(dt.Rows[j][0].ToString()) == menuId)
                    {
                        r[1] = dt.Rows[j][1];
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    continue;
                r[2] = dtHisMenu.Rows[i][1].ToString();
                res.Rows.Add(r);
            }
        }
        private void dgvPerOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (rowindex < 0)
                return;
            if (btnChoose.Selected)
                return;
            else if (btnAdd.Selected)
            {
                curSumPri = float.Parse(labSumPrice.Text.Trim().ToString());


                dt.Rows[rowindex][4] = int.Parse(dt.Rows[rowindex][4].ToString())+1;
                curSumPri += int.Parse(dt.Rows[rowindex][2].ToString());
                labSumPrice.Text = curSumPri.ToString();

                var id = int.Parse(dt.Rows[rowindex][0].ToString());
                var num = int.Parse(dt.Rows[rowindex][4].ToString());
                if (MenuIdToNumber.ContainsKey(id))
                {
                    MenuIdToNumber[id] = MenuIdToNumber[id] + num;
                }
                else
                {
                    MenuIdToNumber.Add(id, num);
                }

            }
            else if (btnMinus.Selected)
            {
                curSumPri = float.Parse(labSumPrice.Text.Trim().ToString());
                if (int.Parse(dt.Rows[rowindex][4].ToString()) == 0)
                    return;
                dt.Rows[rowindex][4] = int.Parse(dt.Rows[rowindex][4].ToString()) - 1;
                curSumPri -= int.Parse(dt.Rows[rowindex][2].ToString());
                labSumPrice.Text = curSumPri.ToString();
            }
        }
        private void NoSelected()
        {
            btnChoose.Selected = false;
            btnAdd.Selected = false;
            btnMinus.Selected = false;
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            NoSelected();
            btnChoose.Selected = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NoSelected();
            btnAdd.Selected = true;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            NoSelected();
            btnMinus.Selected = true;
        }
    }
}



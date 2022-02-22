using ArcSoftFace.Model;
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
using static ArcSoftFace.Chart.MenuLibrary;

namespace ArcSoftFace.Chart.Menu
{
    public partial class AddOrEditMenu : UIEditForm
    {
        private Dictionary<int,string> classifyNameToId = new Dictionary<int,string>();
        public AddOrEditMenu()
        {
            InitializeComponent();
            classifyNameToId.Add(1,"主食");
            classifyNameToId.Add(2,"凉菜");
            classifyNameToId.Add(3,"热菜");
            classifyNameToId.Add(4,"汤类");
        }
        protected override bool CheckData()
        {
            return CheckEmpty(txtMenuName, "请输入菜单名称")
                && CheckEmpty(cbbMenuClassify, "请选择菜单类型")
                   && CheckEmpty(txtMenuPrice, "请输入菜单价格")
                   && CheckRange(txtMenuPrice, 0, 300, "输入价格范围0~300");
        }

        private MenuDto menu;

        public MenuDto Menu
        {
            get
            {
                if (menu == null)
                {
                    menu = new MenuDto();
                }
                menu.Name = txtMenuName.Text;
                menu.Price = txtMenuPrice.DoubleValue;
                return menu;
            }

            set
            {
                menu = value;
                if(value.ClassifyId!=0)
                    cbbMenuClassify.SelectedItem = classifyNameToId[value.ClassifyId];
                txtMenuName.Text = value.Name;
                txtMenuPrice.DoubleValue = value.Price;
            }
        }


    }
}

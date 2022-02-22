using ArcSoftFace.Chart;
using FaceUI.Chart;
using Sunny.UI;
using System.Drawing;
using System.Windows.Forms;

namespace ArcSoftFace
{
    public partial class FaceUI : UIAsideMainFrame
    {
        public static FaceForm face = new FaceForm();
        public static SystemService service; //用来显示人脸库(先不显示)
        public FaceUI()
        {
            InitializeComponent();
            //Aside.BackColor = Color.DodgerBlue;

            service = new SystemService(face);

            Aside.CreateNode(AddPage(face),57507,24);

            Aside.CreateNode(AddPage(new FaceLibrary(face)),61447,24);

            Aside.CreateNode(AddPage(new MenuLibrary()),61474,24);
            TreeNode parent = Aside.CreateNode("数据分析", 61950, 24,3);
            Aside.CreateChildNode(parent, 24, AddPage(new FBarChart()));
            Aside.CreateChildNode(parent, 24, AddPage(new FPieChart()));
            //Aside.CreateChildNode(parent, 24, AddPage(new OrderHistory()));
            Aside.CreateChildNode(parent, 24, AddPage(new History()));

            TreeNode node= Aside.CreateNode(AddPage(service),57460,24);
           
        }

        private void FaceUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            face.Form_Closed(null, null);
        }
    }
}

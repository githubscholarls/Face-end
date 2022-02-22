using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArcSoftFace.SDKModels;
using ArcSoftFace.SDKUtil;
using ArcSoftFace.Utils;
using ArcSoftFace.Entity;
using System.IO;
using System.Configuration;
using System.Threading;
using AForge.Video.DirectShow;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ArcSoftFace.Model;
using Sunny.UI;
using System.Diagnostics;
using static ArcSoftFace.YNFaceDetector;
using StackExchange.Redis;
using System.Data;
using System.Drawing.Imaging;

namespace ArcSoftFace
{
    public delegate string mydele(object sender, EventArgs e);
    public class myclass
    {
        public myclass()
        {
            FaceForm fa = new FaceForm();
            //事件不能在类的外部赋值操作，只能add和remove
            //fa.mydeldes = null;
            fa.mydeldes += (s, e) => "";
        }
    }
    public partial class FaceForm : UIPage
    {
        //protected UINavMenu Aside;
        //protected UINavBar Header;
        #region 参数定义
        /// <summary>
        /// 引擎Handle
        /// </summary>
        private IntPtr pImageEngine = IntPtr.Zero;

        /// <summary>
        /// 注册时候引擎Handle
        /// </summary>
        private IntPtr pImageRegisterEngine = IntPtr.Zero;
        /// <summary>
        /// 保存右侧图片路径
        /// </summary>
        private string image1Path;

        /// <summary>
        /// 图片最大大小
        /// </summary>
        private long maxSize = 1024 * 1024 * 2;

        /// <summary>
        /// 右侧图片人脸特征
        /// </summary>
        private IntPtr image1Feature;

        /// <summary>
        /// 保存对比图片的列表
        /// </summary>
        private List<string> imagePathList = new List<string>();

        /// <summary>
        /// 左侧图库人脸特征列表
        /// </summary>
        private List<IntPtr> imagesFeatureList = new List<IntPtr>();
        /// <summary>
        /// 根据人脸特征存一些用户信息
        /// </summary>
        private Dictionary<IntPtr, UserDto> featerUserDto = new Dictionary<IntPtr, UserDto>();

        /// <summary>
        /// 相似度
        /// </summary>
        private float threshold = 0.8f;

        /// <summary>
        /// 用于标记是否需要清除比对结果
        /// </summary>
        private bool isCompare = false;

        /// <summary>
        /// 是否是双目摄像
        /// </summary>
        private bool isDoubleShot = false;

        /// <summary>
        /// 允许误差范围
        /// </summary>
        private int allowAbleErrorRange = 40;

        /// <summary>
        /// RGB 摄像头索引
        /// </summary>
        private int rgbCameraIndex = 0;
        /// <summary>
        /// IR 摄像头索引
        /// </summary>
        private int irCameraIndex = 0;

        /// <summary>
        /// image基路径
        /// </summary>
        private string ImagePath;

        /// <summary>
        /// image下所有人脸路径(数据库中有的)
        /// </summary>
        private List<string> ImagePaths = new List<string>();

        /// <summary>
        /// 数据库下所有的ImageGuid
        /// </summary>
        private List<string> ImageGuids = new List<string>();

        /// <summary>
        /// 人脸3d角度最大的值(注册时候需要满足的值)
        /// </summary>
        private double Reg3Dmax = 20;

        /// <summary>
        /// 更改信息时候不查询用户信息
        /// </summary>
        private bool isCanSelect = true;

        /// <summary>
        /// 人脸遮挡引擎
        /// </summary>
        YNFaceDetector yn = new YNFaceDetector();

        /// <summary>
        /// 允许同时存在100个人脸
        /// </summary>
        private List<string> _rgbMessage = new List<string>();


        private String bath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\\bin"));

        private int expire = 20;
        public int Expire { get { return expire; }
            set {
                if (value < 20)
                    expire = 60;
                else
                    expire = value;
            }
        }

        /// <summary>
        /// redis缓存 代替文本缓存
        /// </summary>
        public ConnectionMultiplexer redis;
        /// <summary>
        /// 做消费时间间隔
        /// </summary>
        IDatabase db;
        /// <summary>
        /// 做历史记录，不会过期
        /// </summary>
        //IDatabase dbNoExpire;

        #region 视频模式下相关
        /// <summary>
        /// 视频引擎Handle
        /// </summary>
        private IntPtr pVideoEngine = IntPtr.Zero;
        private IntPtr pVideoEngine2 = IntPtr.Zero;
        /// <summary>
        /// RGB视频引擎 FR Handle 处理   FR和图片引擎分开，减少强占引擎的问题
        /// </summary>
        private IntPtr pVideoRGBImageEngine = IntPtr.Zero;
        /// <summary>
        /// IR视频引擎 FR Handle 处理   FR和图片引擎分开，减少强占引擎的问题
        /// </summary>
        private IntPtr pVideoIRImageEngine = IntPtr.Zero;
        /// <summary>
        /// 视频输入设备信息
        /// </summary>
        private FilterInfoCollection filterInfoCollection;
        /// <summary>
        /// RGB摄像头设备
        /// </summary>
        private VideoCaptureDevice rgbDeviceVideo;
        /// <summary>
        /// IR摄像头设备
        /// </summary>
        private VideoCaptureDevice irDeviceVideo;
        #endregion

        #endregion

        #region 初始化
        public FaceForm()
        {
            InitializeComponent();


            CheckForIllegalCrossThreadCalls = false;
            //初始化引擎
            InitEngines();

            //ShowSuccessTip("引擎初始完毕", delay: 2000);
            //隐藏摄像头图像窗口
            rgbVideoSource.Hide();
            irVideoSource.Hide();
            //阈值控件不可用
            txtThreshold.Enabled = false;

            ImagePath = AppDomain.CurrentDomain.BaseDirectory;
            ImagePath = ImagePath.Substring(0, ImagePath.LastIndexOf("\\bin")) + "\\Image\\";

            //人脸遮挡
            LoadFaceModel();
            ShowSuccessTip("加载人脸模型完毕", delay: 2000);
            //加载持久化的消费记录（三小时内不允许再次消费）
            //LoadPerRegisCache();
            redis = ConnectionMultiplexer.Connect("localhost");
            db = redis.GetDatabase();
            ShowSuccessTip("redis服务已连接", delay: 2000);
            //dbNoExpire = redis.GetDatabase(1);

            //db.StringSet("123", "3");
            //db.KeyExpire("123", DateTime.Now.AddSeconds(2));
            //Console.WriteLine(db.StringGet("123"));
            //Thread.Sleep(2);
            //if (!db.KeyExists("123"))
            //    db.StringSet("123", "6");

            //Console.WriteLine(db.StringGet("123"));

            LoadDataReflash();
        }

        private void LoadUserHistory()
        {

            DataTable dt = SqlHelperUtil.ReadTableUserHistory();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

            }
        }

        /// <summary>
        /// 加载人脸遮挡模型
        /// </summary>
        private void LoadFaceModel()
        {
            var bath = AppDomain.CurrentDomain.BaseDirectory;
            bath = bath.Substring(0, bath.LastIndexOf("\\bin"));
            bath = bath + "\\models\\yn_model_detect.tar";
            var yNLoadRes = yn.loadModels(bath);
            if (yNLoadRes != 0)
                MessageBox.Show("加载人脸模型失败!");
        }


        /// <summary>
        /// 加载数据库数据并刷新界面
        /// </summary>
        private void LoadDataReflash()
        {
            ImageGuids.Clear();
            ImagePaths.Clear();
            imageList.Clear();
            //ss
            //FaceUI.service.imageList.Clear();


            ImageGuids = SqlHelperUtil.ReadGuidPathList();
            for (int i = 0; i < ImageGuids.Count; i++)
            {
                ImagePaths.Add(ImagePath + ImageGuids[i] + ".jpg");
            }
            //将image路径下图片加载到左侧
            LoadLeftImageNotSelectSql();
        }

        /// <summary>
        /// 初始化引擎
        /// </summary>
        private void InitEngines()
        {
            //读取配置文件
            AppSettingsReader reader = new AppSettingsReader();
            string appId = (string)reader.GetValue("APP_ID", typeof(string));
            string sdkKey64 = (string)reader.GetValue("SDKKEY64", typeof(string));
            string sdkKey32 = (string)reader.GetValue("SDKKEY32", typeof(string));
            rgbCameraIndex = (int)reader.GetValue("RGB_CAMERA_INDEX", typeof(int));
            irCameraIndex = (int)reader.GetValue("IR_CAMERA_INDEX", typeof(int));
            //判断CPU位数
            var is64CPU = Environment.Is64BitProcess;
            if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(is64CPU ? sdkKey64 : sdkKey32))
            {
                //禁用相关功能按钮
                ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                MessageBox.Show(string.Format("请在App.config配置文件中先配置APP_ID和SDKKEY{0}!", is64CPU ? "64" : "32"));
                return;
            }

            //在线激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
            int retCode = 0;
            try
            {
                retCode = ASFFunctions.ASFActivation(appId, is64CPU ? sdkKey64 : sdkKey32);
            }
            catch (Exception ex)
            {
                //禁用相关功能按钮
                ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                if (ex.Message.Contains("无法加载 DLL"))
                {
                    MessageBox.Show("请将sdk相关DLL放入bin对应的x86或x64下的文件夹中!");
                }
                else
                {
                    MessageBox.Show("激活引擎失败!");
                }
                return;
            }
            Console.WriteLine("Activate Result:" + retCode);

            //初始化引擎
            uint detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            //Video模式下检测脸部的角度优先值
            int videoDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            //Image模式下检测脸部的角度优先值
            int imageDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_ONLY;


            //人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
            int detectFaceScaleVal = 16;
            //最大需要检测的人脸个数
            int detectFaceMaxNum = 5;
            //引擎初始化时需要初始化的检测功能组合
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pImageEngine);





            Console.WriteLine("InitEngine Result:" + retCode);
            AppendText((retCode == 0) ? "引擎初始化成功!\n" : string.Format("引擎初始化失败!错误码为:{0}\n", retCode));

            if (retCode != 0)
            {
                //禁用相关功能按钮
                ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
            }

            //初始化视频模式下人脸检测引擎
            uint detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
            int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION;
            var retCode1 = ASFFunctions.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMaskVideo, ref pVideoEngine);

            var retCode2 = ASFFunctions.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMaskVideo, ref pVideoEngine2);


            //RGB视频专用FR引擎
            detectFaceMaxNum = 1;
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS;
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoRGBImageEngine);

            //IR视频专用FR引擎
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_IR_LIVENESS;
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoIRImageEngine);

            Console.WriteLine("InitVideoEngine Result:" + retCode);
            //MessageBox.Show($"InitVideoEngine Result:" + retCode+$"pvideoEngine:{retCode1},pvideoEngine:{retCode2}");


            initVideo();
        }

        /// <summary>
        /// 摄像头初始化
        /// </summary>
        private void initVideo()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            //如果没有可用摄像头，“启用摄像头”按钮禁用，否则使可用
            if (filterInfoCollection.Count == 0)
            {
                btnStartVideo.Enabled = false;
            }
            else
            {
                btnStartVideo.Enabled = true;
            }
        }

        #endregion

        #region 注册人脸按钮事件
        public int publicVal { get; set; }
        public event mydele mydeldes;
       
        public void ev()
        {
            mydeldes +=(o,e)=>"";
            mydele sto = (o,e) =>"";
            mydeldes += sto;
            mydeldes = sto;

        }


        private object locker = new object();
        /// <summary>
        /// 人脸库图片选择按钮事件
        /// </summary>
        public void ChooseMultiImg(object sender, EventArgs e)
        {
            lock (locker)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "选择图片";
                openFileDialog.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png";
                openFileDialog.Multiselect = true;
                openFileDialog.FileName = string.Empty;
                imageList.Refresh();
                //ss
                //FaceUI.service.imageList.Refresh();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    List<string> imagePathListTemp = new List<string>();
                    var numStart = imagePathList.Count;
                    int isGoodImage = 0;

                    //保存图片路径并显示
                    string[] fileNames = openFileDialog.FileNames;
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        //图片格式判断
                        if (checkImage(fileNames[i]))
                        {
                            imagePathListTemp.Add(fileNames[i]);
                        }
                    }
                    //人脸检测以及提取人脸特征
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        //禁止点击按钮
                        Invoke(new Action(delegate
                        {
                            chooseMultiImgBtn.Enabled = false;
                            matchBtn.Enabled = false;
                            btnClearFaceList.Enabled = false;
                            chooseImgBtn.Enabled = false;
                            btnStartVideo.Enabled = false;
                        }));

                        string errinfo = "";
                        //人脸检测和剪裁
                        for (int i = 0; i < imagePathListTemp.Count; i++)
                        {

                            User user = new User();
                            Image cutImage = null;
                            Image image = ImageUtil.readFromFile(imagePathListTemp[i]);
                            if (image == null)
                            {
                                continue;
                            }
                            if (image.Width > 1536 || image.Height > 1536)
                            {
                                image = ImageUtil.ScaleImage(image, 1536, 1536);
                            }
                            if (image == null)
                            {
                                continue;
                            }
                            if (image.Width % 4 != 0)
                            {
                                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
                            }
                            //人脸检测
                            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, image);
                            //判断检测结果
                            if (multiFaceInfo.faceNum > 0)
                            {
                                //检测是不是正脸照
                                D3Detect(imagePathListTemp[i],ref user);

                                //人脸信息不可信或信息不严格
                                if (user.State == false)
                                {
                                    errinfo += imagePathListTemp[i] + ",";
                                    continue;
                                }


                                #region 过滤已存在人脸特征
                                ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
                                Image imagefeture = ImageUtil.readFromFile(imagePathListTemp[i]);
                                if (imagefeture == null)
                                {
                                    continue;
                                }
                                try
                                {
                                    var oldImg = new Bitmap(imagefeture);
                                    Bitmap img = new Bitmap(oldImg);
                                    Graphics draw = Graphics.FromImage(img);
                                    draw.DrawImage(oldImg, 0, 0, oldImg.Width, oldImg.Height);
                                    oldImg.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                                IntPtr feature = FaceUtil.ExtractFeature(pImageEngine, imagefeture, out singleFaceInfo);

                                if (singleFaceInfo.faceRect.left == 0 && singleFaceInfo.faceRect.right == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    float similarity;
                                    //得到比对结果
                                    int result = compareFeature(feature, out similarity);
                                    //有此用户
                                    if (result > -1)
                                    {
                                        MessageBox.Show(/*imagePathListTemp[i] + "图片中*/"系统中已存在该用户!  请勿重复添加！");
                                        continue;
                                    }
                                }
                                #endregion

                                //若尚未存在此用户
                                imagesFeatureList.Add(feature);
                                if (imagefeture != null)
                                {
                                    imagefeture.Dispose();
                                }

                                imagePathList.Add(imagePathListTemp[i]);
                                MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
                                image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
                                //为保存到image下做记录
                                cutImage = new Bitmap(image);
                            }
                            else
                            {
                                if (image != null)
                                {
                                    image.Dispose();
                                }
                                continue;
                            }
                            //显示人脸
                            this.Invoke(new Action(delegate
                            {
                                if (image == null)
                                {
                                    image = ImageUtil.readFromFile(imagePathListTemp[i]);

                                    if (image.Width > 1536 || image.Height > 1536)
                                    {
                                        image = ImageUtil.ScaleImage(image, 1536, 1536);
                                    }
                                }

                                //裁剪后改名字保存本地
                                string guid = Guid.NewGuid().ToString("N");
                                cutImage.Save(ImagePath + guid + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                //将图片在image文件夹下的名称注册到数据库
                                SqlHelperUtil.AddUser(new User()
                                {
                                    Sex = user.Sex,
                                    Age = user.Age,
                                    ImagePath = guid,
                                    RegisterTime=DateTime.Now,
                                    LastLoginTime = DateTime.Now,
                                    State = true
                                });
                                //添加历史记录
                                SqlHelperUtil.AddLoginTimes(guid);
                                SqlHelperUtil.AddUserHistory(guid,DateTime.Now);
                                ListViewItem item = new ListViewItem();
                                item.Text = (numStart + isGoodImage + 1) + "号";
                                item.Name = guid;
                                item.ImageKey = imagePathListTemp[i];
                                ImagePaths.Add(ImagePath + guid + ".jpg");

                                imageLists.Images.Add(imagePathListTemp[i], image);
                                imageList.Items.Add(item);
                                imageList.Refresh();

                                //ss
                                //Image image1 = (Image)image.Clone();
                                //FaceUI.service.imageLists.Images.Add(imagePathListTemp[i], image1);
                                //FaceUI.service.imageList.Items.Add(item);
                                //FaceUI.service.Refresh();
                                //并刷新本地guidPaths（不然修改人信息会报错(修改时候没查数据库))
                                ImageGuids = SqlHelperUtil.ReadGuidPathList();
                                ImagePaths.Clear();
                                for (int j = 0; j < ImageGuids.Count; j++)
                                {
                                    ImagePaths.Add(ImagePath + ImageGuids[j] + ".jpg");
                                }
                                isGoodImage += 1;
                                if (image != null)
                                {
                                    image.Dispose();
                                }
                            }));
                        }

                        if(errinfo!="") 
                            MessageBox.Show(errinfo + "，等文件中人脸不是正脸照，不能录入系统！");
                        
                        //允许点击按钮
                        Invoke(new Action(delegate
                        {
                            chooseMultiImgBtn.Enabled = true;
                            btnClearFaceList.Enabled = true;
                            btnStartVideo.Enabled = true;

                            if (btnStartVideo.Text == "启用摄像头")
                            {
                                chooseImgBtn.Enabled = true;
                                matchBtn.Enabled = true;
                            }
                            else
                            {
                                chooseImgBtn.Enabled = false;
                                matchBtn.Enabled = false;
                            }
                        }));
                    }));

                }
            }
        }
        #endregion

        #region 批量添加图片用户,3D检测，只让显示正脸的注册
        /// <summary>
        /// 3D检测，并返回检测对象
        /// </summary>
        /// <param name="imagelPath"></param>
        /// <param name="user"></param>
        public void D3Detect(string path,ref User user)
        {
            //获取文件，拒绝过大的图片
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Length > maxSize)
            {
                return;
            }

            Image srcImage = ImageUtil.readFromFile(path);
            if (srcImage == null)
            {
                return;
            }
            if (srcImage.Width > 1536 || srcImage.Height > 1536)
            {
                srcImage = ImageUtil.ScaleImage(srcImage, 1536, 1536);
            }
            if (srcImage == null)
            {
                return;
            }
            //调整图像宽度，需要宽度为4的倍数
            if (srcImage.Width % 4 != 0)
            {
                srcImage = ImageUtil.ScaleImage(srcImage, srcImage.Width - (srcImage.Width % 4), srcImage.Height);
            }
            //调整图片数据，非常重要
            ImageInfo imageInfo = ImageUtil.ReadBMP(srcImage);
            if (imageInfo == null)
            {
                return;
            }
            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, imageInfo);
            //年龄检测
            int retCode_Age = -1;
            ASF_AgeInfo ageInfo = FaceUtil.AgeEstimation(pImageEngine, imageInfo, multiFaceInfo, out retCode_Age);
            //性别检测
            int retCode_Gender = -1;
            ASF_GenderInfo genderInfo = FaceUtil.GenderEstimation(pImageEngine, imageInfo, multiFaceInfo, out retCode_Gender);

            //3DAngle检测
            int retCode_3DAngle = -1;
            ASF_Face3DAngle face3DAngleInfo = FaceUtil.Face3DAngleDetection(pImageEngine, imageInfo, multiFaceInfo, out retCode_3DAngle);

            MemoryUtil.Free(imageInfo.imgData);
            if (multiFaceInfo.faceNum < 1)
            {
                srcImage = ImageUtil.ScaleImage(srcImage, picImageCompare.Width, picImageCompare.Height);
                image1Feature = IntPtr.Zero;
                picImageCompare.Image = srcImage;
                return;
            }

            MRECT temp = new MRECT();
            int ageTemp = 0;
            int genderTemp = 0;
            int rectTemp = 0;
            float r = 10;
            float p = 10;
            float y = 10;
            int status = -1;

            //标记出检测到的人脸
            for (int i = 0; i < multiFaceInfo.faceNum; i++)
            {
                MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryUtil.SizeOf<MRECT>() * i);
                int orient = MemoryUtil.PtrToStructure<int>(multiFaceInfo.faceOrients + MemoryUtil.SizeOf<int>() * i);
                int age = 0;

                if (retCode_Age != 0)
                {
                }
                else
                {
                    age = MemoryUtil.PtrToStructure<int>(ageInfo.ageArray + MemoryUtil.SizeOf<int>() * i);
                }

                int gender = -1;
                if (retCode_Gender != 0)
                {
                }
                else
                {
                    gender = MemoryUtil.PtrToStructure<int>(genderInfo.genderArray + MemoryUtil.SizeOf<int>() * i);
                }

                int face3DStatus = -1;
                float roll = 0f;
                float pitch = 0f;
                float yaw = 0f;
                if (retCode_3DAngle != 0)
                {
                }
                else
                {
                    //角度状态 非0表示人脸不可信
                    face3DStatus = MemoryUtil.PtrToStructure<int>(face3DAngleInfo.status + MemoryUtil.SizeOf<int>() * i);
                    //roll为侧倾角，pitch为俯仰角，yaw为偏航角
                    roll = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.roll + MemoryUtil.SizeOf<float>() * i);
                    pitch = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.pitch + MemoryUtil.SizeOf<float>() * i);
                    yaw = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.yaw + MemoryUtil.SizeOf<float>() * i);
                }

                int rectWidth = rect.right - rect.left;
                int rectHeight = rect.bottom - rect.top;

                //查找最大人脸
                if (rectWidth * rectHeight > rectTemp)
                {
                    rectTemp = rectWidth * rectHeight;
                    temp = rect;
                    ageTemp = age;
                    genderTemp = gender;

                    r = roll;
                    p = pitch;
                    y = yaw;
                    status = face3DStatus;

                }
            }
            //可信人脸,符合要求
            if(status==0&&Math.Abs(r)< Reg3Dmax && Math.Abs(p) < Reg3Dmax && Math.Abs(y) < Reg3Dmax)
            {
                user.Age = ageTemp;
                user.Sex = genderTemp == 0 ? true : false;
                user.State = true;
            }
        }
        #endregion






        #region 清空人脸库按钮事件
        /// <summary>
        /// 清除人脸库事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnClearFaceList_Click(object sender, EventArgs e)
        {
            //防止多次点击发送死锁，不能再识别和注册用户
            isfeaLock = false;


            ////清空数据库
            SqlHelperUtil.DelDatas();

            ////删除从数据库取来的在iamge文件夹的图像
            for (int i = 0; i < ImagePaths.Count; i++)
            {
                string fileFullPath = ImagePaths[i];
                // 1、首先判断文件或者文件路径是否存在
                if (File.Exists(fileFullPath))
                {
                    // 2、根据路径字符串判断是文件还是文件夹
                    FileAttributes attr = File.GetAttributes(fileFullPath);
                    // 3、根据具体类型进行删除
                    if (attr == FileAttributes.Directory)
                    {
                        // 3.1、删除文件夹
                        Directory.Delete(fileFullPath, true);
                    }
                    else
                    {
                        // 3.2、删除文件
                        File.Delete(fileFullPath);
                    }
                }

            }


            foreach (IntPtr intptr in imagesFeatureList)
            {
                MemoryUtil.Free(intptr);
            }
            imagesFeatureList.Clear();
            imagePathList.Clear();
            ImagePaths.Clear();

            if(imageLists.Images.Count!=0)
            //清除左侧界面数据
                imageLists.Images.Clear();
            if (imageList.Items.Count != 0)
                imageList.Items.Clear();

            //ss
            //if (FaceUI.service.imageLists.Images.Count != 0)
            //    FaceUI.service.imageLists.Images.Clear();
            //if (FaceUI.service.imageList.Items.Count != 0)
            //    FaceUI.service.imageList.Items.Clear();

            ShowSuccessNotifier("人脸库清除成功！");
        }
        #endregion

        #region 选择识别图按钮事件
        /// <summary>
        /// “选择识别图片”按钮事件
        /// </summary>
        private void ChooseImg(object sender, EventArgs e)
        {
            lblCompareInfo.Text = "";
            //判断引擎是否初始化成功
            if (pImageEngine == IntPtr.Zero)
            {
                //禁用相关功能按钮
                ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                MessageBox.Show("请先初始化引擎!");
                return;
            }
            //选择图片
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择图片";
            openFileDialog.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog.Multiselect = false;
            openFileDialog.FileName = string.Empty;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                image1Path = openFileDialog.FileName;
                //检测图片格式
                if (!checkImage(image1Path))
                {
                    return;
                }
                DateTime detectStartTime = DateTime.Now;
                AppendText(string.Format("------------------------------开始检测，时间:{0}------------------------------\n", detectStartTime.ToString("yyyy-MM-dd HH:mm:ss:ms")));

                //获取文件，拒绝过大的图片
                FileInfo fileInfo = new FileInfo(image1Path);
                if (fileInfo.Length > maxSize)
                {
                    MessageBox.Show("图像文件最大为2MB，请压缩后再导入!");
                    AppendText(string.Format("------------------------------检测结束，时间:{0}------------------------------\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    AppendText("\n");
                    return;
                }

                Image srcImage = ImageUtil.readFromFile(image1Path);
                if (srcImage == null)
                {
                    MessageBox.Show("图像数据获取失败，请稍后重试!");
                    AppendText(string.Format("------------------------------检测结束，时间:{0}------------------------------\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    AppendText("\n");
                    return;
                }
                if (srcImage.Width > 1536 || srcImage.Height > 1536)
                {
                    srcImage = ImageUtil.ScaleImage(srcImage, 1536, 1536);
                }
                if (srcImage == null)
                {
                    MessageBox.Show("图像数据获取失败，请稍后重试!");
                    AppendText(string.Format("------------------------------检测结束，时间:{0}------------------------------\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    AppendText("\n");
                    return;
                }
                //调整图像宽度，需要宽度为4的倍数
                if (srcImage.Width % 4 != 0)
                {
                    srcImage = ImageUtil.ScaleImage(srcImage, srcImage.Width - (srcImage.Width % 4), srcImage.Height);
                }
                //调整图片数据，非常重要
                ImageInfo imageInfo = ImageUtil.ReadBMP(srcImage);
                if (imageInfo == null)
                {
                    MessageBox.Show("图像数据获取失败，请稍后重试!");
                    AppendText(string.Format("------------------------------检测结束，时间:{0}------------------------------\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    AppendText("\n");
                    return;
                }
                //人脸检测
                ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, imageInfo);
                //年龄检测
                int retCode_Age = -1;
                ASF_AgeInfo ageInfo = FaceUtil.AgeEstimation(pImageEngine, imageInfo, multiFaceInfo, out retCode_Age);
                //性别检测
                int retCode_Gender = -1;
                ASF_GenderInfo genderInfo = FaceUtil.GenderEstimation(pImageEngine, imageInfo, multiFaceInfo, out retCode_Gender);

                //3DAngle检测
                int retCode_3DAngle = -1;
                ASF_Face3DAngle face3DAngleInfo = FaceUtil.Face3DAngleDetection(pImageEngine, imageInfo, multiFaceInfo, out retCode_3DAngle);

                MemoryUtil.Free(imageInfo.imgData);

                if (multiFaceInfo.faceNum < 1)
                {
                    srcImage = ImageUtil.ScaleImage(srcImage, picImageCompare.Width, picImageCompare.Height);
                    image1Feature = IntPtr.Zero;
                    picImageCompare.Image = srcImage;
                    AppendText(string.Format("{0} - 未检测出人脸!\n\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                    AppendText(string.Format("------------------------------检测结束，时间:{0}------------------------------\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    AppendText("\n");
                    return;
                }

                MRECT temp = new MRECT();
                int ageTemp = 0;
                int genderTemp = 0;
                int rectTemp = 0;

                //标记出检测到的人脸
                for (int i = 0; i < multiFaceInfo.faceNum; i++)
                {
                    MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryUtil.SizeOf<MRECT>() * i);
                    int orient = MemoryUtil.PtrToStructure<int>(multiFaceInfo.faceOrients + MemoryUtil.SizeOf<int>() * i);
                    int age = 0;

                    if (retCode_Age != 0)
                    {
                        AppendText(string.Format("年龄检测失败，返回{0}!\n\n", retCode_Age));
                    }
                    else
                    {
                        age = MemoryUtil.PtrToStructure<int>(ageInfo.ageArray + MemoryUtil.SizeOf<int>() * i);
                    }

                    int gender = -1;
                    if (retCode_Gender != 0)
                    {
                        AppendText(string.Format("性别检测失败，返回{0}!\n\n", retCode_Gender));
                    }
                    else
                    {
                        gender = MemoryUtil.PtrToStructure<int>(genderInfo.genderArray + MemoryUtil.SizeOf<int>() * i);
                    }

                    int face3DStatus = -1;
                    float roll = 0f;
                    float pitch = 0f;
                    float yaw = 0f;
                    if (retCode_3DAngle != 0)
                    {
                        AppendText(string.Format("3DAngle检测失败，返回{0}!\n\n", retCode_3DAngle));
                    }
                    else
                    {
                        //角度状态 非0表示人脸不可信
                        face3DStatus = MemoryUtil.PtrToStructure<int>(face3DAngleInfo.status + MemoryUtil.SizeOf<int>() * i);
                        //roll为侧倾角，pitch为俯仰角，yaw为偏航角
                        roll = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.roll + MemoryUtil.SizeOf<float>() * i);
                        pitch = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.pitch + MemoryUtil.SizeOf<float>() * i);
                        yaw = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.yaw + MemoryUtil.SizeOf<float>() * i);
                    }

                    int rectWidth = rect.right - rect.left;
                    int rectHeight = rect.bottom - rect.top;

                    //查找最大人脸
                    if (rectWidth * rectHeight > rectTemp)
                    {
                        rectTemp = rectWidth * rectHeight;
                        temp = rect;
                        ageTemp = age;
                        genderTemp = gender;
                    }
                    AppendText(string.Format("{0} - 人脸坐标:[left:{1},top:{2},right:{3},bottom:{4},orient:{5},roll:{6},pitch:{7},yaw:{8},status:{11}] Age:{9} Gender:{10}\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), rect.left, rect.top, rect.right, rect.bottom, orient, roll, pitch, yaw, age, (gender >= 0 ? gender.ToString() : ""), face3DStatus));
                }

                AppendText(string.Format("{0} - 人脸数量:{1}\n\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), multiFaceInfo.faceNum));

                DateTime detectEndTime = DateTime.Now;
                AppendText(string.Format("------------------------------检测结束，时间:{0}------------------------------\n", detectEndTime.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                AppendText("\n");
                ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
                //提取人脸特征
                image1Feature = FaceUtil.ExtractFeature(pImageEngine, srcImage, out singleFaceInfo);

                //清空上次的匹配结果
                for (int i = 0; i < imagesFeatureList.Count; i++)
                {
                    imageList.Items[i].Text = string.Format("{0}号", i + 1);
                }
                //获取缩放比例
                float scaleRate = ImageUtil.getWidthAndHeight(srcImage.Width, srcImage.Height, picImageCompare.Width, picImageCompare.Height);
                //缩放图片
                srcImage = ImageUtil.ScaleImage(srcImage, picImageCompare.Width, picImageCompare.Height);
                //添加标记
                srcImage = ImageUtil.MarkRectAndString(srcImage, (int)(temp.left * scaleRate), (int)(temp.top * scaleRate), (int)(temp.right * scaleRate) - (int)(temp.left * scaleRate), (int)(temp.bottom * scaleRate) - (int)(temp.top * scaleRate), ageTemp, genderTemp, picImageCompare.Width);

                //显示标记后的图像
                picImageCompare.Image = srcImage;
            }
        }
        #endregion

        #region 开始匹配按钮事件
        /// <summary>
        /// 匹配事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void matchBtn_Click(object sender, EventArgs e)
        {
            if (imagesFeatureList.Count == 0)
            {
                MessageBox.Show("请注册人脸!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (image1Feature == IntPtr.Zero)
            {
                if (picImageCompare.Image == null)
                {
                    MessageBox.Show("请选择识别图!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("比对失败，识别图未提取到特征值!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            //标记已经做了匹配比对，在开启视频的时候要清除比对结果
            isCompare = true;
            float compareSimilarity = 0f;
            int compareNum = 0;
            AppendText(string.Format("------------------------------开始比对，时间:{0}------------------------------\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
            for (int i = 0; i < imagesFeatureList.Count; i++)
            {
                IntPtr feature = imagesFeatureList[i];
                float similarity = 0f;
                int ret = ASFFunctions.ASFFaceFeatureCompare(pImageEngine, image1Feature, feature, ref similarity);
                //增加异常值处理
                if (similarity.ToString().IndexOf("E") > -1)
                {
                    similarity = 0f;
                }
                AppendText(string.Format("与{0}号比对结果:{1}\r\n", i, similarity));
                imageList.Items[i].Text = string.Format("{0}号({1})", i, similarity);
                if (similarity > compareSimilarity)
                {
                    compareSimilarity = similarity;
                    compareNum = i;
                }
            }
            if (compareSimilarity > 0)
            {
                lblCompareInfo.Text = " " + compareNum + "号," + compareSimilarity;
            }
            AppendText(string.Format("------------------------------比对结束，时间:{0}------------------------------\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
        }
        #endregion

        #region 视频检测相关(<摄像头按钮点击事件、摄像头Paint事件、特征比对、摄像头播放完成事件>)

        /// <summary>
        /// 摄像头按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnStartVideo_Click(object sender, EventArgs e)
        {
            //在点击开始的时候再坐下初始化检测，防止程序启动时有摄像头，在点击摄像头按钮之前将摄像头拔掉的情况
            initVideo();
            //必须保证有可用摄像头
            if (filterInfoCollection.Count == 0)
            {
                MessageBox.Show("未检测到摄像头，请确保已安装摄像头或驱动!");
                return;
            }
            if (rgbVideoSource.IsRunning || irVideoSource.IsRunning)
            {
                btnStartVideo.Text = "启用摄像头";
                //关闭摄像头
                if (irVideoSource.IsRunning)
                {
                    irVideoSource.SignalToStop();
                    irVideoSource.Hide();
                }
                if (rgbVideoSource.IsRunning)
                {
                    rgbVideoSource.SignalToStop();
                    rgbVideoSource.Hide();
                }
                //“选择识别图”、“开始匹配”按钮可用，阈值控件禁用
                chooseImgBtn.Enabled = true;
                matchBtn.Enabled = true;
                txtThreshold.Enabled = false;
            }
            else
            {
                if (isCompare)
                {
                    //比对结果清除
                    for (int i = 0; i < imagesFeatureList.Count; i++)
                    {
                        imageList.Items[i].Text = string.Format("{0}号", i);
                        //ss
                        //FaceUI.service.imageList.Items[i].Text = string.Format("{0}号", i);
                    }
                    lblCompareInfo.Text = "";
                    isCompare = false;
                }
                //“选择识别图”、“开始匹配”按钮禁用，阈值控件可用，显示摄像头控件
                txtThreshold.Enabled = true;
                rgbVideoSource.Show();
                irVideoSource.Show();
                chooseImgBtn.Enabled = false;
                matchBtn.Enabled = false;
                btnStartVideo.Text = "关闭摄像头";
                //获取filterInfoCollection的总数
                int maxCameraCount = filterInfoCollection.Count;
                //如果配置了两个不同的摄像头索引
                if (rgbCameraIndex != irCameraIndex && maxCameraCount >= 2)
                {
                    //RGB摄像头加载
                    rgbDeviceVideo = new VideoCaptureDevice(filterInfoCollection[rgbCameraIndex < maxCameraCount ? rgbCameraIndex : 0].MonikerString);
                    rgbDeviceVideo.VideoResolution = rgbDeviceVideo.VideoCapabilities[0];
                    rgbVideoSource.VideoSource = rgbDeviceVideo;
                    rgbVideoSource.Start();

                    //IR摄像头
                    irDeviceVideo = new VideoCaptureDevice(filterInfoCollection[irCameraIndex < maxCameraCount ? irCameraIndex : 0].MonikerString);
                    irDeviceVideo.VideoResolution = irDeviceVideo.VideoCapabilities[0];
                    irVideoSource.VideoSource = irDeviceVideo;
                    irVideoSource.Start();
                    //双摄标志设为true
                    isDoubleShot = true;
                }
                else
                {
                    //仅打开RGB摄像头，IR摄像头控件隐藏
                    rgbDeviceVideo = new VideoCaptureDevice(filterInfoCollection[rgbCameraIndex <= maxCameraCount ? rgbCameraIndex : 0].MonikerString);
                    rgbDeviceVideo.VideoResolution = rgbDeviceVideo.VideoCapabilities[0];
                    rgbVideoSource.VideoSource = rgbDeviceVideo;
                    rgbVideoSource.Start();
                    irVideoSource.Hide();
                }
            }
        }

        private FaceTrackUnit trackRGBUnit = new FaceTrackUnit();
        private FaceTrackUnit trackIRUnit = new FaceTrackUnit();
        private Font font = new Font(FontFamily.GenericSerif, 10f, FontStyle.Bold);
        private SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        private SolidBrush blueBrush = new SolidBrush(Color.Blue);
        private bool isRGBLock = false;
        private bool isIRLock = false;
        private MRECT allRect = new MRECT();
        private object rectLock = new object();
        private bool isfeaLock = false;

        public bool isNameLock = false;

        ASF_MultiFaceInfo multiFaceInfo;
        MRECT rect1;
        bool savaImg = false;
        /// <summary>
        /// RGB摄像头Paint事件，图像显示到窗体上，得到每一帧图像，并进行处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoSource_Paint(object sender, PaintEventArgs e)
        {
            if (rgbVideoSource.IsRunning)
            {
                Image Singalimage=default;

                //得到当前RGB摄像头下的图片
                Bitmap bitmap = rgbVideoSource.GetCurrentVideoFrame();
                if (bitmap == null)
                {
                    MessageBox.Show("当前图像为空");
                    return;
                }
                //检测人脸，得到Rect框
                multiFaceInfo = FaceUtil.DetectFace(pVideoEngine, bitmap);
                if (!savaImg)
                {
                    savaImg = !savaImg;
                    bitmap.Save(@"C:\Users\Administrator\Desktop\currentVideo.jpg", ImageFormat.Jpeg);
                }
                
                //得到最大人脸
                ASF_SingleFaceInfo maxFace = FaceUtil.GetMaxFace(multiFaceInfo);
                //得到Rect
                MRECT rect = maxFace.faceRect;
                //检测RGB摄像头下最大人脸
                Graphics g = e.Graphics;
                float offsetX = rgbVideoSource.Width * 1f / bitmap.Width;
                float offsetY = rgbVideoSource.Height * 1f / bitmap.Height;
                float x = rect.left * offsetX;
                float width = rect.right * offsetX - x;
                float y = rect.top * offsetY;
                float height = rect.bottom * offsetY - y;
                //根据Rect进行画框
                // g.DrawRectangle(Pens.Red, x, y, width, height);
                if (multiFaceInfo.faceNum > 0)
                  //  ShowSuccessTip("开始画框");
                for (int i = 0; i < multiFaceInfo.faceNum; i++)
                {
                    rect1 = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryUtil.SizeOf<MRECT>() * i);
                    g.DrawRectangle(Pens.Red, rect1.left * offsetX, rect1.top * offsetY, (rect1.right-rect1.left) * offsetX, rect1.bottom * offsetY - rect1.top * offsetY);

                    //if (trackRGBUnit.message != "" && x > 0 && y > 0)
                    //{
                    //    //将上一帧检测结果显示到页面上
                    //    g.DrawString(trackRGBUnit.message, font, trackRGBUnit.message.Contains("活体") ? blueBrush : yellowBrush, rect1.left*offsetX, rect1.top*offsetY - 15);
                    //}
                    if (_rgbMessage.Count != 0 && _rgbMessage.Count>i&& x > 0 && y > 0)
                    {
                        //将上一帧检测结果显示到页面上
                        g.DrawString(_rgbMessage[i], font, _rgbMessage[i].Contains("活体") ? blueBrush : yellowBrush, rect1.left * offsetX, rect1.top * offsetY - 15);
                    }
                }
                
                //用另一个画框
                //YNFaces[] faces= yn.Detect(bitmap);
                //for (int i = 0; i < faces.Length; i++)
                //{
                //    var r = faces[i].rect;
                //    //Console.WriteLine($"{r.left}--{r.right}--{r.top}--{r.bottom}");
                //    g.DrawRectangle(Pens.Blue, r.left * offsetX, r.top * offsetY, (r.right -r.left)*offsetX,( r.bottom  - r.top)*offsetY);
                //}

                


                //保证只检测一帧，防止页面卡顿以及出现其他内存被占用情况
                if (isRGBLock == false)
                {
                    isRGBLock = true;
                    //异步处理提取特征值和比对，不然页面会比较卡
                    ThreadPool.QueueUserWorkItem(new WaitCallback(async delegate
                    {
                        if (rect.left != 0 && rect.right != 0 && rect.top != 0 && rect.bottom != 0)
                        {
                            try
                            {
                                lock (rectLock)
                                {
                                    allRect.left = (int)(rect.left * offsetX);
                                    allRect.top = (int)(rect.top * offsetY);
                                    allRect.right = (int)(rect.right * offsetX);
                                    allRect.bottom = (int)(rect.bottom * offsetY);
                                }

                                bool isLiveness = false;

                                //调整图片数据，非常重要
                                ImageInfo imageInfo = ImageUtil.ReadBMP(bitmap);

                                if (imageInfo == null)
                                {
                                    return;
                                }
                                


                                //用这个代替画框时候的multiFaceInfo，不能修改multiFaceInfo！！！！
                                ASF_MultiFaceInfo aSF_MultiFaceInfo = default;


                                ImageInfo SingalimageInfo=default;
                                MRECT rect2;
                                //同时多张人脸登录注册
                                for (int i = 0; i < multiFaceInfo.faceNum; i++)
                                {
                                    rect2 = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryUtil.SizeOf<MRECT>() * i);
                                    Singalimage = ImageUtil.CutImage(bitmap, rect2.left, rect2.top, rect2.right, rect2.bottom);
                                    if (Singalimage.Width % 4 != 0)
                                    {
                                        Singalimage = ImageUtil.ScaleImage(Singalimage, Singalimage.Width - (Singalimage.Width % 4), Singalimage.Height);
                                    }
                                    SingalimageInfo = ImageUtil.ReadBMP(Singalimage);
                                    ASF_MultiFaceInfo singalFaceInfo = FaceUtil.DetectFace(pVideoEngine2, Singalimage);
                                    ASF_SingleFaceInfo singalMaxFace= FaceUtil.GetMaxFace(singalFaceInfo);

                                    int retCode_Liveness = -1;
                                    //RGB活体检测
                                    ASF_LivenessInfo liveInfo = FaceUtil.LivenessInfo_RGB(pVideoRGBImageEngine, SingalimageInfo, singalFaceInfo, out retCode_Liveness);
                                    //判断检测结果
                                    if (retCode_Liveness == 0 && liveInfo.num > 0)
                                    {
                                        int isLive = MemoryUtil.PtrToStructure<int>(liveInfo.isLive);
                                        isLiveness = (isLive == 1) ? true : false;
                                    }

                                    //imageInfo=
                                    if (isLiveness)
                                    {
                                        //提取人脸特征
                                        IntPtr feature = FaceUtil.ExtractFeature(pVideoRGBImageEngine, Singalimage, out singalMaxFace);
                                        if (/*isfeaLock == false && */isNameLock == false)
                                        {
                                            isfeaLock = true;

                                            float similarity = 0f;
                                            //得到比对结果
                                            int result = compareFeature(feature, out similarity);
                                            //MemoryUtil.Free(feature);//不能释放，注册到feature中需要使用

                                            //新老用户都检测年龄和性别
                                            //人脸检测
                                            aSF_MultiFaceInfo = FaceUtil.DetectFace(pImageEngine, SingalimageInfo);
                                            //年龄检测
                                            int retCode_Age = -1;
                                            ASF_AgeInfo ageInfo = FaceUtil.AgeEstimation(pImageEngine, SingalimageInfo, aSF_MultiFaceInfo, out retCode_Age);
                                            //性别检测
                                            int retCode_Gender = -1;
                                            ASF_GenderInfo genderInfo = FaceUtil.GenderEstimation(pImageEngine, SingalimageInfo, aSF_MultiFaceInfo, out retCode_Gender);

                                            //3DAngle检测
                                            int retCode_3DAngle = -1;
                                            ASF_Face3DAngle face3DAngleInfo = FaceUtil.Face3DAngleDetection(pImageEngine, SingalimageInfo, aSF_MultiFaceInfo, out retCode_3DAngle);
                                            int age = 0;

                                            if (retCode_Age != 0)
                                            {
                                                AppendText(string.Format("年龄检测失败，返回{0}!\n\n", retCode_Age));
                                            }
                                            else
                                            {
                                                age = MemoryUtil.PtrToStructure<int>(ageInfo.ageArray + MemoryUtil.SizeOf<int>() * i);
                                            }

                                            int gender = -1;
                                            if (retCode_Gender != 0)
                                            {
                                                AppendText(string.Format("性别检测失败，返回{0}!\n\n", retCode_Gender));
                                            }
                                            else
                                            {
                                                gender = MemoryUtil.PtrToStructure<int>(genderInfo.genderArray + MemoryUtil.SizeOf<int>() * i);
                                            }
                                            //老用户
                                            if (result > -1)
                                            {
                                                if (imageList.Items.Count == 0)
                                                    continue;
                                                
                                                //用户登录一次
                                                string guid = imageList.Items[result].ImageKey;
                                                guid = guid.Substring(guid.Length - 36, 32);
                                                string name = SqlHelperUtil.selectNameByGuid(guid);
                                                //模拟登录一次
                                                if (!db.KeyExists(guid))
                                                {
                                                    db.StringSet(guid,DateTime.Now.ToString());
                                                    db.KeyExpire(guid, DateTime.Now.AddSeconds(Expire));
                                                    SqlHelperUtil.AddLoginTimes(guid);
                                                    SqlHelperUtil.AddUserReportTimes();
                                                    SqlHelperUtil.AddUserHistory(guid, DateTime.Now);
                                                    BeginInvoke(new Action(() => ShowSuccessNotifier($"欢迎老用户“{name}”再次光临")));
                                                    //ShowSuccessNotifier("+++++++");
                                                }

                                                

                                                //AppendText(String.Format("id为----{0}----用户登录一次\n\n", guid));
                                                //将比对结果放到显示消息中，用于最新显示
                                                //trackRGBUnit.message = string.Format(" {0}号 {1},{2}", result + 1, /*similarity*/SqlHelperUtil.selectNameByGuid(guid), string.Format("RGB{0},性别{1},年龄{2}", isLiveness ? "活体" : "假体", gender == 0 ? "男" : "女", age));
                                                //_rgbMessage.Add(string.Format(" {0}号 {1},{2}", result + 1, /*similarity*/SqlHelperUtil.selectNameByGuid(guid), string.Format("RGB{0},性别{1},年龄{2}", isLiveness ? "活体" : "假体", gender == 0 ? "男" : "女", age)));
                                                _rgbMessage.Insert(i, string.Format("  {1},{2}", result + 1, /*similarity*/name, string.Format("RGB{0},性别{1},年龄{2}", isLiveness ? "活体" : "假体", gender == 0 ? "男" : "女", age)));
                                            }
                                            //新用户
                                            else
                                            {
                                                //显示消息
                                                //trackRGBUnit.message = string.Format("RGB{0},性别{1},年龄{2}", isLiveness ? "活体" : "假体", gender == 0 ? "男" : "女", age);
                                                _rgbMessage.Add( string.Format("RGB{0},性别{1},年龄{2}", isLiveness ? "活体" : "假体", gender == 0 ? "男" : "女", age));
                                                if (aSF_MultiFaceInfo.faceNum < 1)
                                                {
                                                    image1Feature = IntPtr.Zero;
                                                    picImageCompare.Image = Singalimage;
                                                    AppendText(string.Format("{0} - 未检测出人脸!\n\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                                                }
                                                else
                                                {

                                                    int orient = MemoryUtil.PtrToStructure<int>(aSF_MultiFaceInfo.faceOrients + MemoryUtil.SizeOf<int>() * i);
                                                    int face3DStatus = -1;
                                                    float roll = 0f;
                                                    float pitch = 0f;
                                                    float yaw = 0f;
                                                    if (retCode_3DAngle != 0)
                                                    {
                                                        AppendText(string.Format("3DAngle检测失败，返回{0}!\n\n", retCode_3DAngle));
                                                    }
                                                    else
                                                    {
                                                        //角度状态 非0表示人脸不可信
                                                        face3DStatus = MemoryUtil.PtrToStructure<int>(face3DAngleInfo.status + MemoryUtil.SizeOf<int>() * i);
                                                        //roll为侧倾角，pitch为俯仰角，yaw为偏航角
                                                        roll = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.roll + MemoryUtil.SizeOf<float>() * i);
                                                        pitch = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.pitch + MemoryUtil.SizeOf<float>() * i);
                                                        yaw = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.yaw + MemoryUtil.SizeOf<float>() * i);
                                                    }
                                                    AppendText(string.Format("{0} - 人脸坐标:[left:{1},top:{2},right:{3},bottom:{4},orient:{5},roll:{6},pitch:{7},yaw:{8},status:{11}] Age:{9} Gender:{10}\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), rect2.left, rect2.top, rect2.right, rect2.bottom, orient, roll, pitch, yaw, age, (gender >= 0 ? gender.ToString() : ""), face3DStatus));
                                                    //人脸注册条件   3D为正面照，并可信
                                                    if (face3DStatus == 0 && Math.Abs(roll) < Reg3Dmax && Math.Abs(pitch) < Reg3Dmax && Math.Abs(yaw) < Reg3Dmax)
                                                    {
                                                        var res = yn.Detect((Bitmap)Singalimage);
                                                        if (res.Length == 0)
                                                        {

                                                        }
                                                        else if (res[0].score < 0.9)
                                                        {
                                                            Console.WriteLine(res[0].score);
                                                            //MessageBox.Show("人脸注册，请勿遮挡人脸");
                                                        }
                                                        else
                                                        {
                                                            //将用户人脸保存到本地
                                                            string guid = Guid.NewGuid().ToString("N");
                                                            Singalimage.Save(ImagePath + guid + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                                                            //将图片在image文件夹下的名称注册到数据库
                                                            SqlHelperUtil.AddUser(new User()
                                                            {
                                                                Sex = (gender == 0) ? true : false,
                                                                Age = age,
                                                                ImagePath = guid,
                                                                RegisterTime = DateTime.Now,
                                                                LastLoginTime = DateTime.Now,
                                                                State = true
                                                            }) ;
                                                            //注册新用户到imagesFeatureList
                                                            imagesFeatureList.Add(feature);
                                                            //AppendText("新用户注册成功!\n " + similarity.ToString());
                                                            ShowSuccessNotifier($"注册成功，欢迎您的首次光临!");

                                                            //更新左侧界面
                                                            ListViewItem item = new ListViewItem();
                                                            item.Text = imagesFeatureList.Count + "号";
                                                            item.Name = guid;
                                                            item.ImageKey = ImagePath + guid + ".jpg";
                                                            imageList.Items.Add(item);
                                                            imageLists.Images.Add(ImagePath + guid + ".jpg", Singalimage);
                                                            //ss
                                                            //FaceUI.service.imageList.Items.Add(item);
                                                            //FaceUI.service.imageLists.Images.Add(ImagePath + guid + ".jpg", Singalimage);
                                                            //刷新ImageGuids（只启动时候同步了数据库)
                                                            ImageGuids.Add(guid);
                                                            ImagePaths.Add(ImagePath + guid + ".jpg");
                                                            imageList.Refresh();
                                                            //ss
                                                            //FaceUI.service.imageList.Refresh();
                                                        }
                                                    }
                                                }
                                            }
                                            isfeaLock = false;
                                        }
                                    }
                                    else
                                    {
                                        _rgbMessage.Clear();
                                        //显示消息
                                        //trackRGBUnit.message = string.Format("RGB{0}", isLiveness ? "活体" : "假体");
                                        //_rgbMessage.Add( string.Format("RGB{0}", isLiveness ? "活体" : "假体"));
                                    }
                                }

                                if (SingalimageInfo != null)
                                    MemoryUtil.Free(SingalimageInfo.imgData);
                                //处理人脸信息后再释放imageinfo
                                if (imageInfo != null)
                                {
                                    MemoryUtil.Free(imageInfo.imgData);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                if (Singalimage != null)
                                    Singalimage.Dispose();


                                if (bitmap != null)
                                {
                                    bitmap.Dispose();
                                }
                                isRGBLock = false;
                                isfeaLock = true;
                            }
                        }
                        else
                        {
                            lock (rectLock)
                            {
                                allRect.left = 0;
                                allRect.top = 0;
                                allRect.right = 0;
                                allRect.bottom = 0;
                            }
                        }
                        isRGBLock = false;
                    }));
                }
            }
        }


        /// <summary>
        /// 不查询数据库刷新左侧(适用与修改用户)
        /// </summary>
        public void LoadLeftImageNotSelectSql()
        {
            imagesFeatureList.Clear();
            imagePathList.Clear();
            imageList.Clear();
            imageLists.Images.Clear();
            

            List<string> imagePathListTemp = new List<string>();
            int isGoodImage = 1;
            for (int i = 0; i < ImagePaths.Count; i++)
            {
                //图片格式判断
                if (checkImage(ImagePaths[i]))
                {
                    imagePathListTemp.Add(ImagePaths[i]);
                }
            }
            for (int i = 0; i < imagePathListTemp.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                imagePathList.Add(imagePathListTemp[i]);
                Image image = ImageUtil.readFromFile(imagePathListTemp[i]);
                if (image == null)
                {
                    continue;
                }
                imageLists.Images.Add(imagePathListTemp[i], image);
                item.Text = (0 + isGoodImage) + "号";
                item.Name = ImageGuids[i];
                item.ImageKey = imagePathListTemp[i];
                //添加到左侧界面
                imageList.Items.Add(item);
                //ss
                //ListViewItem itemss = new ListViewItem();
                //itemss = (ListViewItem)item.Clone();
                //FaceUI.service.imageLists.Images.Add(imagePathListTemp[i], image);
                //FaceUI.service.imageList.Items.Add(itemss);
                isGoodImage += 1;
                if (image != null)
                {
                    image.Dispose();
                }
            }
            imageList.Refresh();
            //ss
            //FaceUI.service.imageList.Refresh();
            //提取人脸特征
            for (int i = 0; i < imagePathList.Count; i++)
            {
                ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
                Image image = ImageUtil.readFromFile(imagePathList[i]);
                if (image == null)
                {
                    continue;
                }
                IntPtr feature = FaceUtil.ExtractFeature(pImageEngine, image, out singleFaceInfo);
                imagesFeatureList.Add(feature);//将image文件夹下的图片提取特征到List
                if (image != null)
                {
                    image.Dispose();
                }
            }

        }
        public Bitmap Scale(Bitmap bitmap)
        {
            Image image = bitmap;
            if (image == null)
            {
                return null;
            }
            if (image.Width > 1536 || image.Height > 1536)
            {
                image = ImageUtil.ScaleImage(image, 1536, 1536);
            }
            if (image == null)
            {
                return null;
            }
            if (image.Width % 4 != 0)
            {
                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
            }
            return image as Bitmap;
        }
        public string bitmapToBase64(Bitmap bitmap)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// RGB摄像头Paint事件,同步RGB人脸框，对比人脸框后进行IR活体检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void irVideoSource_Paint(object sender, PaintEventArgs e)
        {
            if (isDoubleShot && irVideoSource.IsRunning)
            {
                //如果双摄，且IR摄像头工作，获取IR摄像头图片
                Bitmap irBitmap = irVideoSource.GetCurrentVideoFrame();
                if (irBitmap == null)
                {
                    return;
                }
                //得到Rect
                MRECT rect = new MRECT();
                lock (rectLock)
                {
                    rect = allRect;
                }
                float irOffsetX = irVideoSource.Width * 1f / irBitmap.Width;
                float irOffsetY = irVideoSource.Height * 1f / irBitmap.Height;
                float offsetX = irVideoSource.Width * 1f / rgbVideoSource.Width;
                float offsetY = irVideoSource.Height * 1f / rgbVideoSource.Height;
                //检测IR摄像头下最大人脸
                Graphics g = e.Graphics;

                float x = rect.left * offsetX;
                float width = rect.right * offsetX - x;
                float y = rect.top * offsetY;
                float height = rect.bottom * offsetY - y;
                //根据Rect进行画框
                g.DrawRectangle(Pens.Red, x, y, width, height);
                if (trackIRUnit.message != "" && x > 0 && y > 0)
                {
                    //将上一帧检测结果显示到页面上
                    g.DrawString(trackIRUnit.message, font, trackIRUnit.message.Contains("活体") ? blueBrush : yellowBrush, x, y - 15);
                }

                //保证只检测一帧，防止页面卡顿以及出现其他内存被占用情况
                if (isIRLock == false)
                {
                    isIRLock = true;
                    //异步处理提取特征值和比对，不然页面会比较卡
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        if (rect.left != 0 && rect.right != 0 && rect.top != 0 && rect.bottom != 0)
                        {
                            bool isLiveness = false;
                            try
                            {
                                //得到当前摄像头下的图片
                                if (irBitmap != null)
                                {
                                    //检测人脸，得到Rect框
                                    ASF_MultiFaceInfo irMultiFaceInfo = FaceUtil.DetectFace(pVideoIRImageEngine, irBitmap);
                                    if (irMultiFaceInfo.faceNum <= 0)
                                    {
                                        return;
                                    }
                                    //得到最大人脸
                                    ASF_SingleFaceInfo irMaxFace = FaceUtil.GetMaxFace(irMultiFaceInfo);
                                    //得到Rect
                                    MRECT irRect = irMaxFace.faceRect;
                                    //判断RGB图片检测的人脸框与IR摄像头检测的人脸框偏移量是否在误差允许范围内
                                    if (isInAllowErrorRange(rect.left * offsetX / irOffsetX, irRect.left) && isInAllowErrorRange(rect.right * offsetX / irOffsetX, irRect.right)
                                    && isInAllowErrorRange(rect.top * offsetY / irOffsetY, irRect.top) && isInAllowErrorRange(rect.bottom * offsetY / irOffsetY, irRect.bottom))
                                    {
                                        int retCode_Liveness = -1;
                                        //将图片进行灰度转换，然后获取图片数据
                                        ImageInfo irImageInfo = ImageUtil.ReadBMP_IR(irBitmap);
                                        if (irImageInfo == null)
                                        {
                                            return;
                                        }
                                        //IR活体检测
                                        ASF_LivenessInfo liveInfo = FaceUtil.LivenessInfo_IR(pVideoIRImageEngine, irImageInfo, irMultiFaceInfo, out retCode_Liveness);
                                        //判断检测结果
                                        if (retCode_Liveness == 0 && liveInfo.num > 0)
                                        {
                                            int isLive = MemoryUtil.PtrToStructure<int>(liveInfo.isLive);
                                            isLiveness = (isLive == 1) ? true : false;
                                        }
                                        if (irImageInfo != null)
                                        {
                                            MemoryUtil.Free(irImageInfo.imgData);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                trackIRUnit.message = string.Format("IR{0}", isLiveness ? "活体" : "假体");
                                if (irBitmap != null)
                                {
                                    irBitmap.Dispose();
                                }
                                isIRLock = false;
                            }
                        }
                        else
                        {
                            trackIRUnit.message = string.Empty;
                        }
                        isIRLock = false;
                    }));
                }
            }
        }


        /// <summary>
        /// 得到feature比较结果
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        private int compareFeature(IntPtr feature, out float similarity)
        {
            int result = -1;
            similarity = 0f;
            //如果人脸库不为空，则进行人脸匹配
            if (imagesFeatureList != null && imagesFeatureList.Count > 0)
            {
                for (int i = 0; i < imagesFeatureList.Count; i++)
                {
                    //调用人脸匹配方法，进行匹配
                    ASFFunctions.ASFFaceFeatureCompare(pVideoRGBImageEngine, feature, imagesFeatureList[i], ref similarity);
                    if (similarity >= threshold)
                    {
                        result = i;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 摄像头播放完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        private void videoSource_PlayingFinished(object sender, AForge.Video.ReasonToFinishPlaying reason)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                chooseImgBtn.Enabled = true;
                matchBtn.Enabled = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region 阈值相关
        /// <summary>
        /// 阈值文本框键按下事件，检测输入内容是否正确，不正确不能输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtThreshold_KeyPress(object sender, KeyPressEventArgs e)
        {
            //阻止从键盘输入键
            e.Handled = true;
            //是数值键，回退键，.能输入，其他不能输入
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == '.')
            {
                //渠道当前文本框的内容
                string thresholdStr = txtThreshold.Text.Trim();
                int countStr = 0;
                int startIndex = 0;
                //如果当前输入的内容是否是“.”
                if (e.KeyChar == '.')
                {
                    countStr = 1;
                }
                //检测当前内容是否含有.的个数
                if (thresholdStr.IndexOf('.', startIndex) > -1)
                {
                    countStr += 1;
                }
                //如果输入的内容已经超过12个字符，
                if (e.KeyChar != 8 && (thresholdStr.Length > 12 || countStr > 1))
                {
                    return;
                }
                e.Handled = false;
            }
        }

        /// <summary>
        /// 阈值文本框键抬起事件，检测阈值是否正确，不正确改为0.8f
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtThreshold_KeyUp(object sender, KeyEventArgs e)
        {
            //如果输入的内容不正确改为默认值
            if (!float.TryParse(txtThreshold.Text.Trim(), out threshold))
            {
                threshold = 0.8f;
            }
        }
        #endregion

        #region 窗体关闭
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        public void Form_Closed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (rgbVideoSource.IsRunning)
                {
                    btnStartVideo_Click(sender, e); //关闭摄像头
                }

                //销毁引擎
                int retCode = ASFFunctions.ASFUninitEngine(pImageEngine);
                Console.WriteLine("UninitEngine pImageEngine Result:" + retCode);
                //销毁引擎
                retCode = ASFFunctions.ASFUninitEngine(pVideoEngine);
                Console.WriteLine("UninitEngine pVideoEngine Result:" + retCode);

                //销毁引擎
                retCode = ASFFunctions.ASFUninitEngine(pVideoRGBImageEngine);
                Console.WriteLine("UninitEngine pVideoImageEngine Result:" + retCode);

                //销毁引擎
                retCode = ASFFunctions.ASFUninitEngine(pVideoIRImageEngine);
                Console.WriteLine("UninitEngine pVideoIRImageEngine Result:" + retCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UninitEngine pImageEngine Error:" + ex.Message);
            }
        }
        #endregion

        #region 公用方法
        /// <summary>
        /// 恢复使用/禁用控件列表控件
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="controls">控件列表</param>
        private void ControlsEnable(bool isEnable, params Control[] controls)
        {
            if (controls == null || controls.Length <= 0)
            {
                return;
            }
            foreach (Control control in controls)
            {
                control.Enabled = isEnable;
            }
        }

        /// <summary>
        /// 校验图片
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private bool checkImage(string imagePath)
        {
            if (imagePath == null)
            {
                AppendText("图片不存在，请确认后再导入\r\n");
                return false;
            }
            try
            {
                //判断图片是否正常，如将其他文件把后缀改为.jpg，这样就会报错
                Image image = ImageUtil.readFromFile(imagePath);
                if (image == null)
                {
                    throw new Exception();
                }
                else
                {
                    image.Dispose();
                }
            }
            catch
            {
                AppendText(string.Format("{0} 图片格式有问题，请确认后再导入\r\n", imagePath));
                return false;
            }
            FileInfo fileCheck = new FileInfo(imagePath);
            if (fileCheck.Exists == false)
            {
                AppendText(string.Format("{0} 不存在\r\n", fileCheck.Name));
                return false;
            }
            else if (fileCheck.Length > maxSize)
            {
                AppendText(string.Format("{0} 图片大小超过2M，请压缩后再导入\r\n", fileCheck.Name));
                return false;
            }
            else if (fileCheck.Length < 2)
            {
                AppendText(string.Format("{0} 图像质量太小，请重新选择\r\n", fileCheck.Name));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 追加公用方法
        /// </summary>
        /// <param name="message"></param>
        private void AppendText(string message)
        {
            logBox.AppendText(message);
        }

        /// <summary>
        /// 判断参数0与参数1是否在误差允许范围内
        /// </summary>
        /// <param name="arg0">参数0</param>
        /// <param name="arg1">参数1</param>
        /// <returns></returns>
        private bool isInAllowErrorRange(float arg0, float arg1)
        {
            bool rel = false;
            if (arg0 > arg1 - allowAbleErrorRange && arg0 < arg1 + allowAbleErrorRange)
            {
                rel = true;
            }
            return rel;
        }
        #endregion


        //private bool TwoShow = true;
        private void imageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            return;
            ////第一次索引改变并不会发生行为
            //if (TwoShow)
            //{
            //    TwoShow = !TwoShow;
            //    return;
            //}
            //var focus = (ListView)sender;
            //var text = focus.FocusedItem.Name;
            //TwoShow = !TwoShow;
        }

        private void imageList_DoubleClick(object sender, EventArgs e)
        {
            isNameLock = true;
            var focus = (ListView)sender;
            var guid = focus.FocusedItem.Name;               
            User user = SqlHelperUtil.SelectUserByGuid(guid);
            if (user == null)
            {
                ShowErrorTip("暂不存在此用户! 请确认是否已被删除！");
                return;
            }
            EditUser editUser = new EditUser(this);
            editUser.User = user;
            editUser.ShowDialog();
            
            if (editUser.IsOK)
            {
                cutEditSave(editUser.User, editUser.newimagePath);
                ShowSuccessNotifier("保存成功");
            }
            editUser.Dispose();
            isNameLock = false;
            if (isNoPerFace)
                return;
            LoadLeftImageNotSelectSql();
        }
        private bool isNoPerFace = true;
        //修改人脸时候负责裁剪和修改本地和数据库
        public int cutEditSave(User user, string imagepath)
        {
            //没有修改照片
            if (imagepath == null)
            {
                //更新数据库信息
                SqlHelperUtil.UpdateUserInfo(user,true);
                return 0;
            }
            if (!checkImage(imagepath))
            {
                return 1;
            }
            Image image = ImageUtil.readFromFile(imagepath);
            if (image == null)
            {
                return 2;
            }
            if (image.Width > 1536 || image.Height > 1536)
            {
                image = ImageUtil.ScaleImage(image, 1536, 1536);
            }
            if (image == null)
            {
                return 3;
            }
            if (image.Width % 4 != 0)
            {
                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
            }
            //人脸检测
            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, image);
            //判断检测结果
            if (multiFaceInfo.faceNum > 0)
            {
                MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
                image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
                //覆盖本地之前照片
                image.Save(ImagePath + user.ImagePath + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //更新数据库信息
                SqlHelperUtil.UpdateUserInfo(user);

                isNoPerFace = false;
                return 0;
            }
            else
            {
                isNoPerFace = true;
                if (image != null)
                {
                    image.Dispose();
                }
                return 4;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

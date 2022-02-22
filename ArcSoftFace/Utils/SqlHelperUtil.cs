using ArcSoftFace.context;
using ArcSoftFace.Entity;
using ArcSoftFace.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.Utils
{
    public static class SqlHelperUtil
    {
        private static SqlConnection conn = null;
        private static SqlCommand cmd = null;
        private static SqlDataReader sdr = null;
        private static string constr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        private static string TableUser = "[User]";
        private static string TableUserReport = "[UserReport]";
        private static string TableUserHistory = "[UserHistory]";
        private static string TableUserMenu = "[UserMenu]";
        private static string TableMenuClassify = "[MenuClassify]";
        private static string TableMenu = "[Menu]";
        private static DateTime MinTime = DateTime.Parse("1001-01-01");
        //private static UserDB userDB = new UserDB();
        public static DataTable ReadTableUser(bool state=true)
        {
            string sql;
            sql = $"select * from {TableUser} where State='{state}'";
            return ExecuteDataTable(sql, null);
        }
        public static DataTable ReadTableUserHistory()
        {
            string sql = $"select Guid,DateTime from {TableUserHistory}";
            return ExecuteDataTable(sql, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Id,ClassifyId,Name,Price</returns>
        public static DataTable ReadTableMenu()
        {
            string sql = $"select Id,ClassifyId,Name,Price from {TableMenu}";
            return ExecuteDataTable(sql, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Id ,ClassName</returns>
        public static DataTable ReadTableMenuClassify()
        {
            string sql = $"select Id,ClassName from {TableMenuClassify} where State='1'";
            return ExecuteDataTable(sql, null);
        }
        public static DataTable ReadTableUserMenu(string guid)
        {
            string sql = $"select MenuId,Total from {TableUserMenu} where UserGuid='{guid}' order by MenuId";
            return ExecuteDataTable(sql, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClassifyId"></param>
        /// <param name="MenuName"></param>
        /// <returns>Id,ClassifyId,Name,Price</returns>
        internal static DataTable SelectMenuByClassifyOrName(int ClassifyId=0, string MenuName=null)
        {
            string sql = "";
            if (ClassifyId == 0 && string.IsNullOrWhiteSpace(MenuName))
            {
                return ReadTableMenu();
            }
            else if (ClassifyId != 0&& string.IsNullOrWhiteSpace(MenuName))
            {
                sql = $"select Id,ClassifyId,Name,Price from {TableMenu} where ClassifyId='{ClassifyId}'";
            }
            else if (ClassifyId == 0 && !string.IsNullOrWhiteSpace(MenuName))
            {
                sql = $"select Id,ClassifyId,Name,Price from {TableMenu} where Name like '%{MenuName}%'";
            }
            else
            {
                sql = $"select Id,ClassifyId,Name,Price from {TableMenu} where ClassifyId='{ClassifyId}' and Name like '%{MenuName}%'";
            }
            return ExecuteDataTable(sql, null);
        }

        public static DataTable ReadTableUserByName(string search)
        {
            string sql = $"select * from {TableUser} where State=1 and Name like '%{search}%' ";
            if (string.IsNullOrWhiteSpace(search))
                sql = $"select * from {TableUser}";
            return ExecuteDataTable(sql);
        }
        public static DataTable ReadTableUserByIphone(string search)
        {
            string sql = $"select * from {TableUser} where State=1 and Iphone like '%{search}%' ";
            if (string.IsNullOrWhiteSpace(search))
                sql = $"select * from {TableUser}";
            return ExecuteDataTable(sql);
        }
        public static DataTable ReadTableUserAll()
        {
            string sql;
            sql = $"select * from {TableUser}";
            return ExecuteDataTable(sql, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>MenuId,sum(Total)</returns>
        public static DataTable ReadUserMenuFirst8()
        {
            string sql = $"select MenuId,sum(Total) from {TableUserMenu} group by MenuId order by sum(Total) desc";
            return ExecuteDataTable(sql, null);

        }

        /// <summary>
        /// 返回lastTime到此时时间段内不同年龄段的总人数
        /// </summary>
        /// <param name="lastTime">开始时间点</param>
        /// <returns>返回 依次为时间区间内 年龄段从高到底的 客流量总人数</returns>
        public static List<int> GetDifferentAgeGroupsLastTime(DateTime lastTime)
        {
            List<int> res = new List<int>() { 0, 0, 0, 0, 0 };
            DateTime cur = DateTime.Now.AddDays(1);
            var curDate=cur.ToString("yyyy - MM - dd");
            string sql = $"select lef.num,rig.Age from (select Guid,count(*) as num from {TableUserHistory} where DateTime < /*GETDATE()*/'{curDate}' and DateTime>'{lastTime}' group by Guid) as lef  left join {TableUser} as rig on lef.Guid=rig.GuidPath";
            DataTable dt = ExecuteDataTable(sql, null);
            foreach (DataRow item in dt.Rows)
            {
                int r = 0;
                if (!int.TryParse(item[1].ToString(), out r))
                    continue;
                if (r > 79)
                    res[4] += Convert.ToInt32(item[0]);
                else if(r > 59)
                    res[3] += Convert.ToInt32(item[0]);
                else if (r > 39)
                    res[2] += Convert.ToInt32(item[0]);
                else if (r > 19)
                    res[1] += Convert.ToInt32(item[0]);
                else if (r > 0)
                    res[0] += Convert.ToInt32(item[0]);
            }
            return res;

        }
        public static List<int> GetTableUserAllAge()
        {
            List<int> res = new List<int>();
                string sql = $"select Age from {TableUser} where state=1";
            DataTable dt = ExecuteDataTable(sql, null);
            foreach (DataRow item in dt.Rows)
            {
                res.Add(Convert.ToInt32(item[0]));
            }
            return res;
        }
        public static void GetTableUserSexNumber(out int man,out int woman)
        {
            string sql = $"select count(*) as '人数', case Sex when 'True' Then '男' When 'False' Then '女' End as '性别' from {TableUser} where state=1 group by Sex";
            DataTable dt= ExecuteDataTable(sql, null);
            man = 0;
            woman = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i][1].ToString().Equals("男"))
                    man = Convert.ToInt32(dt.Rows[i][0]);
                else if(dt.Rows[i][1].ToString().Equals("女"))
                    woman = Convert.ToInt32(dt.Rows[i][0]);
            }

        }
        public static List<int> GetRegisPerByLastYear(DateTime currentTime,DateTime lastTime)
        {
            string current = currentTime.ToString("yyyy-MM-dd");
            string last = lastTime.ToString("yyyy-MM-dd");
            string sql = $"select MONTH(RegisterTime) as '月份' ,count(*) as '注册人数' from {TableUser} where RegisterTime > '{last}' and RegisterTime<'{current}' group by MONTH(RegisterTime)";
            DataTable dt = ExecuteDataTable(sql, null);
            List<int> res = new List<int>();

            int lastMonth = lastTime.Month;
            int currentMonth = lastMonth+12;
            for (int i = 0; i < dt.Rows.Count||lastMonth<currentMonth; i++)
            {
                if (i >= dt.Rows.Count)
                {
                    res.Add(0);
                    lastMonth++;
                    continue;
                }
                if (Convert.ToInt32(dt.Rows[i][0]) != lastMonth%12)
                {
                    res.Add(0);
                    i--;
                }
                else
                {
                    res.Add(Convert.ToInt32(dt.Rows[i][1]));
                }
                lastMonth++;
            }
            return res;
        }
        public static List<int> GetLoginPerByLastYear(DateTime currentTime,DateTime lastTime)
        {
            string current = currentTime.ToString("yyyy-MM-dd");
            string last = lastTime.ToString("yyyy-MM-dd");
            string sql = $"select MONTH(CurrentDateTime) as '月份' ,sum(LoginTimes) as '登录人数' from {TableUserReport} where CurrentDateTime > '{last}' and CurrentDateTime<'{current}' group by MONTH(CurrentDateTime) ";
            DataTable dt = ExecuteDataTable(sql, null);
            List<int> res = new List<int>();

            int lastMonth = lastTime.Month;
            int currentMonth = lastMonth + 12;
            for (int i = 0; i < dt.Rows.Count || lastMonth < currentMonth; i++)
            {
                if (i >= dt.Rows.Count)
                {
                    res.Add(0);
                    lastMonth++;
                    continue;
                }
                if (Convert.ToInt32(dt.Rows[i][0]) != lastMonth % 12)
                {
                    res.Add(0);
                    i--;
                }
                else
                {
                    res.Add(Convert.ToInt32(dt.Rows[i][1]));
                }
                lastMonth++;
            }
            res.RemoveAt(0);
            return res;
        }
        public static List<int> GetRegisPerByLastWeek(DateTime currentTime,DateTime lastTime)
        {

            string current = currentTime.ToString("yyyy-MM-dd");
            string last = lastTime.ToString("yyyy-MM-dd");
            string sql = $"select count(*) as '注册人数',CONVERT(varchar(8), RegisterTime, 112) as '当前日期' from {TableUser} where RegisterTime > '{last}' and RegisterTime<'{current}' group by CONVERT(varchar(8), RegisterTime, 112)";

            DataTable dt = ExecuteDataTable(sql, null);
            return TableToList(lastTime.Date, currentTime.Date, dt);
        }
        
        
        public static List<int> GetLoginPerByLastWeek(DateTime currentTime, DateTime lastTime)
        {
            string current = currentTime.ToString("yyyy-MM-dd");
            string last = lastTime.ToString("yyyy-MM-dd");
            string sql = $"select sum(LoginTimes) as '登录次数', CONVERT(varchar(10),CurrentDateTime,112) as '日期' from {TableUserReport} where CurrentDateTime>'{last}' and CurrentDateTime<'{current}' group by CONVERT(varchar(10),CurrentDateTime,112)  order by CONVERT(varchar(10),CurrentDateTime,112)";

            DataTable dt = ExecuteDataTable(sql, null);
            return TableToList(lastTime.Date, currentTime.Date, dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastTime">开始日期（不包括时间）</param>
        /// <param name="currentTime">结束日期（不含时间）</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<int> TableToList(DateTime lastTime,DateTime currentTime,DataTable dt)
        {
            List<int> res = new List<int>();
            DateTime rowTime;
            for (int i = 0; i < dt.Rows.Count||lastTime<currentTime; i++)
            {
                //数据库表读完了，但是没到结束日期
                if (i>=dt.Rows.Count)
                {
                    res.Add(0);
                    lastTime=lastTime.AddDays(1);
                    continue;
                }
                rowTime=DateTime.ParseExact(dt.Rows[i][1].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                //一般不会转错
                if (rowTime==default)
                {
                    res.Add(0);
                    lastTime= lastTime.AddDays(1);
                    
                    continue;
                }
                if (DateTime.Equals(lastTime, rowTime))
                {
                    res.Add(Convert.ToInt32(dt.Rows[i][0].ToString()));
                }
                else //数据库没有当前日期的用户登录
                {
                    res.Add(0);
                    i--;
                }

                lastTime=lastTime.AddDays(1);
            }
            return res;
        }
        
        public static int AddUser(User user)
        {
            string sql = String.Format("insert into {0}(Sex,Age,GuidPath,RegisterTime,LastLoginTime,state) values('{1}',{2},'{3}','{4}','{5}','{6}')", TableUser, user.Sex, user.Age, user.ImagePath,user.RegisterTime, user.LastLoginTime, user.State);
            return ExecuteNonQuery(sql, null);
        }
        public static int AddUserHistory(string guid,DateTime time)
        {
            string sql = $"insert into {TableUserHistory}(Guid,DateTime) values('{guid}','{time}') ";
            return ExecuteNonQuery(sql, null);

        }
        public static int AddUserReportTimes()
        {
            string sql = $"update {TableUserReport} set LoginTimes=LoginTimes+1 where CurrentDateTime='{DateTime.Now.Date.ToString("yyyy-MM-dd")}'";
            if(ExecuteNonQuery(sql, null) == 0)
            {
                sql = $"insert into {TableUserReport} values('{DateTime.Now.Date.ToString("yyyy-MM-dd")}','1')";
                return ExecuteNonQuery(sql, null);
            }
            return 1;
        }
        public static int AddMenu(MenuDto menuDto)
        {
            string sql = $"insert into {TableMenu} values('{menuDto.ClassifyId}','{menuDto.Name}','{menuDto.Price}')";
            return ExecuteNonQuery(sql, null);
        }
        public static string selectNameByGuid(string GuidPath)
        {
            string sql = string.Format("select Name from {0} where GuidPath='{1}' and State=1", TableUser, GuidPath);
            if (ExecuteScalar(sql, null) == null)
                return "";
            return ExecuteScalar(sql, null).ToString();
        }

        public static User SelectUserByGuid(string GuidPath)
        {
            string sql = string.Format("select Name,Sex,Age,GuidPath from {0} where GuidPath='{1}' and State=1", TableUser, GuidPath);
            DataTable dt = ExecuteDataTable(sql, null);
            if (dt.Rows.Count==0)
            {
                return default;
            }
            return new User()
            {
                Name = dt.Rows[0][0].ToString(),
                Sex = (dt.Rows[0][1].ToString() == "True") ? true : false,
                Age = Convert.ToInt32(dt.Rows[0][2].ToString()),
                ImagePath = dt.Rows[0][3].ToString()
            };
        }
        public static User SelectUserByGuidHistory(string Guid)
        {
            string sql = string.Format("select Name,Sex,Age,Iphone from {0} where GuidPath='{1}'", TableUser, Guid);
            DataTable dt = ExecuteDataTable(sql, null);
            return new User()
            {
                Name = dt.Rows[0][0].ToString(),
                Sex = (dt.Rows[0][1].ToString() == "True") ? true : false,
                Age = Convert.ToInt32(dt.Rows[0][2].ToString()),
                Iphone = dt.Rows[0][3].ToString()
            };
        }
        public static DataTable SelectUserHistoryMenuHeader(string guid)
        {
            string sql = $"select MenuId as 菜名Id,Total as 历史消费总价 from {TableUserMenu} where UserGuid='{guid}' order by Total desc";
            return ExecuteDataTable(sql, null);
        }

        public static int AddLoginTimes(string GuidPath)
        {
            string sql = String.Format("update {0} set LoginTimes+=1,LastLoginTime='{1}' where GuidPath='{2}'", TableUser, DateTime.Now, GuidPath);
            return ExecuteNonQuery(sql, null);
        }
        public static int UpdateUserInfo(User user,bool jieMian=false)
        {
            string sql;
            if (string.IsNullOrWhiteSpace(user.Iphone))
            {
               sql= string.Format("update {0} set Name='{1}',sex='{2}',age='{3}' where GuidPath='{4}'", TableUser, user.Name, user.Sex, user.Age, user.ImagePath);
            }else
            {
                sql = string.Format("update {0} set Name='{1}',sex='{2}',age='{3}',Iphone='{5}' where GuidPath='{4}'", TableUser, user.Name, user.Sex, user.Age, user.ImagePath, user.Iphone);
            }
            if (jieMian)
                sql = string.Format("update {0} set Name='{1}',sex='{2}',age='{3}' where GuidPath='{4}'", TableUser, user.Name, user.Sex, user.Age, user.ImagePath);
                return ExecuteNonQuery(sql, null);
        }

        public static int UpdateUser(User user)
        {
            string sql = String.Format("update {0} set lastLoginTime='{1}',LoginTimes+=1 where GuidPath='{2}'", TableUser, DateTime.Now, user.ImagePath);
            return ExecuteNonQuery(sql, null);
        }
        public static int UpdateTableToUserMenu(DataTable dt,string guid)
        {
            bool isHasUpdate=default;
            string sql="";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (int.Parse(dt.Rows[i][4].ToString()) == 0)
                    continue;

                int total = int.Parse(dt.Rows[i][4].ToString()) + int.Parse(dt.Rows[i][3].ToString());

                if (int.Parse(dt.Rows[i][3].ToString()) == 0)//之前没有这个数据
                {
                    sql += $"insert {TableUserMenu} values('{i+1}','{guid}','{total}');";
                }
                else
                {
                    sql += $"update {TableUserMenu} set Total={total} where MenuId='{i+1}' and UserGuid='{guid}';";
                }
                
            }
            if (string.IsNullOrWhiteSpace(sql))
                return 0;
            return ExecuteNonQuery(sql, null);
        }
        public static int UpdateMenu(MenuDto menuDto)
        {
            string sql = $"update {TableMenu} set ClassifyId='{menuDto.ClassifyId}',Name='{menuDto.Name}',Price='{menuDto.Price}') where Id='{menuDto.Id}'";
            return ExecuteNonQuery(sql, null);

        }
        
        /// <summary>
        /// 读取数据库图像路径
        /// </summary>
        /// <returns></returns>
        public static List<string> ReadGuidPathList()
        {
            List<string> gupath = new List<string>();
            DataTable dt = new DataTable();
            string sql = string.Format("select * from {0} where State=1", TableUser);
            dt = ExecuteDataTable(sql, null);
            foreach (DataRow row in dt.Rows)
            {
                gupath.Add(row["GuidPath"].ToString());
            }
            return gupath;
        }
        /// <summary>
        /// 删除表数据
        /// </summary>
        /// <returns></returns>
        public static int DelDatas()
        {
            string sql = string.Format("update {0} set State=0", TableUser);
            return ExecuteNonQuery(sql, null);
        }
        public static int DelUserByGuid(string guid)
        {
            string sql = $"update {TableUser} set State=0 where GuidPath='{guid}'";
            return ExecuteNonQuery(sql, null);
        }

        public static int ExecuteNonQuery(string sql, params SqlParameter[] pms)
        {
            using (conn = new SqlConnection(constr))
            {
                using (cmd = new SqlCommand(sql, conn))
                {
                    if (pms != null)
                        cmd.Parameters.AddRange(pms);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 返回首行首列数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] pms)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] pms)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, constr))
            {
                if (pms != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(pms);
                }
                adapter.Fill(dt);
            }
            return dt;
        }

    }
}


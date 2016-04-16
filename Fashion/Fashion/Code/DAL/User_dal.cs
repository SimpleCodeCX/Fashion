using Fashion.Code.BLL;
using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fashion.Code.DAL
{
    public class User_dal
    {


        /// <summary>
        /// 通过用户名查询数据库里该用户的条数
        /// </summary>
        /// <param name="userName">用户名</param>       
        /// <returns></returns>
        public object GetAccountCount(string userName)
        {
            string sqlStr = "select count(*) from [tb_User] where User_Name=@userName";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }


       /// <summary>
       /// 获取用户的密码
       /// </summary>
        /// <param name="userName">用户的账号</param>
       /// <returns></returns>
        public object GetPassword(string userName)
        {
            string sqlStr = "select [User_Password] from [tb_User] where User_Name=@userName";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }



        /// <summary>
        /// 通过用户名查找用户的真实名字
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public object GetRealName (string userName)
        {
            string sqlStr = "select User_RealName from [tb_User] where User_Name = @userName";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }

       /// <summary>
        /// 通过通过用户名查找用户的出生年月日
       /// </summary>
       /// <param name="userName"></param>
       /// <returns></returns>
        public object GetBirthDate(string userName)
        {
            string sqlStr = "select User_BirthDate from [tb_User] where User_Name = @userName";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }



        /// <summary>
        /// 通过用户名查找用户的职业
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public object GetProfession(string userName)
        {
            string sqlStr = "select User_Profession from [tb_User] where User_Name = @userName";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }
        /// <summary>
        /// 通过用户名查找用户的手机
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public object GetPhoneNumber(string userName)
        {
            string sqlStr = "select  User_PhoneNumber from [tb_User] where User_Name = @userName";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }

        /// <summary>
        /// 通过用户名查找用户的学历
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public object GetEducationalBackground(string userName)
        {
            string sqlStr = "select  User_EducationalBackground from [tb_User] where User_Name = @userName";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }

        /// <summary>
        /// 通过用户名查找用户的兴趣
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public object GetInterest(string userName)
        {
            string sqlStr = "select User_Interest from [tb_User] where User_Name = @userName";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }

        /// <summary>
        /// 获取用户的个人基本信息（真实姓名，生日，职业，手机，学历，爱好）
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable GetPersonalInformation(string userName)
        {
            string sqlStr = "select User_RealName,User_BirthDate, User_Profession,User_PhoneNumber,User_EducationalBackground,User_Interest from [tb_User] where User_Name =  @userName";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
           return SqlHelper.ExecuteDataTable(sqlStr, parameters);
        
        }


        /// <summary>
        /// 通过用户名查询该用户的盐值，返回类型为object
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public object GetSalt(string userName)
        {
            string sqlStr = "select User_Salt from [tb_User] where User_Name=@userName";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }

        /// <summary>
        /// 通过用户名获取该用户的密码和盐值
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        //public DataTable GetPwdAndSalt(string userName)
        //{
        //    string sqlStr = "select [password],[salt] from [user] where userName=@userName";
        //    SqlParameter[] parameters = new SqlParameter[] { 
        //        new SqlParameter("@userName",userName)
        //    };
        //    return SqlHelper.ExecuteDataTable(sqlStr,parameters);
        //}
        public User_model GetPwdAndSaltModel(string userName)
        {
            string sqlStr = "select [User_Password],[User_Salt] from [tb_User] where User_Name=@userName";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@userName",userName)
            };
            DataTable dt=SqlHelper.ExecuteDataTable(sqlStr,parameters);
            if (dt.Rows.Count > 1)
            {
                //数据库出错处理，数据库里存在大于两条用户名一样的数据,抛出异常
                throw new Exception("more than 1 row was found");
            }
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            //把取回来的dt_User表的一行数据转化为model
            User_model model = new User_model();
            model.password = (string)row["User_Password"];
            model.salt = (string)row["User_Salt"];
            return model;
        }

       
        /// <summary>
        /// 通过用户名获取该用户的头像的url，返回结果为string型
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public object GetImgUrlTouXiang(string userName)
        {
            string sqlStr = "select User_TouXiangUrl from tb_User where User_Name=@userName";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr,parameters);
        }

       
        /// <summary>
        /// 实现将用户的头像的url插入到数据库的功能，url为相对路径如：/Images/TouXiang/userName.png
        /// 函数返回受影响的函数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="urlTouXiang">图片相对路径</param>
        /// <returns></returns>
        public int InsertUrlTouXiang(string userName,string urlTouXiang)
        {
            string sqlStr = "update tb_User set User_TouXiangUrl=@urlTouXiang where User_Name=@userName";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@urlTouXiang",urlTouXiang),
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteNonquery(sqlStr, parameters);
        }


        /// <summary>
        /// 实现将用户的全身照的url插入到数据库的功能，url为相对路径如：/Images/QuanShenZhao/userName.png
        /// 函数返回受影响的函数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="urlQuanShenZhao">图片相对路径</param>
        /// <returns></returns>
        public int InsertUrlQuanShenZhao(string userName, string urlQuanShenZhao)
        {
            string sqlStr = "update tb_User set User_QuanShenZhaoUrl=@urlQuanShenZhao where User_Name=@userName";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userName",userName),
                new SqlParameter("@urlQuanShenZhao",urlQuanShenZhao)
            };
            return SqlHelper.ExecuteNonquery(sqlStr,parameters);
        }


        //未编辑
        /// <summary>
        /// 封装sqlparameters的功能
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        //public SqlParameter[] pp(params Dictionary<string,string>)
        //{ 
        //    SqlParameter[] ppp=new SqlParameter[]{
        //    };
        //    return ppp;
        //}
      
        /// <summary>
        /// 查询tb_User表，获取指定的一行数据
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public User_model Get(string user_id)
        {
            DataTable dt = SqlHelper.ExecuteDataTable("select  * from tb_User where User_Id=@user_id", new SqlParameter("@user_id", user_id));
            if(dt.Rows.Count>1)
            {
                throw new Exception("more than 1 row was found");
            }
            if(dt.Rows.Count<=0)
            {
                return null;
            }
           /////////////////////////////////////////////////////////
            //还没写完，因为还没用到，所以以后再写
            DataRow row=dt.Rows[0];
            User_model model = ToModel(row);
            /////////////////////////////////////////////////////////
            return model;     
        }

        /// <summary>
        /// 将从数据库里取回的一行数据转化为User_model数据
        /// </summary>
        /// <param name="row">一行数据</param>
        /// <returns></returns>
        private static User_model ToModel(DataRow row)
        {
            User_model model=new User_model();
            /////////////////////////////////////////////////////////
            //还没写完，因为还没用到，所以以后再写
            model.userId=(int)row["User_Id"];
            model.userName = (string)row["User_Name"];
            /////////////////////////////////////////////////////////
            return model;
        }
     
        
       
      


        

       



        /// <summary>
        /// 用户注册，注册成功返回1
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="salt">盐值</param>
        /// <param name="password">密码</param>
        /// <param name="rankId">等级编号</param>
        /// <returns></returns>
        public int InsertRegister(string userName,string salt,string password,string rankId)
        {

            string sqlStr = "insert into [tb_User] (User_Name,User_Salt,[User_Password],User_RankId ) values (@userName,@salt,@password,@rankId)";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("userName",userName),
                new SqlParameter("salt",salt),
                new SqlParameter("password",password),
                new SqlParameter("rankId",rankId)
            };
            return SqlHelper.ExecuteNonquery(sqlStr,parameters);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 用户使用手机号注册，注册成功返回1
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="salt">盐值</param>
        /// <param name="password">密码</param>
        /// <param name="rankId">等级编号</param>
        /// <returns></returns>
        public int InsertPhoneNumberRegister(string userName, string salt, string password, string rankId, string phoneNumber)
        {

            string sqlStr = "insert into [tb_User] (User_Name,User_Salt,[User_Password],User_RankId,User_PhoneNumber) values (@userName,@salt,@password,@rankId,@phoneNumber)";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("userName",userName),
                new SqlParameter("@salt",salt),
                new SqlParameter("password",password),
                new SqlParameter("rankId",rankId),
                new SqlParameter("phoneNumber",phoneNumber)
            };
            return SqlHelper.ExecuteNonquery(sqlStr, parameters);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        //1.函数要首字母大写
        /// <summary>
        /// 用户使用邮箱注册，注册成功返回1
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="salt"></param>
        /// <param name="password"></param>
        /// <param name="rankId"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public int InsertEmailRegister(string userName, string salt, string password, string rankId, string email)
        {
            string sqlStr = "insert into [tb_User] (User_Name,User_Salt,[User_Password],User_RankId,User_Email) values (@userName,@salt,@password,@rankId,@email)";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("userName",userName),
                new SqlParameter("@salt",salt),
                new SqlParameter("password",password),
                new SqlParameter("rankId",rankId),
                new SqlParameter("email",email)
                 };
            return SqlHelper.ExecuteNonquery(sqlStr, parameters);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////

      
        //public DataTable Get(string[] columnNames,)
        //{ 
        //    if(columnNames.Length<=0)
        //    { }
        //    string sqlStr = "select ";
        //    sqlStr=sqlStr+columnNames[0];
        //    for(int i=1;i<columnNames.Length;i++)
        //    {
        //        sqlStr = sqlStr + "," + columnNames[i];
        //    }
        //    sqlStr = sqlStr + " from User where userId=@userId";
        //    SqlParameter[] parameters = new SqlParameter[]{
        //        new SqlParameter("@userId",userId)
        //    };

                
            
        //}
        /// <summary>
        /// 通过用户名查询该用户的ID，返回类型为object
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public object GetUserId(string userName)
        {
            string sqlStr = "select USER_ID from [tb_User] where User_Name = @userName";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }

    }
}
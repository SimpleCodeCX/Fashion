using Fashion.Code.DAL;
using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Fashion.Code.BLL
{
    public class People_bll
    {
        /////////////////////////////////////////////
        //user表
        ////////////////////////////////////////////
        /// <summary>
        ///  user表
        /// 判断是否存在该用户
        /// 结果返回1代表存在
        /// 结果返回0代表不存在
        /// 结果返回2代表数据库出错，因为数据库存在超过2条数据的该用户
        /// </summary>
        /// <param name="userName"></param>
        public int HavingUserName(string userName)
        {
            
            User_dal user = new User_dal();
            int accountCount = (int)user.GetAccountCount(userName);
            if (accountCount<=0)
            {
                return 0;
            }
            if (accountCount > 1)
            {
                return 2;
            }
            else
            {
                return 1;
            }  
        }

       


        /// <summary>
        /// 判断登录是否成功，成功返回true，失败返回false
        /// 使用者：People控制器里的ajaxMakeLogin
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool LoginYes(string userName,string password)
        {
            
            
            User_dal user_dal = new User_dal();
            /*object AccountCount = user_dal.GetAccountCount(userName);//用户的数量
            //null代表数据库不存在该数据，System.DBNull.Value代表数据库里存在数据，但是该字段的值为null
            if (AccountCount == null || AccountCount == System.DBNull.Value)
            {
                return false;
            }
            //如果用户的数量小于0
            if ((int)AccountCount <= 0)
            {
                return false;
            }
            if ((int)AccountCount > 1)
            {
                return false;
            }*/
            //以上判断存在该用户后，获取其盐值和密码
            User_model user_model = new User_model();
            user_model = user_dal.GetPwdAndSaltModel(userName);
            try
            {
                user_model = user_dal.GetPwdAndSaltModel(userName);
            }
            catch (Exception e)
            {
                //数据库异常处理，数据库里存在大于两条用户名一样的数据,抛出异常
                throw new Exception(e.ToString());
            }

            //finally { }
            
            string salt = user_model.salt; //颜值
            string realPassword = user_model.password; //密码
            //将盐值加在密码的后面，并转化为二进制
            byte[] pwdAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            //经过哈希算法加密后得到的二进制值
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(pwdAndSaltBytes);
            string hashPassword = Convert.ToBase64String(hashBytes);
            //判断密码是否正确
            if (realPassword == hashPassword)
            {
                return true;
            }
            else
            {
                return false;
            }      
        }

        /// <summary>
        /// 通过用户名获取该用户的头像的url，返回结果为string型
        /// 查询成功返回url的string格式
        /// null代表：查询到的数据为null，数据库中的User表里不存在该userName用户对应的数据
        ///           或查询到的该字段为null,即数据库存在该数据但数据库中的User表里用户名为userName的数据对应的User_TouXiangUrl为null
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public string GetImgUrlTouXiang(string userName)
        {
            User_dal user = new User_dal();
            object UrlTouXiangObj = user.GetImgUrlTouXiang(userName);
            if (UrlTouXiangObj == null || UrlTouXiangObj == System.DBNull.Value)
            {
                return null;
            }
            return UrlTouXiangObj.ToString();
        }

        /// <summary>
        /// 通过用户名得到用户Id，返回用户Id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GainUserId(string userName)
        {
            User_dal user = new User_dal();
            int userId = (int)user.GetUserId(userName);
            return userId;
        }

        
        /// <summary>
        /// 实现将用户的头像的url插入到数据库的功能，url为相对路径如：/Images/UserImages/TouXiang/userName.png
        /// 成功返回true 失败返回false
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="ImgExtension">图片扩展名</param>
        /// <returns></returns>
        public bool InsertUrlTouXiang(string userName, string ImgExtension)
        {
            string TouXiangUrl = "/Images/UserImages/TouXiang/" + userName + ImgExtension;
            User_dal user_dal = new User_dal();
            int NonqCount = user_dal.InsertUrlTouXiang(userName, TouXiangUrl); //受影响的函数
            if (NonqCount == 1)
            {
                return true;
            }
            else {
                return false;
            }
        }


        /// <summary>
        /// 实现将用户的全身照的url插入到数据库的功能，url为相对路径如：/Images/UserImages/QuanShenZhao/userName.png
        /// 成功返回true 失败返回false
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="ImgExtension">图片扩展名</param>
        /// <returns></returns>
        public bool InsertUrlQuanShenZhao(string userName, string ImgExtension)
        {
            string QuanShenZhaoUrl = "/Images/UserImages/TouXiang/" + userName + ImgExtension;
            User_dal user_dal = new User_dal();
            int NonqCount = user_dal.InsertUrlQuanShenZhao(userName, QuanShenZhaoUrl);//受影响的函数
            if (NonqCount == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }




        /////////////////////////////////////////////
        //user表和Rank表
        ////////////////////////////////////////////
        /// <summary>
        /// 实现注册功能，返回结果为int型
        /// 0代表注册成功；
        /// 1代表：数据插入出错，注册失败
        /// 2代表：查询到的数据为null，数据库中的等级表里不存在该rankName对应的数据
        /// 3代表：查询到的该字段为null,数据库中的等级表里等级名为rankName的数据对应的rankId为null
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="rankName">等级名（管理员，普通用户，时尚达人，专家）</param>
        /// <returns></returns>
        public int Register(string userName,string password,string rankName,string phoneNumberOrEmail)
        {
            //通过等级名得到等级编号
            Rank_dal rankDal = new Rank_dal();
            object rankIdObj = rankDal.GetRankId(rankName);
            if (rankIdObj == null)
            {
                return 2;
            }
            if (rankIdObj == System.DBNull.Value)
            {
                return 3;
            }
            string rankId = rankIdObj.ToString();

            //盐值
            string salt = Guid.NewGuid().ToString();
            //将盐值加在密码的后面，并转化为二进制
            byte[] pwdAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password+salt);
            //经过哈希算法加密后得到的二进制值
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(pwdAndSaltBytes);
            string hashPassword = Convert.ToBase64String(hashBytes);
            User_dal userDal = new User_dal();
            ////////////////////////////////////////////
            //通过正则表达式判断传进来的值是手机号还是邮箱
            //正则表达式字符串
            string emailStr =
            @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+$";
            //邮箱正则表达式对象
            Regex emailReg = new Regex(emailStr);
            if (emailReg.IsMatch(phoneNumberOrEmail))
            {
                if (userDal.InsertEmailRegister(userName, salt, hashPassword, rankId, phoneNumberOrEmail) == 1)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (userDal.InsertPhoneNumberRegister(userName, salt, hashPassword, rankId, phoneNumberOrEmail) == 1)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            /////////////////////////////////////////////////
            
        }



      





        /// <summary>
        /// 检查数据库得到的真实姓名
        /// </summary>
        /// <param name="useName"></param>
        /// <returns></returns>
        public string CheckRealName(string userName)
        {
            User_dal user = new User_dal();
            DataTable dt = user.GetPersonalInformation(userName);
            object RealName = dt.Rows[0][0];
            
            if (RealName == null || RealName == System.DBNull.Value)
            {
                return null;
            }
            else
            {
                return RealName.ToString();
            }
        }
       /// <summary>
        /// 检查数据库得到的出生日期
       /// </summary>
       /// <param name="userName"></param>
       /// <returns></returns>
        public string CheckBirthDate(string userName)
        {
            User_dal user = new User_dal();
            DataTable dt = user.GetPersonalInformation(userName);
            object BirthDate = dt.Rows[0][1];
            if (BirthDate == null || BirthDate == System.DBNull.Value)
            {
                return null;
            }
            else 
            {
                return String.Format("{0:yyyy\\/MM\\/dd}", BirthDate);
            }
           
        }
        /// <summary>
        /// 检查数据库得到的职业
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CheckProfession(string userName)
        {
            User_dal user = new User_dal();
            DataTable dt = user.GetPersonalInformation(userName);
            object Profession = dt.Rows[0][2];
            if (Profession == null || Profession == System.DBNull.Value)
            {
                return null;
            }
            else
            {
                return Profession.ToString(); 
            }

        }
        /// <summary>
        ///  检查数据库得到的手机号
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CheckPhoneNumber(string userName)
        {
            User_dal user = new User_dal();
            DataTable dt = user.GetPersonalInformation(userName);
            object PhoneNumber = dt.Rows[0][3];
            if (PhoneNumber == null || PhoneNumber == System.DBNull.Value)
            {
                return null;
            }
            else
            {
                return PhoneNumber.ToString();
            }

        }
        /// <summary>
        /// 检查数据库得到的学历
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CheckEducationalBackground(string userName)
        {
            User_dal user = new User_dal();
            DataTable dt = user.GetPersonalInformation(userName);
            object EducationalBackground = dt.Rows[0][4];
            if (EducationalBackground == null || EducationalBackground == System.DBNull.Value)
            {
                return null;
            }
            else
            {
                return EducationalBackground.ToString();
            }

        }

        /// <summary>
        /// 检查数据库得到的兴趣
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CheckInterest(string userName)
        {
            User_dal user = new User_dal();
            DataTable dt = user.GetPersonalInformation(userName);
            object Interest = dt.Rows[0][5];
            if (Interest == null || Interest == System.DBNull.Value)
            {
                return null;
            }
            else
            {
                return Interest.ToString();
            }

        }



        

    }
}
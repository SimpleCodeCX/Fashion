using Fashion.Code.BLL;
using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashion.Controllers
{
     

    public class PeopleController : Controller
    {
        //
        // GET: /People/
        public ActionResult Expert_Register()
        {

            return View();
        }
        public ActionResult Index()
        {

            LoginStatusConfig();//配置登录状态
            if (Session["userName"] == null)
            {
                return Content("请先登录");
            }
            People_bll user = new People_bll();
            string userName = Session["userName"].ToString();
            //检测姓名是否为空
            if (user.CheckRealName(userName) == null)
            {
                ViewData["realName"] = "请输入您的真实姓名";
            }
            else
            {
                ViewData["realName"] = user.CheckRealName(userName);
            }
            // 检测出生年月是否为空
            if (user.CheckBirthDate(userName) == null)
            {
                ViewData["BirthDate"] = "请输入您的出生年月";
            }
            else
            {
                ViewData["BirthDate"] = user.CheckBirthDate(userName);
            }
            //检测职业是否为空
            if (user.CheckProfession(userName) == null)
            {
                ViewData["Profession"] = "请输入您的职业";
            }
            else
            {
                ViewData["Profession"] = user.CheckProfession(userName);
            }
            // 检测手机号是否为空
            if (user.CheckPhoneNumber(userName) == null)
            {
                ViewData["PhoneNumber"] = "请输入您的手机号";
            }
            else
            {
                ViewData["PhoneNumber"] = user.CheckPhoneNumber(userName);
            }
            //检测学历是否为空
            if (user.CheckEducationalBackground(userName) == null)
            {
                ViewData["CheckEducationalBackground"] = "请输入您的手机号";
            }
            else
            {
                ViewData["CheckEducationalBackground"] = user.CheckEducationalBackground(userName);
            }
            //检测爱好是否为空
            if (user.CheckInterest(userName) == null)
            {
                ViewData["Interest"] = "请输入您的手机号";
            }
            else
            {
                ViewData["Interest"] = user.CheckInterest(userName);
            }
            return View();
        }
       


       
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            //throw new Exception("可以抛出一个异常");
            string finshRegister1 = Request["finshRegister"]; //获取一个值，1代表用户是通过注册跳转到该函数的，0则相反
            if (Convert.ToInt32(finshRegister1) == 1)
            {
                ViewData["finshRegister"] = 1;
                return View();
            }
            else
            {
                ViewData["finshRegister"] = 0;
                return View();
            }
        }
        /// <summary>
        /// 判断登录是否成功，登录成功返回1，登录失败返回0，数据库异常返回2
        /// login
        /// </summary>
        /// <returns></returns>
        public ActionResult ajaxMakeLogin()
        {
            People_bll people = new People_bll();
            string username = Request["username"];
            string password = Request["password"];
            bool login = false;  //true代表账号密码都正确
            try
            {
               login = people.LoginYes(username, password);
            }
            catch (Exception e)
            {
                //数据库异常处理，数据库里存在大于两条用户名一样的数据
                ErrorMessage_bll errorMessage_bll = new ErrorMessage_bll();
                errorMessage_bll.InsertErrorMessage("数据库出错", "数据库里tb_User表存在大于2条用户名一样的数据，用户名为：" + username);
                return Content("2");
            }
            if (login)
            {
                //登录成功之后，保存用户的用户名，权限等资料到session
                Session["userName"] = username;
                Session["rank"] = "rank";

                return Content("1");
            }
            else
            {
                return Content("0");
            }
            
        }

        /// <summary>
        /// 根据用户名判断账号名是否存在，存在返回1，不存在返回0，数据库出错返回2
        /// 用于Login页面、Register页面
        /// </summary>
        /// <returns></returns>      
        public ActionResult ajaxUserName()
        {
            string userName = Request["userName"].ToString();
            People_bll people = new People_bll();
            //数据库出错处理，数据库里存在大于两条用户名一样的数据
            if (people.HavingUserName(userName) >= 2)
            {
                ErrorMessage_bll errorMessage_bll = new ErrorMessage_bll();
                errorMessage_bll.InsertErrorMessage("数据库出错","数据库存在2条用户名一样的数据，用户名为："+userName);
                //return View("PeopleHomePage");
                //throw new Exception("数据库出错，存在两条用户名一样的数据。");
            }
            return Content(people.HavingUserName(userName).ToString());
        }


        /// <summary>
        // 测试用的
        /// </summary>
        /// <returns></returns>
        
        public ActionResult ajax()
        {
         //return JavaScript("window.location.href = '../Topic/Answer'");
            return View();
            //return RedirectToAction("Answer","Home");  
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }





        /// <summary>
        /// 实现注册功能，把数据保存到数据库
        /// </summary>
        /// <returns></returns>
        public ActionResult makeRegister()
        {
            People_bll people = new People_bll();
            string username = Request["username"];
            string password = Request["password"];
            string phoneNumberOrEmail = Request["phoneNumberOrEmail"];
            if (people.Register(username, password, "普通用户", phoneNumberOrEmail) == 0)
            {
                return RedirectToAction("Login", new { finshRegister = 1 });
            }
            else
            {
                return View();
            }

        }
        /// <summary>
        /// 我的主页
        /// </summary>
        /// <returns></returns>
        public ActionResult PeopleHomePage()
        {
            //return Content(DateTime.Now.ToShortDateString());
            return View();
        }

        public ActionResult Change_Data()
        {
            LoginStatusConfig();//配置登录状态
            if (Session["userName"] == null)
            {
                return Content("请先登录");
            }
            return View();
        }
        /// <summary>
        /// 获取前端传过来的头像的图片的base64数据，保存到本地/Images/UserImages/TouXiang/文件夹下，并且在数据库里添加纪录，成功返回1
        /// 用于Change_Data页面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadTouXiang()
        {
            //判断是否登录，未登录返回0
            if (Session["userName"] == null)
            {
                return Content("0");
            }
            string userName = Session["userName"].ToString();
            byte[]imgBase64Byte=Convert.FromBase64String(Request["data1"]);//将图片数据转化为base64的格式
            System.IO.MemoryStream ms=new System.IO.MemoryStream(imgBase64Byte);
            System.Drawing.Bitmap bitmap=new System.Drawing.Bitmap(ms);
            //接下来将图片保存在本地
            bitmap.Save(Server.MapPath("~/Images/UserImages/TouXiang/" + userName + ".png"), System.Drawing.Imaging.ImageFormat.Png);
            People_bll people = new People_bll();
            //将图片的路径保存到数据库
            people.InsertUrlTouXiang(userName,".png");
            return Content("1");
        }


        /// <summary>
        /// 获取前端传过来的全身照的图片的base64数据，保存到本地/Images/UserImages/QuanShenZhao/文件夹下，并且在数据库里添加纪录，成功返回1
        /// 用于Change_Data页面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadQuanShenZhao()
        {
            //判断是否登录，未登录返回0
            if (Session["userName"] == null)
            {
                return Content("0");
            }
            string userName = Session["userName"].ToString();
            byte[] imgBase64Byte = Convert.FromBase64String(Request["data1"]);//将图片数据转化为base64的格式
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imgBase64Byte);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(ms);
            //接下来将图片保存在本地
            bitmap.Save(Server.MapPath("~/Images/UserImages/QuanShenZhao/" + userName + ".png"), System.Drawing.Imaging.ImageFormat.Png);
            People_bll people = new People_bll();
            //将图片的路径保存到数据库
            people.InsertUrlQuanShenZhao(userName, ".png");
            return Content("1");
        }



        ///// <summary>
        ///// 验证登录是否成功；
        ///// 若登录失败，设置ViewData["LoginYes"] = 0；
        ///// 若登录成功，设置ViewData["LoginYes"] = 1；并且从数据库里取出用户头像的链接：ViewData["TouXiangUrl"] =...；
        ///// </summary>
        //public void CheckLogin()
        //{
        //    if (Session["userName"] == null)
        //    {//未登录
        //        ViewData["LoginYes"] = 0;
        //    }
        //    else
        //    {//已登录
        //        ViewData["LoginYes"] = 1;
        //        ViewData["userName"] = Session["userName"].ToString();
        //        People_bll peopleBll = new People_bll();
        //        ViewData["TouXiangUrl"] = peopleBll.GetImgUrlTouXiang(Session["userName"].ToString());//从数据库里获取头像url
        //    }
        //}



        /// <summary>
        /// 配置用户登录状态
        /// 如果已登录，返回true，并且设置登录状态：设置ViewData["LoginYes"] = 1；并且从数据库里取出用户头像的链接：ViewData["TouXiangUrl"] =...；
        /// 未登录返回false,并且设置ViewData["LoginYes"] = 0
        /// </summary>
        /// 

        public bool LoginStatusConfig()
        {
            if (Session["userName"] == null)
            {//未登录
                ViewData["LoginYes"] = 0;
                return false;
            }
            //已登录
            ViewData["LoginYes"] = 1;
            ViewData["userName"] = Session["userName"].ToString();
            People_bll peopleBll = new People_bll();
            ViewData["TouXiangUrl"] = peopleBll.GetImgUrlTouXiang(Session["userName"].ToString());//从数据库里获取头像url
            return true;
        }
     
    }
}

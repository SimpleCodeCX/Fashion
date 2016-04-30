using Fashion.Code.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashion.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 退出登录状态，退出成功返回1，失败返回0
        /// login
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxMakeUnLogin()
        {
            Session["userName"] = null;
            Session["rank"] = null;
            return Content("1");

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

    }
}

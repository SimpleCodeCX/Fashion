using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Fashion.Models;
using Fashion.Code.BLL;
namespace Fashion.Controllers
{
   
  
    public class TopicController : Controller
    {
        //
        // GET: /Topic/  
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Test2( )
        {
            //上传头像
            var path = System.IO.Path.Combine(Server.MapPath("~/test"), Request.Files[0].FileName);
            Request.Files[0].SaveAs(path);
            return Content("成功");
        }
        
        
        
        public ActionResult Home()
        {

            CheckLogin();//验证登录
            return View();
            
        }
        /// <summary>
        /// 获取Home页面的数据，帖子遍历的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxHomeGetData()
        {
            //string theme = "{ 'name': 'dong' }";
            List<Dictionary<string,object>>list=new List<Dictionary<string,object>>();
            Dictionary<string,object>dic=new Dictionary<string,object>();
            dic.Add("name1","dong");
            //list.Add(dic);
            dic.Add("name2","xu");
            list.Add(dic);
            Dictionary<string, object> dic2 = new Dictionary<string, object>();
            dic2.Add("name3", "dong2");
            //list.Add(dic);
            dic2.Add("name4", "xu2");
            list.Add(dic2);
            JavaScriptSerializer serializer1 = new JavaScriptSerializer();
            string json = serializer1.Serialize(list);
            return Content(json);
            //return Content(theme);
        }

        
        
        public ActionResult Answer()
        {
            //ViewData["a"]=Request["type"].ToString();
            //ViewData["a"]="a";
            return View();
        }
        public ActionResult AjaxAnswer()
        {
            return Content("HelloWorld This is Answer");
        }
        public ActionResult Post()
        {
            CheckLogin();//验证登录
            return View();
        }
        public ActionResult fabu()
        {
            return View();
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <returns></returns>
        public ActionResult postData()
        {

            Post_bll Post = new Post_bll();
            Theme_bll themeName = new Theme_bll();
            People_bll User = new People_bll();
            string caption = Request["question"].ToString();
            string content = Request["content"].ToString();
            string userName = Session["userName"].ToString();
            int userId = User.GainUserId(userName);
            string theme = Request["theme"].ToString();
            int themeId = themeName.CollocateThemeId(theme);


            //判断用户提问是否成功，成功返回主页面，失败返回提问页面
            if (Post.finshInsert(caption, content, userId, themeId) == 1)
            {
                return RedirectToAction("Home");
            }
            else
            {
                return RedirectToAction("Post");
            }
        }
        /// <summary>
        /// 验证登录是否成功；
        /// 若登录失败，设置ViewData["LoginYes"] = 0；
        /// 若登录成功，设置ViewData["LoginYes"] = 1；并且从数据库里取出用户头像的链接：ViewData["TouXiangUrl"] =...；
        /// </summary>
        public void CheckLogin()
        {
            if (Session["userName"] == null)
            {//未登录
                ViewData["LoginYes"] = 0;

            }
            else
            {//已登录
                ViewData["LoginYes"] = 1;
                ViewData["userName"] = Session["userName"].ToString();
                People_bll peopleBll = new People_bll();
                ViewData["TouXiangUrl"] = peopleBll.GetImgUrlTouXiang(Session["userName"].ToString());//从数据库里获取头像url
            }
        }

    }
}

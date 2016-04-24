using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashion.Controllers
{
    public class TemplateController : Controller
    {
        //
        // GET: /Template/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 模板页，查看我的  “回答 提问 收藏 ” 帖子，
        /// </summary>
        /// <returns></returns>
        public ActionResult MyPostTemplate()
        {
            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.kindeditor.asp.net
{
    /// <summary>
    /// ExpertAnswerUpload_json 的摘要说明
    /// </summary>
    public class ExpertAnswerUpload_json : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //1.相对目录路径,用于返回到前端
            //2.物理目录路径,用于保存图片到本地
            //3.定义允许上传的文件扩展名
            //4.获取文件
            //5.判断文件的扩展名是否为允许的
            //6.定义文件名,保存图片
            //1,2
            string xiangDuiPath = "/Images/ConsultExpertAnswerImages/";
            string wuLiPath = context.Server.MapPath("~//Images/ConsultExpertAnswerImages/");
            //3.
            System.Collections.Hashtable extTable = new System.Collections.Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");//可以保存的图片类型
            extTable.Add("flash", "swf,flv"); //也定义一些别的，留着以后扩展
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");
            //4.
            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            //5.
            //获取文件扩展名,去掉一点，并且转化为小写
            string imgExt = System.IO.Path.GetExtension(imgFile.FileName).Substring(1).ToLower();
            //通过extTable取得允许的文件扩展名数组
            string[] strExt = extTable["image"].ToString().Split(',');

            //定义一个hashtable，待会转化为json数据返回客户端
            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            //定义json序列化类
            System.Web.Script.Serialization.JavaScriptSerializer TheSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            if (string.IsNullOrEmpty(imgExt) || Array.IndexOf(strExt, imgExt) == -1)
            {
                hash["error"] = 1;
                hash["message"] = "上传的文件扩展名为不允许的扩展名\n只允许" + extTable["image"].ToString() + " 格式！";
                context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                string jsonData = TheSerializer.Serialize(hash);
                context.Response.Write(jsonData);
                context.Response.End();
            }
            //6.生成唯一的字符
            //用guid
            string fileName = Guid.NewGuid().ToString() + "." + imgExt;
            //或者用时间
            string fileName2 = DateTime.Now.ToString("yyyyMMddHHmmss_ffff") + "." + imgExt;
            xiangDuiPath += fileName;
            wuLiPath += fileName;
            //保存
            imgFile.SaveAs(wuLiPath);
            //返回客户端json数据
            hash["error"] = 0;
            hash["url"] = xiangDuiPath;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string s = js.Serialize(hash);
            context.Response.Write(s);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
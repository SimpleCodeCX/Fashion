using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Models
{
    public class Post_model
     {
        //帖子信息
        public int postId;//帖子编码
        public string postCaption;//帖子标题
        public string postContent;//帖子内容
        public string postSenderId;//发布者编号
        public int postModuleId;//模块编码
        public int postThemeId;//板块（话题）编码
        public int postSupportCount;//点赞数
        public DateTime postDate;//发帖日期
        public string postHtmlUrl;//帖子静态页面地址
        public int commentCount;//评论条数
        public User_model User;//User_model类，拥有用户的信息
        public Theme_model Theme;//Theme_model类，帖子的话题
        public Post_model()
        {
            User = new User_model();
            Theme = new Theme_model();
        }
    }
   
}
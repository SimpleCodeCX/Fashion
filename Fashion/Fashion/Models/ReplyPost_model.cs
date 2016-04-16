using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Models
{
    public class ReplyPost_model
    {
        //回帖信息
        public int replyPostId;//回帖编号
        public int PostId;//帖子编号
        public string replyPostReplyerId;//回帖者编号
        public string replyPostContent;//回帖内容
        public int replyPostSupportCount;//点赞数
        public DateTime replyPostDate;//回帖日期
        public string replyPostHtmlUrl;//回帖帖子的静态页面地址
    }
}
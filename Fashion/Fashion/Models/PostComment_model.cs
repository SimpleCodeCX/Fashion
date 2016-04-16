using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Models
{
    public class PostComment_model
    {
        //原贴评论
        public int postCommentId;//评论编号
        public int postCommentPostId;//帖子编号
        public string postCommentCommenterId;//评论者编号
        public string postCommentBeCommenterId;//被评论者编号
        public string postCommentContent;//评论内容
        public DateTime postCommentDate; //评论时间
        public int postCommentSupportCount;//点赞数
        public int postType;//帖子的类型，1代表原贴，2代表回帖
        public User_model Commenter;//User_model类，评论者
        public User_model BeCommenter;//User_model类，被评论者
        public PostComment_model() {
            Commenter = new User_model();
            BeCommenter = new User_model();
        }
    }
}
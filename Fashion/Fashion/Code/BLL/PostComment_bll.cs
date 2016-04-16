using Fashion.Code.DAL;
using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Code.BLL
{
    public class PostComment_bll
    {
        /// <summary>
        /// 获取评论数据
        /// </summary>
        /// 这里还没有分条数，即这里全部取出来
        /// <param name="postId">帖子的id</param>
        /// <param name="postType">帖子类型</param>
        /// <returns></returns>
        public List<PostComment_model> GetPostComment(int postId, int postType)
        { 
            PostComment_dal postComment_dal=new PostComment_dal();
            return postComment_dal.GetPostComment(postId,postType);
        }
        /// <summary>
        /// 向数据库插入一条帖子评论
        /// 成功返回1，错误返回0
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <param name="commenterId">评论者</param>
        /// <param name="beCommenterId">被评论者</param>
        /// <param name="content">评论内容</param>
        /// <param name="datetime">评论时间</param>
        /// <param name="supportCount">点赞数</param>
        /// <param name="postType">帖子类型</param>
        /// <returns></returns>
        public int InsertComment(int postId, int commenterId, int beCommenterId, string content, DateTime datetime, int postType)
        {
            PostComment_dal postComment_dal = new PostComment_dal();
            if (postComment_dal.InsertComment(postId, commenterId, beCommenterId, content, datetime, postType) == 1)
            {
                return 1;
            }
            else {
                return 0;
            }
        }

    }
}
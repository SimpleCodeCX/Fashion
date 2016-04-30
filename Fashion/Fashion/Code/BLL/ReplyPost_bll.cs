using Fashion.Code.DAL;
using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Code.BLL
{
    public class ReplyPost_bll
    {
        /// <summary>
        /// 获取原帖为postId的所有回帖
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public List<ReplyPost_model> GetReplyPost(int postId)
        {
            ReplyPost_dal replyPost_dal = new ReplyPost_dal();            
            return replyPost_dal.GetReplyPost(postId);

        }

    }
}
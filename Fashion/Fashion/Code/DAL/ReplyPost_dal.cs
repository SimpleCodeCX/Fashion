using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fashion.Code.DAL
{
    public class ReplyPost_dal
    {
        /// <summary>
        /// 获取原帖为postId的所有回帖
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public List<ReplyPost_model> GetReplyPost(int postId)
        {
            string sqlStr = "select * from ReplyPostView where postId=@postId";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@postId",postId)
            };
            DataTable dataTable = SqlHelper.ExecuteDataTable(sqlStr,parameters);
            List<ReplyPost_model> replyPost_modelList = new List<ReplyPost_model>();
            foreach (DataRow row in dataTable.Rows)
            {
                replyPost_modelList.Add(ToModel(row));
            }
            return replyPost_modelList;
        }


        /// <summary>
        /// 将一条数据转化为ReplyPost_model数据
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public ReplyPost_model ToModel(DataRow row)
        {
            ReplyPost_model replyPost_model = new ReplyPost_model();
            replyPost_model.Commenter.userId = (int)row["userId"];
            replyPost_model.Commenter.userName = row["userName"].ToString();
            replyPost_model.Commenter.signature = (row["signature"] != DBNull.Value ? row["signature"].ToString() : null);
            replyPost_model.Commenter.touXiangUrl = row["touXiangUrl"].ToString();
            replyPost_model.replyPostId = (int)row["id"];
            replyPost_model.PostId = (int)row["postId"];
            replyPost_model.replyPostContent = row["content"].ToString();
            if (row["datetime"] != null)
            {
                replyPost_model.replyPostDate = (DateTime)row["datetime"];
            }
            replyPost_model.replyPostHtmlUrl = row["htmlUrl"].ToString();
            replyPost_model.replyPostSupportCount = (int)row["supportCount"];
            replyPost_model.commentCount = (row["commentCount"] != DBNull.Value ? (int)row["commentCount"] : 0);
            return replyPost_model;
        }
    }
}
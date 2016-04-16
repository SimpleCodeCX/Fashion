using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Code.DAL
{
    public class PostPhoto_dal
    {
        /// <summary>
        /// 将帖子编号为postId的图片路径插入到数据库，图片路径的条数不确定
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="imgUrl"></param>
        /// <param name="postType"></param>
        /// <returns></returns>
        public int InsertPhotoUrl(int postId, string[] imgUrl, int postType)
        {
            string sqlStr = "";
            for (int i = 0; i < imgUrl.Length; i++)
            {
                sqlStr = sqlStr + @"insert tb_PostPhoto(PostPhoto_PostId,PostPhoto_PhotoUrl,PostPhoto_PostType) values(" + postId + ",'" + imgUrl[i] + "'" + "," + postType + ")   ";
            }
            return SqlHelper.ExecuteNonquery(sqlStr);
        }
    }
}
using Fashion.Code.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Code.BLL
{
    public class PostPhoto_bll
    {

        /// <summary>
        /// 将帖子编号为postId的图片路径插入到数据库，图片路径的条数不确定
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public int InsertPhotoUrl(int postId, string[] imgUrl,int postType)
        {
            PostPhoto_dal postPhoto = new PostPhoto_dal();
            return postPhoto.InsertPhotoUrl(postId, imgUrl,postType);
        }
    }
}
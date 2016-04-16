using Fashion.Code.DAL;
using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Code.BLL
{
    public class Post_bll
    {
        /// <summary>
        /// 判断是否存在该标题
        /// 结果返回1代表存在
        /// 结果返回0代表不存在
        /// 结果返回2代表数据库出错，因为数据库存在超过2条数据的该标题
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        public int havingCaption(string caption)
        {
            Post_dal Caption = new Post_dal();
            int accountCount = (int)Caption.getCaptionCount(caption);
            if (accountCount <= 0)
            {
                return 0;
            }
            if (accountCount > 1)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 通过标题查询数据库获得postId
        /// 结果返回1代表数据库出错
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        public int GetPostId(string caption)
        {
            Post_dal post_dal = new Post_dal();
            object postId = post_dal.GetPostId(caption);
            if (postId == null || postId == System.DBNull.Value)
            {
                return 0;
            }
            return (int)postId;
        }

        /// <summary>
        /// 插入问题，问题说明，提问人
        /// 成功返回1
        /// 失败返回0
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="content"></param>
        /// <param name="postsender"></param>
        /// <returns></returns>
        public int finshInsert(string caption, string content, int postsenderId, int themeId, string staticHtmlPath, DateTime datetime)
        {
            Post_dal insert = new Post_dal();
            if (insert.insertCaption(caption, content, postsenderId, themeId,staticHtmlPath, datetime) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// 获取帖子的10条数据
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="min">第一条数据id</param>
        /// <param name="max">最后一条数据id</param>
        public List<Post_model> GetPost(int page,int min=1, int max=10)
        {
            Post_dal post_dal = new Post_dal();
            return post_dal.GetPost(page,min,max);
        }

    }
}
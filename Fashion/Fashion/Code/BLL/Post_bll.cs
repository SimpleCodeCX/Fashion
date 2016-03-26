using Fashion.Code.DAL;
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
        /// 插入问题，问题说明，提问人
        /// 成功返回1
        /// 失败返回0
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="content"></param>
        /// <param name="postsender"></param>
        /// <returns></returns>
        public int finshInsert(string caption, string content, int postsenderId, int themeId)
        {
            Post_dal insert = new Post_dal();
            if (insert.insertCaption(caption, content, postsenderId, themeId) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
    }
}
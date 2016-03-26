using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fashion.Code.DAL
{
    public class Post_dal
    {
         /// <summary>
        /// 通过标题查询数据库里该标题的条数
        /// </summary>
        /// <param name="Caption"></param>
        /// <returns></returns>
        public object getCaptionCount(string caption)
        {
            string sqlStr = "select count(*) from [Post] where caption =@caption";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@caption",caption)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }

        /// <summary>
        /// 插入数据 标题   内容  提问题的人的编号    主题Id  
        /// 编号从tb_User里面拿，有空在写
        /// 插入成功返回1
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="content"></param>
        /// <param name="postsender"></param>
        /// <param name="themeId"></param>
        /// <returns></returns>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        public int insertCaption(string caption, string content, int postsenderId, int themeId)
        {

            string sqlStr = "insert into [tb_Post] ( Post_Caption,Post_Content,Post_SenderId,Post_ThemeId) values (@caption,@content,@postsenderId,@themeId)";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@caption",caption),
                new SqlParameter("@content",content),
                new SqlParameter("@postsenderId",postsenderId),
                new SqlParameter("@themeId",themeId)
            };
            return SqlHelper.ExecuteNonquery(sqlStr, parameters);
        }
    }
    
}
using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// 通过标题查询数据库获得postId
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        public object GetPostId(string caption)
        {
            string sqlStr = "select Post_Id from tb_Post where Post_Caption=@caption";
            SqlParameter[] parameters = new SqlParameter[]{
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
        public int insertCaption(string caption, string content, int postsenderId, int themeId, string staticHtmlPath, DateTime datetime)
        {

            string sqlStr = "insert into [tb_Post] ( Post_Caption,Post_Content,Post_SenderId,Post_ThemeId,Post_HtmlUrl,Post_Date) values (@caption,@content,@postsenderId,@themeId,@staticHtmlPath,@datetime)";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@caption",caption),
                new SqlParameter("@content",content),
                new SqlParameter("@postsenderId",postsenderId),
                new SqlParameter("@themeId",themeId),
                new SqlParameter("@staticHtmlPath",staticHtmlPath),
                new SqlParameter("@datetime",datetime)
            };
            return SqlHelper.ExecuteNonquery(sqlStr, parameters);
        }

        /// <summary>
        /// 获取帖子的10条数据
        /// </summary>
        /// <returns></returns>        
      /// <param name="page">页数</param>
      /// <param name="min">第一条数据id</param>
      /// <param name="max">最后一条数据id</param>
      /// <returns></returns>
        public List<Post_model> GetPost(int page,int min, int max)
        {
            string sqlStr = @"select * from(
                                                          select ROW_NUMBER() over(order by id) orderNumber,* from PostView
                                                               ) as postTable 
                                                      where postTable.orderNumber between @min and @max";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@min",min*page),
                new SqlParameter("@max",max*page)
            };
            DataTable dataTable = SqlHelper.ExecuteDataTable(sqlStr, parameters);
            List<Post_model> post_modelList = new List<Post_model>();
            foreach(DataRow row in dataTable.Rows)
            {
                post_modelList.Add(ToModel(row));
            }
            return post_modelList;
        }

        /// <summary>
        /// 获取全部帖子数据
        /// </summary>
        /// <returns></returns>
        public List<Post_model> GetAllPost()
        {
            string sqlStr = "select * from PostView";
            DataTable dataTable = SqlHelper.ExecuteDataTable(sqlStr);
            List<Post_model> post_modelList = new List<Post_model>();
            foreach (DataRow row in dataTable.Rows)
            {
                post_modelList.Add(ToModel(row));
            }
            return post_modelList;

        }

        /// <summary>
        /// 获取帖子编号为postId的数据,一条数据
        /// </summary>
        /// <param name="postId">帖子编号</param>
        /// <returns></returns>
        public Post_model GetOnePost(int postId)
        {
            string sqlStr = "select * from PostView where id=@postId";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@postId",postId)
            };
            DataTable dataTable = SqlHelper.ExecuteDataTable(sqlStr, parameters);
            Post_model post_model= new Post_model();
            post_model = ToModel(dataTable.Rows[0]);
            return post_model;
        }
        
        /// <summary>
        /// 将一条数据转化为Post_model数据
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public Post_model ToModel(DataRow row)
        {
            Post_model post_model = new Post_model();
            post_model.User.userId = (int)row["userId"];
            post_model.User.userName = row["userName"].ToString();
            post_model.User.signature = (row["signature"] != DBNull.Value ? row["signature"].ToString() : null);
            post_model.User.touXiangUrl = row["touXiangUrl"].ToString();
            post_model.postId = (int)row["id"];
            post_model.postCaption = row["caption"].ToString();
            post_model.postContent = row["content"].ToString();
            post_model.postDate = (DateTime)row["datetime"];
            post_model.postHtmlUrl = row["htmlUrl"].ToString();
            post_model.postSupportCount = (int)row["supportCount"];
            post_model.Theme.themeName = row["themeName"].ToString();
            post_model.Theme.themeId = (int)row["themeId"];
            post_model.commentCount = (row["commentCount"] != DBNull.Value ? (int)row["commentCount"] : 0);
            post_model.tuiTieCount = (row["tuiTieCount"] != DBNull.Value ? (int)row["tuiTieCount"] : 0);
            return post_model;
        }
        
       /// <summary>
       /// 通过原贴的帖子id获取该贴的点赞数
       /// </summary>
       /// <param name="postId"></param>
        public object GetPostSupportCount(int postId)
        {
            string sqlStr="select Post_SupportCount from tb_Post where Post_Id=@postId";
            SqlParameter[]parameters=new SqlParameter[]{
                new SqlParameter("@postId",postId)
            };
            return SqlHelper.ExecuteScalar(sqlStr,parameters);
        }


        /// <summary>
        /// 通过postId更新帖子，让点赞数+1
        /// 返回受影响的行数
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public int UpdateSupportCountAdd1(int postId)
        {
            string sqlStr = "update tb_Post set Post_SupportCount=Post_SupportCount+1 where Post_Id=@postId";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@postId",postId)
            };
            return SqlHelper.ExecuteNonquery(sqlStr, parameters);
        }
        /// <summary>
        /// 通过postId更新帖子，让点赞数-1
        /// 返回受影响的行数
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public int UpdateSupportCountReduce1(int postId)
        {
            string sqlStr = "update tb_Post set Post_SupportCount=Post_SupportCount-1 where Post_Id=@postId";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@postId",postId)
            };
            return SqlHelper.ExecuteNonquery(sqlStr, parameters);
        }


    }
    
}
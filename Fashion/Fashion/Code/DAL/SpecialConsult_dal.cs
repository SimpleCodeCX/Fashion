using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fashion.Code.DAL
{
    public class SpecialConsult_dal
    {

        /// <summary>
        /// 通过专家解答的特定咨询staticConsultAnswerHtml查询该条数据的id
        /// </summary>
        /// <param name="staticConsultAnswerHtml"></param>
        /// <returns></returns>
        public object GetConsultAnswerId(string staticConsultAnswerHtml)
        {
            string sqlStr = "select SpecialConsultAnswer_Id from tb_SpecialConsultAnswer where SpecialConsultAnswer_HtmlUrl=@staticConsultAnswerHtml";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@staticConsultAnswerHtml",staticConsultAnswerHtml)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
            
        }
        /// <summary>
        /// 通过特定咨询的帖子编号specialConsultId查询数据库的tb_SpecialConsult，获取特定咨询的
        /// 用户咨询数据
        /// </summary>
        /// <param name="specialConsultId">特定咨询的帖子编号</param>
        /// <returns></returns>
        public SpecialConsult_model GetOneConsultData(int specialConsultId)
        {
            string sqlStr = @"select consult.SpecialConsult_Id id,consult.SpecialConsult_Caption caption,consult.SpecialConsult_Occasion occasion,
                                             	   consult.SpecialConsult_UserPhotoUrl geRenZhao,consult.SpecialConsult_LikeStyleUrl likeStyleUrl,
                                             	   consult.SpecialConsult_DislikeStyleUrl dislikeStyleUrl,consult.SpecialConsult_Detail detail,
                                             	   theUser.User_Id userId,theUser.User_Name userName,theUser.User_Height height,theUser.User_Weight weight,
                                             	   theUser.User_YaoWei yaoWei,theUser.User_LegLength tuiChang,theUser.User_ThighGirth daTuiWei,
                                             	   theUser.User_SkinColor skinColor,theUser.User_TunWei tunWei,theUser.User_ArmGirth biWei,
                                             	   theUser.User_XiongWei xiongWei,theUser.User_CalfGirth xiaoTuiWei,
                                             	   expert.User_Id expertId,expert.User_Name expertName
                                            from (tb_SpecialConsult consult 
                                                           left join tb_User theUser on consult.SpecialConsult_UserId=theUser.User_Id)
                                               			   left join tb_User expert on consult.SpecialConsult_ExpertId=expert.User_Id
                                            where consult.SpecialConsult_Id=@specialConsultId";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@specialConsultId",specialConsultId)
            };
            DataTable dataTable=SqlHelper.ExecuteDataTable(sqlStr, parameters);
            SpecialConsult_model specialConsult_model = ToModel(dataTable.Rows[0]);
            return specialConsult_model;
        }

        public SpecialConsult_model ToModel(DataRow row)
        {
            SpecialConsult_model specialConsult_model = new SpecialConsult_model();
            specialConsult_model.id = (int)row["id"];
            specialConsult_model.caption = row["caption"].ToString();
            specialConsult_model.userPhotoUrl = row["geRenZhao"].ToString();
            specialConsult_model.occasion = row["occasion"].ToString();
            specialConsult_model.likeStyleUrl = row["likeStyleUrl"].ToString();
            specialConsult_model.dislikeStyleUrl = row["dislikeStyleUrl"].ToString();
            specialConsult_model.detail = row["detail"].ToString();
            //咨询者数据
            specialConsult_model.user.userId = (int)row["userId"];
            specialConsult_model.user.userName = row["userName"].ToString();
            specialConsult_model.user.height = Convert.ToInt32(row["height"]);
            specialConsult_model.user.weight = Convert.ToInt32(row["weight"]);
            specialConsult_model.user.yaoWei = Convert.ToInt32(row["yaoWei"]);
            specialConsult_model.user.tuiChang = Convert.ToInt32(row["tuiChang"]);
            specialConsult_model.user.daTuiWei = Convert.ToInt32(row["daTuiWei"]);
            specialConsult_model.user.xiaoTunWei = Convert.ToInt32(row["xiaoTuiWei"]);
            specialConsult_model.user.tunWei = Convert.ToInt32(row["tunWei"]);
            specialConsult_model.user.biWei = Convert.ToInt32(row["biWei"]);
            specialConsult_model.user.xiongWei = Convert.ToInt32(row["xiongWei"]);
            specialConsult_model.user.skinColor = row["skinColor"].ToString();
            //专家数据
            specialConsult_model.expert.userId = (int)row["expertId"];            
            return specialConsult_model;
        }

        /// <summary>
        /// 用户特定咨询，保存用户特定咨询填写的数据
        /// </summary>
        /// <param name="userId">咨询者的id</param>
        /// <param name="expertId">专家id</param>
        /// <param name="occasion">场合</param>
        /// <param name="details">咨询详情</param>
        /// <param name="geRenZhaoUrl">个人照片url</param>
        /// <param name="likeStyleImageUrl">喜欢风格的照片url</param>
        /// <param name="dislikeStyleImageUrl">不喜欢风格的照片url</param>
        /// <returns></returns>
        public int InsertConsultData(int userId, int expertId, string occasion, string details, string geRenZhaoUrl, string likeStyleImageUrl, string dislikeStyleImageUrl)
        {
            string sqlStr = @"insert into tb_SpecialConsult 
                                                             ( SpecialConsult_UserId, SpecialConsult_UserPhotoUrl,
                                                               SpecialConsult_Occasion, SpecialConsult_LikeStyleUrl, 
                                                               SpecialConsult_DislikeStyleUrl, SpecialConsult_Detail, SpecialConsult_ExpertId)
                                                   values(@userId,@geRenZhaoUrl,
                                                               @occasion,@likeStyleImageUrl,
   	                                                           @dislikeStyleImageUrl,@details,@expertId)";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@userId",userId),
                new SqlParameter("@expertId",expertId),
                new SqlParameter("@occasion",occasion),
                new SqlParameter("@details",details),
                new SqlParameter("@geRenZhaoUrl",geRenZhaoUrl),
                new SqlParameter("@likeStyleImageUrl",likeStyleImageUrl),
                new SqlParameter("@dislikeStyleImageUrl",dislikeStyleImageUrl),
            };
            return SqlHelper.ExecuteNonquery(sqlStr, parameters);

        }

       /// <summary>
        /// 将专家对特定咨询的回答存入数据库tb_SpecialConsultAnswer
       /// </summary>
       /// <param name="specialConsultId">用户特定咨询数据表里的id</param>
       /// <param name="htmlUrl">专家回答的静态htmlurl</param>
       /// <returns></returns>
        public int InsertAnswerData(int specialConsultId, string htmlUrl,DateTime datetime)
        {
            string sqlStr = @"insert tb_SpecialConsultAnswer
                                                      ( SpecialConsultAnswer_SpecialConsultId, SpecialConsultAnswer_HtmlUrl,SpecialConsultAnswer_Date)  
                                            values(@specialConsultId,@htmlUrl,@datetime)";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@specialConsultId",specialConsultId),
                new SqlParameter("@htmlUrl",htmlUrl),
                new SqlParameter("@datetime",datetime),
            };
            return SqlHelper.ExecuteNonquery(sqlStr, parameters);
            
        }

    }
}









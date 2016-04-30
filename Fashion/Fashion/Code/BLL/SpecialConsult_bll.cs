using Fashion.Code.DAL;
using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Code.BLL
{
    public class SpecialConsult_bll
    {
        /// <summary>
        /// 用户特定咨询，保存用户特定咨询填写的数据
        /// </summary>
        /// <param name="userId">咨询者的id</param>
        /// <param name="expertId">专家id</param>
        /// <param name="occasion">场合</param>
        /// <param name="details">咨询详情</param>
        /// <param name="geRenZhaoFileName">个人照片名</param>
        /// <param name="likeStyleImageFileName">喜欢风格的照片名</param>
        /// <param name="dislikeStyleImageFileName">不喜欢风格的照片名</param>
        /// <returns></returns>

        public int InsertConsultData(int userId, int expertId, string occasion, string details, string geRenZhaoFileName, string likeStyleImageFileName, string dislikeStyleImageFileName)
        {
            SpecialConsult_dal specialConsul_dal = new SpecialConsult_dal();
            string geRenZhaoUrl="/Images/ConsultImages/GeRenZhao/"+ geRenZhaoFileName;
            string likeStyleImageUrl="/Images/ConsultImages/LikeStyleImage/"+ likeStyleImageFileName;
            string dislikeStyleImageUrl = "/Images/ConsultImages/DisLikeStyleImage/" + dislikeStyleImageFileName;
            return specialConsul_dal.InsertConsultData(userId, expertId, occasion, details, geRenZhaoUrl, likeStyleImageUrl, dislikeStyleImageUrl);
        }


        /// <summary>
        /// 通过特定咨询的帖子编号specialConsultId查询数据库的tb_SpecialConsult，获取特定咨询的
        /// 用户咨询数据
        /// </summary>
        /// <param name="specialConsultId">特定咨询的帖子编号</param>
        /// </summary>
        /// <param name="specialConsultId"></param>
        /// <returns></returns>
        public SpecialConsult_model GetOneSpecialConsult(int specialConsultId)
        {
            SpecialConsult_dal specialConsult_dal = new SpecialConsult_dal();
            return specialConsult_dal.GetOneConsultData(specialConsultId);
        }
        /// <summary>
        /// 专家对特定咨询的解答
        /// 将专家对特定咨询的回答存入数据库tb_SpecialConsultAnswer，1条数据
        /// 成功返回1，失败返回0
        /// </summary>
        /// <param name="specialConsultId">用户特定咨询数据表里的id</param>
        /// <param name="htmlUrl">专家回答的静态htmlurl</param>
        /// <returns></returns>
        public int InsertAnswerData(int specialConsultId, string htmlUrl,DateTime datetime)
        {
            SpecialConsult_dal specialConsult_dal = new SpecialConsult_dal();

            if (specialConsult_dal.InsertAnswerData(specialConsultId, htmlUrl, datetime) != 1)
            {
                return 0;
            }
            return 1;
        }
        /// <summary>
        /// 通过专家解答的特定咨询staticConsultAnswerHtml查询该条数据的id
        /// </summary>
        /// <param name="staticConsultAnswerHtml"></param>
        /// <returns></returns>
        public int GetConsultAnswerId(string staticConsultAnswerHtml)
        {
            SpecialConsult_dal specialConsult_dal = new SpecialConsult_dal();
            object objSpecialConsultAnswer_Id = specialConsult_dal.GetConsultAnswerId(staticConsultAnswerHtml);
            if (objSpecialConsultAnswer_Id == null || objSpecialConsultAnswer_Id == System.DBNull.Value)
            { 

            }
            return Convert.ToInt32(objSpecialConsultAnswer_Id);
        }

    }
}
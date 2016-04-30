using Fashion.Code.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Code.BLL
{
    public class SpecialConsultAnswerClothes_bll
    {
        /// <summary>
        /// 将专家对特定咨询的接到的衣服购买链接存储到数据库
        /// </summary>
        /// <param name="specialConsultAnswer_Id">专家的解答的数据表的id</param>
        /// <param name="selectClothUrlDic">衣服购买链接的键值对</param>
        /// <returns></returns>
        public int InsertConsultAnswerClothes(int specialConsultAnswer_Id, Dictionary<string, string> selectClothUrlDic)
        {
            SpecialConsultAnswerClothes_dal specialConsultAnswerClothes_dal = new SpecialConsultAnswerClothes_dal();
            return specialConsultAnswerClothes_dal.InsertConsultAnswerClothes(specialConsultAnswer_Id, selectClothUrlDic);
        }

    }
}
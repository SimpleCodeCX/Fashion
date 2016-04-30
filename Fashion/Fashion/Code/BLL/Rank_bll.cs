using Fashion.Code.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Code.BLL
{
    public class Rank_bll
    {
        /// <summary>
        /// 通过用户名，获得用户角色等级等级名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public string GetRankName(string userName)
        {
            Rank_dal rank_dal = new Rank_dal();
            object objRankName=rank_dal.GetRankName(userName);
            if (objRankName == null || objRankName == System.DBNull.Value)
            { 
                //throw new ("通过用户名查询数据库里的等级名rankName出错");
                return "出错";
            }
            string rankName=objRankName.ToString();
            return rankName;
        }
    }
}
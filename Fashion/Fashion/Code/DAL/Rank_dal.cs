using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fashion.Code.DAL
{
    public class Rank_dal
    {
        /// <summary>
        /// 通过等级名，获得用户角色等级Id
        /// </summary>
        /// <param name="rankName">用户等级名</param>
        /// <returns></returns>
        public object GetRankId(string rankName)
        {
            string sqlStr = "select Rank_Id from [tb_Rank] where Rank_Name=@rankName";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@rankName",rankName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }
        
        /// <summary>
        /// 通过等级Id，获得用户角色等级等级名
        /// </summary>
        /// <param name="rankId"></param>
        /// <returns></returns>
        public object GetRankName(char rankId)
        {
            string sqlStr = "select Rank_Name from [tb_Rank] where Rank_Id=@rankId";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@rankId",rankId)
            };
            return SqlHelper.ExecuteScalar(sqlStr,parameters);
        }


        /// <summary>
        /// 重载函数
        /// 通过用户名，获得用户角色等级等级名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public object GetRankName(string userName)
        {
            string sqlStr = "select Rank_Name from tb_User left join tb_Rank on tb_User.User_RankId=tb_Rank.Rank_Id where tb_User.User_Name=@userName";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@userName",userName)
            };
            return SqlHelper.ExecuteScalar(sqlStr, parameters);
        }
        



    }
}
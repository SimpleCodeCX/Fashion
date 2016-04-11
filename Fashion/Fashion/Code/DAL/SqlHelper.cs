using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fashion.Code.DAL
{
    public static class SqlHelper
    {
        //数据库连接语句
        private static string sqlConnectionString=ConfigurationManager.ConnectionStrings["conStr3"].ConnectionString;

        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="sqlStr">sqlStr代表数据库操作语句</param>
        /// <param name="parameters">parameters代表数据库操作语句里的参数</param>
        /// <returns></returns>
        public static int ExecuteNonquery(string sqlStr, params SqlParameter[] parameters)
        {
            //params实现动态参数个数（也可以为0）
            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using(SqlCommand cmd=new SqlCommand(sqlStr,conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        //返回查询后的第一行的第一列
        public static object ExecuteScalar(string sqlStr,params SqlParameter[] parameters)
        { 
            using(SqlConnection conn=new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                   
                    return cmd.ExecuteScalar();
                }
            }
        }

        //返回一个数据表
        public static DataTable ExecuteDataTable(string sqlStr,params SqlParameter[] parameters)
        { 
            using(SqlConnection conn=new SqlConnection(sqlConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        public static object ToDBValue(this object value)
        {
            return value == null ? DBNull.Value : value;
        }

        public static object FromDBValue(this object dbValue)
        {
            return dbValue == DBNull.Value ? null : dbValue;
        }


    }
}
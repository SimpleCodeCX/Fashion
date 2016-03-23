using Fashion.Code.BLL;
using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fashion.Code.DAL
{
    public class Theme_dal
    {
        public Theme_model GetThemeIdModel(string themeName)
        {
            string sqlStr = "select [Theme_Id],[Theme_Name] from [tb_Theme] where Theme_Name= @themeName";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@themeName",themeName)
            };
            DataTable dt = SqlHelper.ExecuteDataTable(sqlStr, parameters);
            if (dt.Rows.Count > 1)
            {
                //数据库出错处理，数据库里存在大于两条标题名一样的数据
                ErrorMessage_bll errorMessage_bll = new ErrorMessage_bll();
                errorMessage_bll.InsertErrorMessage("数据库出错", "数据库存在2条标题名一样的数据，用户名为：" + themeName);
                throw new Exception("more than 1 row was found");
            }
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            //把取回来的dt_Theme表的一行数据转化为model
            Theme_model model = new Theme_model();
            model.themeName = (string)row["Theme_Name"];
            model.themeId = (int)row["Theme_Id"];
            return model;
        }
    }
}
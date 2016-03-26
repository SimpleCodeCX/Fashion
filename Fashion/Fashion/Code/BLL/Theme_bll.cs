using Fashion.Code.DAL;
using Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion.Code.BLL
{
    public class Theme_bll
    {
        /// <summary>
        /// 通过匹配themeName得到themeId
        /// </summary>
        /// <param name="themeName"></param>
        /// <returns></returns>
        public int CollocateThemeId(string themeName)
        {
            Theme_dal theme_dal = new Theme_dal();
            Theme_model theme_model = theme_dal.GetThemeIdModel(themeName);
            int themeId = (int)theme_model.themeId;
            return themeId;
        }
    }
}
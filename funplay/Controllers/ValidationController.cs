using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace funplay.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class ValidationController : Controller
    {
        /// <summary>
        /// 檢查使用者編號欄位唯一值
        /// </summary>
        /// <param name="UserNo">編號欄位值, 例: U001</param>
        /// <returns></returns>
        public JsonResult IsUserNoUnique(string UserNo)
        {
            using (z_repoUsers repos = new z_repoUsers())
            {
                if (!repos.NoExists(SessionService.KeyValue, UserNo)) return Json(true, JsonRequestBehavior.AllowGet);
                string str_message = $"{UserNo} 重覆輸入!!";
                return Json(str_message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    public class GetDataListController : BaseController
    {
        /// <summary>
        /// 取得縣市區域 Json 格式列表
        /// </summary>
        /// <param name="id">縣市名稱</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCityAreaList(string id)
        {
            using (ListItemData listDta = new ListItemData())
            {
                var model = listDta.CityAreaList(id);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
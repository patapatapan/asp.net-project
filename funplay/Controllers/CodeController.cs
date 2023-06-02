using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    public class CodeController : BaseController
    {
        [HttpPost]
        public JsonResult GetPropertys(string id)
        {
            using (CodeBase codeBase = new CodeBase())
            {
                List<dmColumnProperty> model = new List<dmColumnProperty>();
                model = codeBase.GetClassPropertyList(id);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public string GetGuidList()
        {
            string str_value = "";
            for (int i = 0; i < 50; i++)
            {
                str_value += Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                str_value += "</br>";
            }
            return str_value;
        }
    }
}
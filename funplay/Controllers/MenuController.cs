using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    public class MenuController : Controller
    {
        [LoginAuthorize()]
        public ActionResult Index(string id)
        {
            using (CodeBase code = new CodeBase())
            {
                using (z_repoPrograms prg = new z_repoPrograms())
                {
                    var model = prg.GetData(UserService.RoleNo, id);
                    if (model == null) return RedirectToAction("Login", "Web", new { area = "" });
                    PrgService.ModuleNo = model.ModuleNo;
                    PrgService.PrgNo = model.PrgNo;
                    PrgService.PrgName = model.PrgName;
                    PrgService.SetProgramSecurity();
                    SessionService.Init();
                    bool bln_exists = code.ViewFileExists(model.AreaName, model.ControllerName, model.ActionName);
                    if (!bln_exists)
                    {
                        TempData["ErrorMessage"] = "程式尚未架構完成!!";
                        return RedirectToAction(ActionService.Index, ActionService.Home, new { area = model.AreaName });
                    }
                    return RedirectToAction(model.ActionName, model.ControllerName, new { area = model.AreaName });
                }
            }
        }
    }
}
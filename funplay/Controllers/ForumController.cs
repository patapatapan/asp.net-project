using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    public class ForumController : BaseController
    {
        [HttpGet]
        [LoginAuthorize(RoleList = "User,Mis")]
        public ActionResult Index()
        {
            using (z_repoForumBoards repos = new z_repoForumBoards())
            {
                PrgService.SetAction(enAction.Index, enCardSize.Max);
                PrgService.SetProgramNoName("Forum", "討論區版塊");
                PrgService.SubHeader = "";
                var model = repos.GetDapperDataList("");
                return View(model);
            }
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "User,Mis")]
        public ActionResult Board(string id)
        {
            using (z_repoForumBoards repos = new z_repoForumBoards())
            {
                var data = repos.GetDataName(id);
                SessionService.TagNo1 = id;
                SessionService.TagName1 = data;
                return RedirectToAction("Index", "ForumBoard", new { area = "" });
            }
        }
    }
}
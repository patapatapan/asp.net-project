using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace funplay.Controllers
{
    public class HtmlEditorController : BaseController
    {
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Index()
        {
            PrgService.SetAction(enAction.Edit, enCardSize.Max);
            vmHtmlEditor model = new vmHtmlEditor()
            {
                Id = ActionService.PriorKeyValue,
                HtmlText = ActionService.PriorTextValue
            };
            return View(model);
        }

        [HttpPost]
        [LoginAuthorize()]
        [ValidateInput(false)]
        public ActionResult Index(vmHtmlEditor model)
        {
            using (repoHtmlEditor repos = new repoHtmlEditor())
            {
                ActionService.PriorTextValue = model.HtmlText;
                repos.UpdateHtmlText();
                return RedirectToAction("ReturnPriorAction", "HtmlEditor", new { area = "" });
            }
        }

        [HttpGet]
        [LoginAuthorize()]
        public ActionResult Cancel()
        {
            return RedirectToAction("ReturnPriorAction", "HtmlEditor", new { area = "" });
        }

        [HttpGet]
        [LoginAuthorize()]
        public ActionResult ReturnPriorAction()
        {
            if (ActionService.PriorParmIdType == enPriorParmIdType.None)
                return RedirectToAction(ActionService.PriorAction, ActionService.PriorController, new { area = ActionService.PriorArea });
            if (ActionService.PriorParmIdType == enPriorParmIdType.TypeInt)
            {
                int int_value = (int)ActionService.PriorParmIdValue;
                return RedirectToAction(ActionService.PriorAction, ActionService.PriorController, new { area = ActionService.PriorArea, id = int_value });
            }
            else
            {
                string str_value = ActionService.PriorParmIdValue.ToString();
                return RedirectToAction(ActionService.PriorAction, ActionService.PriorController, new { area = ActionService.PriorArea, id = str_value });
            }
        }
    }
}
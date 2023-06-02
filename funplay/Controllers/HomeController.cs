using DocumentFormat.OpenXml.Bibliography;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.SectionName = "關於公司";
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            vmContact model = new vmContact();
            ViewBag.SectionName = "連絡我們";
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Contact(vmContact model)
        {
            if (!ModelState.IsValid) return View(model);
            using (SendMailService mail = new SendMailService())
            {
                mail.ContactUs(model);
                TempData["ErrorMessage"] = "您的訊息已送出!!";
                return RedirectToAction("Contact", "Home", new { area = "" });
            }
        }

        [HttpGet]
        public ActionResult Client()
        {
            ViewBag.SectionName = "公司客戶";
            return View();
        }

        [HttpGet]
        public ActionResult Photo()
        {
            ViewBag.SectionName = "商品分類";
            return View();
        }

        [HttpGet]
        public ActionResult PhotoDetail(string id)
        {
            using (z_repoPhotos photos = new z_repoPhotos())
            {
                var model = photos.GetDapperFoderNameData(id);
                if (model == null)
                {
                    TempData["ErrorMessage"] = "無明細資料!!";
                    return RedirectToAction("Photo", "Home", new { area = "" });
                }
                ViewBag.SectionName = "商品資訊明細";
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Service()
        {
            ViewBag.SectionName = "服務項目";
            return View();
        }

        [HttpGet]
        public ActionResult Team()
        {
            using (z_repoTeams team = new z_repoTeams())
            {
                ViewBag.SectionName = "團隊介紹";
                var model = team.repo.ReadAll()
                    .OrderBy(m => m.SortNo).ToList();
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Pricing()
        {
            using (z_repoPricings price = new z_repoPricings())
            {
                ViewBag.SectionName = "商品價格";
                var model = price.repo.ReadAll()
                    .OrderBy(m => m.SortNo).ToList();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Subscription(FormCollection collection)
        {
            using (SendMailService sendMail = new SendMailService())
            {
                object obj_email = collection["email"];
                if (obj_email != null)
                {
                    string str_email = obj_email.ToString();
                    if (!string.IsNullOrEmpty(str_email))
                    {
                        string str_message = sendMail.Subscription(str_email, true);
                        TempData["ErrorMessage"] = (string.IsNullOrEmpty(str_message)) ? "您的訂閱訊息已送出!!" : str_message;
                    }
                }
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult UnSubscription(FormCollection collection)
        {
            using (SendMailService sendMail = new SendMailService())
            {
                object obj_email = collection["email"];
                if (obj_email != null)
                {
                    string str_email = obj_email.ToString();
                    if (!string.IsNullOrEmpty(str_email))
                    {
                        string str_message = sendMail.Subscription(str_email, false);
                        TempData["ErrorMessage"] = (string.IsNullOrEmpty(str_message)) ? "您的取消訂閱訊息已送出!!" : str_message;
                    }
                }
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
    }
}
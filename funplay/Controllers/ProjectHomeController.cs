using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.EMMA;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using Dapper;
using System.Web.UI.WebControls;
using System.Data.Common;

namespace funplay.Controllers
{
    /// <summary>
    /// ProjectController
    /// </summary>
    public class ProjectHomeController : BaseController
    {
        [HttpGet]
        [LoginAuthorize()]
        public ActionResult ProjectIndex()
        {
            //using (DapperRepository ImageFirst = new DapperRepository())
            //{
            //    var query = SELECT[dbo].[Games].* ,[dbo].[Images].ImageName FROM[dbo].[Games] left join[dbo].[Images] on[dbo].[Games].GameNo = [dbo].[Images].GameNo;
            //}
            //string str_conn = WebConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            //using (var cn = new SqlConnection(str_conn))
            //{
            //    cn.Open();
            //    List<Games> ImageName = cn.Query<Games>("SELECT *\r\nFROM (\r\n    SELECT [dbo].[Games].* ,[dbo].[Images].ImageName,\r\n        ROW_NUMBER() OVER (PARTITION BY [dbo].[Games].GameNo ORDER BY [dbo].[Images].ImageNo) AS RowNum\r\n    FROM [dbo].[Games]\r\n    LEFT JOIN [dbo].[Images] ON [dbo].[Games].GameNo = [dbo].[Images].GameNo\r\n) AS Subquery\r\nWHERE RowNum = 1\r\nORDER BY GameNo;").ToList();
            //    //Console.WriteLine($"筆數{ImageName.Count()}");
            //}
            using (dbEntities db = new dbEntities())
            {

                var model = db.Games.OrderBy(m => m.GameNo).ToList();

                return View(model);
            }
        }

        [HttpGet]
        [LoginAuthorize()]
        public ActionResult ProjectLogin()
        {
            vmProjectLogin model = new vmProjectLogin();
            return View(model);
        }

        /*登入驗證*/
        [HttpPost]
        [LoginAuthorize()]
        public ActionResult ProjectLogin(vmProjectLogin model)
        {
            if (!ModelState.IsValid) return View(model);
            using (z_repoUsers repos = new z_repoUsers())
            {
                //bool bln_value = repos.ProjectLogin(model.Account, model.Password);
                int state = repos.ProjectLogin(model.Account, model.Password);
                //if (!bln_value)
                //{
                //    ModelState.AddModelError("Account", "帳號或密碼輸入錯誤!!");
                //    return View(model);
                //}
                switch (state)
                {
                    case 0:
                        //if (!AppService.IsConfig) AppService.Init();
                        return RedirectToAction("ProjectIndex", "ProjectHome", new { area = "" });
                        break;
                    case -1:
                        ModelState.AddModelError("Account", "帳號或密碼輸入錯誤!!");
                        return View(model);
                        break;
                    case -2:
                        ModelState.AddModelError("Account", "帳號驗證未通過，請去信箱接收驗證信!!");
                        return View(model);
                        break;
                    default:
                        ModelState.AddModelError("Account", "不明錯誤程序，請回報開發人員!!");
                        return View(model);
                        break;
                }
            }
        }

        [HttpGet]
        [LoginAuthorize()]
        public ActionResult ProjectRegister()
        {
            vmProjectRegister model = new vmProjectRegister();
            return View(model);
        }

        [HttpPost]
        [LoginAuthorize()]
        public ActionResult ProjectRegister(vmProjectRegister model)
        //public ActionResult ProjectRegister(vmProjectRegister model)
        {
            //vmProjectRegister model = new vmProjectRegister();
            //model.UserName = collection["UserName"].ToString();
            ////model.Birthday = DateTime.Parse(collection["Birthday"]);
            //model.Tel = collection["Tel"].ToString();
            //model.Email = collection["Email"].ToString();
            //model.Account = collection["Account"].ToString();
            //model.Password = collection["Password"].ToString();
            //model.ConfirmPassword = collection["ConfirmPassword"].ToString();

            //if (model.Password != model.ConfirmPassword)
            //{
            //    ModelState.AddModelError("ConfirmPassword", "密碼與確認密碼不一致");
            //    return View(model);
            //}

            //1. 沒有通過驗證，返回登入頁繼續輸入
            if (!ModelState.IsValid) return View(model);


            //2. 判斷登入資訊是否正確，不正確時手動引發一個錯誤
            string str_message = UserService.ProjectRegister(model);
            if (!string.IsNullOrEmpty(str_message))
            {
                if (str_message == "登入信箱重複註冊!!")
                {
                    ModelState.AddModelError("Email", str_message);
                    return View(model);
                }

                if (str_message == "登入帳號重複註冊!!")
                {
                    ModelState.AddModelError("Account", str_message);
                    return View(model);
                }    
            }
            //3.新增一筆未審核會員資訊
            string str_code = UserService.ProjectRegisterCreate(model);
            //4.寄出一封註冊驗證信件
            using (SendMailService sendEmail = new SendMailService())
            {
                sendEmail.UserRegister(str_code);
            }
            //5.顯示註冊訊息
            TempData["MessageText"] = $"您的註冊資訊已提交，請至您的電子信箱{model.Email}中驗證電子信箱功能,謝謝!!";
            return RedirectToAction("Message", "ProjectHome", new { area = "" });
        }

        [HttpGet]
        [LoginAuthorize()]
        public ActionResult ProjectProduct()
        {
            return View();
        }

        //顯示訊息用
        public ActionResult Message()
        {
            return View();
        }

        //註冊信箱驗證
        public ActionResult ValidateEmail(string id)
        {
            using (z_repoUsers users = new z_repoUsers())
            {
                var userData = users.repo.ReadSingle(m => m.ValidateCode == id);

                string str_message = "";
                if (!users.ValidateEmail(id, ref str_message))
                    TempData["Message"] = str_message;
                else
                {
                    //記錄會員註冊驗證成功時間
                    using (z_repoLogs logs = new z_repoLogs())
                    {
                        logs.EventLogCount(enLogType.EmailSend, userData.UserNo, id);
                    }
                    TempData["MessageText"] = "電子郵件已驗證成功，您可以進入登入頁登入系統!!";
                }
                //顯示訊息畫面
                return RedirectToAction("Message", "ProjectHome", new { area = "" });
            }
        }

    }


}
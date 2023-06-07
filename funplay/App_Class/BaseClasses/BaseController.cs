using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

/// <summary>
/// 控制器底層
/// </summary>
public class BaseController : Controller
{
    protected override void ExecuteCore()
    {
        if (this.Session == null || this.Session["LanguageNo"] == null)
        {
            object obj_culture = WebConfigurationManager.AppSettings["LanguageNo"];
            Session["LanguageNo"] = (obj_culture == null) ? "zh-TW" : obj_culture.ToString();
        }
        string str_culture = Session["LanguageNo"].ToString();
        CultureInfo culture = new CultureInfo(str_culture);
        Thread.CurrentThread.CurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
        if (!AppService.IsConfig) AppService.Init();
        //if (AppService.DebugMode && !UserService.IsLogin)
        //{
        //    UserService.RoleNo = "User";
        //    if (!string.IsNullOrEmpty(ActionService.Area))
        //        UserService.RoleNo = ActionService.Area;
        //    UserService.DemoUser();
        //}
        base.ExecuteCore();
    }

    protected override bool DisableAsyncSupport
    {
        get { return true; }
    }
}
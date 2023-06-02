using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

/// <summary>
/// 前台自定義權限
/// </summary>
public class LoginAuthorizeAttribute : AuthorizeAttribute
{
    public string RoleList { get; set; } = "";
    /// <summary>
    /// 自定義權限 Filter 建構子
    /// </summary>
    public LoginAuthorizeAttribute()
    {
        RoleList = "";
    }
    public LoginAuthorizeAttribute(string roleList)
    {
        RoleList = roleList;
    }
    /// <summary>
    /// 覆寫 Authorize 設定
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <returns>驗證結果</returns>
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        //除錯模式
        if (AppService.DebugMode) return true;

        //未登入即不通過驗證 
        if (!UserService.IsLogin) return false;

        //未限制角色不檢查權限
        if (string.IsNullOrEmpty(RoleList)) return true;

        //檢查登入者角色是否包含在限制的角色中
        List<string> roleLists = RoleList.Split(',').ToList();
        string str_user_role = UserService.RoleNo.Trim().ToUpper();
        foreach (string roleName in roleLists)
        {
            string str_role = roleName.Trim().ToUpper();
            if (str_role == str_user_role) return true;
        }
        return false;
    }

    /// <summary>
    /// 驗證不通過時返回登入頁面
    /// </summary>
    /// <param name="filterContext"></param>
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        //返回登入頁 (User/Login)
        filterContext.Result = new RedirectToRouteResult
        (
            new RouteValueDictionary
            (
                new
                {
                    area = "",
                    controller = "Web",
                    action = "Login"
                }
            )
        );
    }
}

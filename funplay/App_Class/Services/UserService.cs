using funplay.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;

/// <summary>
/// 使用者相關服務
/// </summary>
public static class UserService
{
    public static string UserNo { get; set; } = "";
    public static string UserName { get; set; } = "";
    public static string RoleNo { get; set; } = "";
    public static string RoleName { get { using (z_repoRoles roles = new z_repoRoles()) { return roles.GetDataName(RoleNo); } } }
    public static string DeptName { get; set; } = "";
    public static string TitleName { get; set; } = "";
    public static string UserImage { get { return GetUserImage(UserNo); } }
    public static bool IsLogin { get; set; } = false;
    public static Securitys UserSecurity { get; set; } = new Securitys
    {
        TargetNo = "",
        RoleNo = "",
        IsAdd = false,
        IsConfirm = false,
        IsDelete = false,
        IsDownload = false,
        IsEdit = false,
        IsPrint = false,
        IsUndo = false,
        IsUpload = false
    };
    public static string GetUserImage(string userNo)
    {
        string str_image = string.Format("~/Images/User/{0}.jpg", userNo);
        string str_stamp = DateTime.Now.ToString("yyyyMMddHHmmssff");
        if (!File.Exists(HttpContext.Current.Server.MapPath(str_image)))
            str_image = "~/Images/User/none.jpg";
        return string.Format("{0}?t={1}", str_image, str_stamp);
    }
    /// <summary>
    /// 登入
    /// </summary>
    /// <param name="userNo">使用者代號</param>
    /// <param name="userName">使用者姓名</param>
    /// <param name="roleNo">角色代號</param>
    public static void Login(string userNo, string userName, string roleNo)
    {
        UserNo = userNo;
        UserName = userName;
        RoleNo = roleNo;
        DeptName = "";
        TitleName = "";
        IsLogin = true;
        if (roleNo == "User")
        { using (z_repoUsers user = new z_repoUsers()) { user.SetUserInfo(); } }
        { using (z_repoCompanys user = new z_repoCompanys()) { user.SetDefaultCompany(); } }
    }
    /// <summary>
    /// 登出
    /// </summary>
    public static void Logout()
    {
        UserNo = "";
        UserName = "";
        RoleNo = "";
        DeptName = "";
        TitleName = "";
        IsLogin = false;
    }
    /// <summary>
    /// 除錯模式預設使用者
    /// </summary>
    public static void DemoUser()
    {
        UserNo = "demo";
        UserName = "測試帳號";
        DeptName = "資訊部";
        TitleName = "程式設計師";
        IsLogin = true;
        DemoSecurity();
    }
    /// <summary>
    /// 權限初始化
    /// </summary>
    public static void InitSecurity()
    {
        UserSecurity.Id = -1;
        UserSecurity.TargetNo = "";
        UserSecurity.RoleNo = "";
        UserSecurity.IsAdd = false;
        UserSecurity.IsConfirm = false;
        UserSecurity.IsDelete = false;
        UserSecurity.IsDownload = false;
        UserSecurity.IsEdit = false;
        UserSecurity.IsInvalid = false;
        UserSecurity.IsPrint = false;
        UserSecurity.IsUndo = false;
        UserSecurity.IsUpload = false;
    }
    /// <summary>
    /// 除錯模式預設使用者權限
    /// </summary>
    public static void DemoSecurity()
    {
        UserSecurity.Id = -1;
        UserSecurity.TargetNo = UserNo;
        UserSecurity.RoleNo = RoleNo;
        UserSecurity.IsAdd = true;
        UserSecurity.IsConfirm = true;
        UserSecurity.IsDelete = true;
        UserSecurity.IsDownload = true;
        UserSecurity.IsEdit = true;
        UserSecurity.IsInvalid = true;
        UserSecurity.IsPrint = true;
        UserSecurity.IsUndo = true;
        UserSecurity.IsUpload = true;
    }
}
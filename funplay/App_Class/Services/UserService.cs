using DocumentFormat.OpenXml.EMMA;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

/// <summary>
/// 使用者相關服務
/// </summary>
public static class UserService
{
    public static string UserNo { get { return SessionService.GetValue("UserNo"); } set { SessionService.SetValue("UserNo", value); } }
    public static string UserName { get { return SessionService.GetValue("UserName"); } set { SessionService.SetValue("UserName", value); } }
    public static string RoleNo { get { return SessionService.GetValue("RoleNo"); } set { SessionService.SetValue("RoleNo", value); } }
    public static string RoleName { get { using (z_repoRoles roles = new z_repoRoles()) { return roles.GetDataName(RoleNo); } } }
    public static string DeptName { get { return SessionService.GetValue("DeptName"); } set { SessionService.SetValue("DeptName", value); } }
    public static string TitleName { get { return SessionService.GetValue("TitleName"); } set { SessionService.SetValue("TitleName", value); } }
    public static string Account { get { return SessionService.GetValue("Account"); } set { SessionService.SetValue("Account", value); } }

    public static DateTime Birthday { get; set; }
    //把存取的值轉換成 Session 讓暫存的值的生命周期較長
    public static string UserImage { get { return GetUserImage(UserNo); } }
    public static bool IsLogin { get { return SessionService.GetBoolValue("IsLogin"); } set { SessionService.SetValue("IsLogin", value); } }
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

    //檢測是否重複
    public static string ProjectRegister(vmProjectRegister model)
    {
        using (z_repoUsers user = new z_repoUsers())
        {
            var data = user.repo.ReadSingle(m => m.Account == model.Account);
            if (data != null) return "登入帳號重複註冊!!";
            data = user.repo.ReadSingle(m => m.Email == model.Email);
            if (data != null) return "登入信箱重複註冊!!";
            return "";
        }
    }

    //將表單輸入的值傳送至資料庫
    public static string ProjectRegisterCreate(vmProjectRegister model)
    {
        using (z_repoUsers user = new z_repoUsers())
        {
            using (CryptographyService cryp = new CryptographyService())
            {
                string str_code = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //string str_password = cryp.SHA256Encode(model.Password);
                Users newUser = new Users();
                //資料庫的值 = 從 vmProjectRegister 取得的值
                newUser.IsValid = false;
                newUser.RoleNo = "Member";
                newUser.ValidateCode = str_code;
                newUser.Account = model.Account;
                newUser.UserName = model.UserName;
                newUser.Password = model.Password;
                //newUser.Birthday = model.Birthday;
                newUser.Email = model.Email;
                newUser.Tel = model.Tel;
                user.repo.Create(newUser);
                user.repo.SaveChanges();
                return str_code;
            }
        }
    }

    /// <summary>
    /// 登入
    /// </summary>
    /// <param name="userNo">使用者代號</param>
    /// <param name="userName">使用者姓名</param>
    /// <param name="roleNo">角色代號</param>
    //public static void Login(string userNo, string userName, string roleNo)
    //{
    //    UserNo = userNo;
    //    UserName = userName;
    //    RoleNo = roleNo;
    //    DeptName = "";
    //    TitleName = "";
    //    IsLogin = true;
    //    if (roleNo == "User")
    //    { using (z_repoUsers user = new z_repoUsers()) { user.SetUserInfo(); } }
    //    { using (z_repoCompanys user = new z_repoCompanys()) { user.SetDefaultCompany(); } }
    //}
    public static void Login(string account, string userName, string roleNo)
    {
        Account = account;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class AppService
{
    public static bool IsConfig
    {
        get { return SessionService.GetBoolValue("IsConfig", false); }
        set { SessionService.SetValue("IsConfig", value); }
    }
    public static string CompanyNo
    {
        get { return SessionService.GetValue("CompanyNo", "001"); }
        set { SessionService.SetValue("CompanyNo", value); }
    }
    public static string CompanyName
    {
        get { return SessionService.GetValue("CompanyName", "好好用科技股份有限公司"); }
        set { SessionService.SetValue("CompanyName", value); }
    }
    public static string CompanyShortName
    {
        get { return SessionService.GetValue("CompanyShortName", "好好用科技"); }
        set { SessionService.SetValue("CompanyShortName", value); }
    }
    public static string EnglishName
    {
        get { return SessionService.GetValue("EnglishName", "Good Use Technology Co., Ltd."); }
        set { SessionService.SetValue("EnglishName", value); }
    }
    public static string EnglishShortName
    {
        get { return SessionService.GetValue("EnglishShortName", "Good Use Technology"); }
        set { SessionService.SetValue("EnglishShortName", value); }
    }
    public static string AppName
    {
        get { if (!IsConfig) Init(); return SessionService.GetValue("AppName", "好好用ERP"); }
        set { SessionService.SetValue("AppName", value); }
    }
    public static string AppVersion
    {
        get { return SessionService.GetValue("AppVersion", "1.0"); }
        set { SessionService.SetValue("AppVersion", value); }
    }
    public static bool EncryptionMode
    {
        get { return SessionService.GetBoolValue("EncryptionMode", false); }
        set { SessionService.SetValue("EncryptionMode", value); }
    }
    public static string PowerBy
    {
        get { return SessionService.GetValue("PowerBy", "DevStudio"); }
        set { SessionService.SetValue("PowerBy", value); }
    }
    public static string LanguageNo
    {
        get { return SessionService.GetValue("LanguageNo", "zh-TW"); }
        set { SessionService.SetValue("LanguageNo", value); }
    }
    public static string WebSiteUrl
    {
        get { return SessionService.GetValue("WebSiteUrl", "http://localhost:8888"); }
        set { SessionService.SetValue("WebSiteUrl", value); }
    }
    public static string MailSenderName
    {
        get { return SessionService.GetValue("WebSiteUrl", "網站管理員"); }
        set { SessionService.SetValue("WebSiteUrl", value); }
    }
    public static string MailSenderEmail
    {
        get { return SessionService.GetValue("MailSenderEmail", "xxxxxxxx@gmail.com"); }
        set { SessionService.SetValue("MailSenderEmail", value); }
    }
    public static string MailAppPassword
    {
        get { return SessionService.GetValue("MailAppPassword", "xxxxxxxx"); }
        set { SessionService.SetValue("MailAppPassword", value); }
    }
    public static string MailHostUrl
    {
        get { return SessionService.GetValue("MailHostUrl", "smtp.gmail.com"); }
        set { SessionService.SetValue("MailHostUrl", value); }
    }
    public static int MailHostPort
    {
        get { return SessionService.GetIntValue("MailHostPort", 587); }
        set { SessionService.SetValue("MailHostPort", value); }
    }
    public static bool MailUseSSL
    {
        get { return SessionService.GetBoolValue("MailUseSSL", true); }
        set { SessionService.SetValue("MailUseSSL", value); }
    }
    public static bool DebugMode
    {
        get { return SessionService.GetBoolValue("DebugMode", true); }
        set { SessionService.SetValue("DebugMode", value); }
    }
    public static void Init()
    {
        using (z_repoApplications app = new z_repoApplications())
        {
            using (z_repoCompanys comp = new z_repoCompanys())
            {
                comp.SetDefaultCompany();
                app.SetDefaultApplication();
                IsConfig = true;
            }
        }
    }
}
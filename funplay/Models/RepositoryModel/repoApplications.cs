using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Slapper.AutoMapper;

/// <summary>
/// Applications CRUD
/// </summary>
public class z_repoApplications : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<Applications> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoApplications()
    {
        repo = new EFGenericRepository<Applications>(new dbEntities());
    }

    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <returns></returns>
    public Applications GetEnabledApplication()
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere("");
            str_query += " WHERE (IsEnabled = @IsEnabled) ";
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("IsEnabled", true);
            var model = dp.ReadSingle<Applications>(str_query, parm);
            return model;
        }
    }

    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <returns></returns>
    public Applications GetDapperData()
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("IsEnabled", true);
            var model = dp.ReadSingle<Applications>(str_query, parm);
            return model;
        }
    }

    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Applications> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            var model = dp.ReadAll<Applications>(str_query);
            return model;
        }
    }

    /// <summary>
    /// 取得 SQL 欄位及表格名稱
    /// <summary>
    /// <returns></returns>
    private string GetSQLSelect()
    {
        string str_query = @"
SELECT Id,IsEnabled,IsDebug,AppName,AppVersion,EncryptionMode,PowerBy,LanguageNo 
,GoogleMapKey,MailSenderName,MailSenderEmail,MailReceiverName,MailReceiverEmail 
,MailAppPassword,MailHostUrl,MailHostPort,MailUseSSL,WebSiteUrl,Remark 
FROM dbo.Applications 
";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// <summary>
    /// <returns></returns>
    private string GetSQLWhere()
    {
        string str_query = "WHERE (IsEnabled = @IsEnabled) ";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// <summary>
    /// <param name="searchText">查詢文字</param>
    /// <returns></returns>
    private string GetSQLWhere(string searchText)
    {
        string str_query = "";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += " WHERE ";
            str_query += $"(AppName LIKE '%{searchText}%'  OR ";
            str_query += $"AppVersion LIKE '%{searchText}%'  OR ";
            str_query += $"PowerBy LIKE '%{searchText}%'  OR ";
            str_query += $"MailSenderName LIKE '%{searchText}%'  OR ";
            str_query += $"MailHostUrl LIKE '%{searchText}%'  OR ";
            str_query += $"Remark LIKE '%{searchText}%'  ";
            str_query += ") ";
        }
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 排序
    /// <summary>
    /// <returns></returns>
    private string GetSQLOrderBy()
    {
        return " ORDER BY  AppName";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(Applications model)
    {
        repo.CreateEdit(model, model.Id);
    }
    /// <summary>
    /// 刪除
    /// <summary>
    /// <param name="id">Id</param>
    public void Delete(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        if (model != null) repo.Delete(model, true);
    }
    /// <summary>
    /// 檢查 Id 是否存在
    /// <summary>
    /// <param name="id">Id</param>
    /// <returns></returns>
    public bool IdExists(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        return (model != null);
    }
    #endregion
    #region 自定義事件及函數
    public void SetDefaultApplication()
    {
        var model = GetEnabledApplication();
        if (model != null)
        {
            AppService.AppName = model.AppName;
            AppService.AppVersion = model.AppVersion;
            AppService.EncryptionMode = model.EncryptionMode;
            AppService.PowerBy = model.PowerBy;
            AppService.LanguageNo = model.LanguageNo;
            AppService.WebSiteUrl = model.WebSiteUrl;
            AppService.MailSenderName = model.MailSenderName;
            AppService.MailSenderEmail = model.MailSenderEmail;
            AppService.MailAppPassword = model.MailAppPassword;
            AppService.MailHostUrl = model.MailHostUrl;
            AppService.MailHostPort = model.MailHostPort;
            AppService.MailUseSSL = model.MailUseSSL;
        }
    }
    #endregion
}
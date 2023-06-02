using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 公司主檔 CRUD
/// </summary>
public class z_repoCompanys : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// </summary>
    public IEFGenericRepository<Companys> repo;
    /// <summary>
    /// 建構子
    /// </summary>
    public z_repoCompanys()
    {
        repo = new EFGenericRepository<Companys>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// </summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Companys> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            var model = dp.ReadAll<Companys>(str_query);
            return model;
        }
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// </summary>
    /// <returns></returns>
    public Companys GetDapperDataList()
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere();
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("IsDefault", true);
            var model = dp.ReadSingle<Companys>(str_query, parm);
            return model;
        }
    }
    /// <summary>
    /// 取得 SQL 欄位及表格名稱
    /// </summary>
    /// <returns></returns>
    private string GetSQLSelect()
    {
        string str_query = @"
SELECT Companys.Id, Companys.IsDefault, Companys.IsEnabled, 
Companys.CodeNo,vi_CodeCompany.CodeName,Companys.CompNo, 
Companys.CompName, Companys.ShortName,Companys.EngName, 
Companys.EngShortName, Companys.RegisterDate, Companys.BossName, 
Companys.ContactName, Companys.CompTel, Companys.ContactTel, 
Companys.CompID , Companys.CompFax, Companys.ContactEmail, 
Companys.CompAddress, Companys.CompUrl, Companys.TwitterUrl, 
Companys.FacebookUrl, Companys.InstagramUrl, Companys.SkypeUrl, 
Companys.LinkedinUrl, Companys.Latitude, Companys.Longitude, 
Companys.AboutusText, Companys.SupportText, Companys.ReturnText, 
Companys.ShippingText, Companys.PaymentText, Companys.Remark 
FROM Companys 
LEFT OUTER JOIN vi_CodeCompany ON Companys.CodeNo = vi_CodeCompany.CodeNo  
";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// </summary>
    /// <returns></returns>
    private string GetSQLWhere()
    {
        return " WHERE (Companys.IsDefault = @IsDefault) ";
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// </summary>
    /// <param name="searchText"></param>
    /// <returns></returns>
    private string GetSQLWhere(string searchText)
    {
        string str_query = "";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += "  WHERE (";
            str_query += $"Companys.CompNo LIKE '%{searchText}%'  OR ";
            str_query += $"Companys.CompName LIKE '%{searchText}%'  OR ";
            str_query += $"Companys.ShortName LIKE '%{searchText}%'  OR ";
            str_query += $"Companys.EngName LIKE '%{searchText}%'  OR ";
            str_query += $"Companys.EngShortName LIKE '%{searchText}%'  OR ";
            str_query += $"Companys.BossName LIKE '%{searchText}%'  OR ";
            str_query += $"Companys.ContactName LIKE '%{searchText}%'  OR ";
            str_query += $"Companys.CompTel LIKE '%{searchText}%'  OR ";
            str_query += $"Companys.Remark LIKE '%{searchText}%'  ";
            str_query += ") ";
        }
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 排序
    /// </summary>
    /// <returns></returns>
    private string GetSQLOrderBy()
    {
        return " ORDER BY  Companys.CompNo";
    }
    /// <summary>
    /// 新增或修改
    /// </summary>
    /// <param name="model"></param>
    public void CreateEdit(Companys model)
    {
        repo.CreateEdit(model, model.Id);
    }
    /// <summary>
    /// 刪除
    /// </summary>
    /// <param name="id">Id</param>
    public void Delete(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        if (model != null) repo.Delete(model, true);
    }
    /// <summary>
    /// 取得名稱
    /// </summary>
    /// <param name="dataNo">編號</param>
    /// <returns></returns>
    public string GetDataName(string dataNo)
    {
        string str_value = "";
        var model = repo.ReadSingle(m => m.CompNo == dataNo);
        if (model != null) str_value = model.ShortName;
        return str_value;
    }
    /// <summary>
    /// 檢查 Id 是否存在
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IdExists(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        return (model != null);
    }
    #endregion
    #region 自定義事件及函數
    /// <summary>
    /// 設定預設公司資訊
    /// </summary>
    public void SetDefaultCompany()
    {
        AppService.CompanyNo = "";
        AppService.CompanyName = "";
        AppService.CompanyShortName = "";
        AppService.EnglishName = "";
        AppService.EnglishShortName = "";
        var model = repo.ReadSingle(m => m.IsDefault == true);
        if (model != null)
        {
            AppService.CompanyNo = model.CompNo;
            AppService.CompanyName = model.CompName;
            AppService.CompanyShortName = model.ShortName;
            AppService.EnglishName = model.EngName;
            AppService.EnglishShortName = model.EngShortName;
        }
    }
    /// <summary>
    /// 取得預設公司資訊
    /// </summary>
    public Companys GetDefaultCompany()
    {
        return repo.ReadSingle(m => m.IsDefault);
    }
    #endregion
}
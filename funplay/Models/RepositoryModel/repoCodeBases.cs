using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// CodeBases CRUD
/// </summary>
public class z_repoCodeBases : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<CodeBases> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoCodeBases()
    {
        repo = new EFGenericRepository<CodeBases>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="isAdmin">後台參數</param>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<CodeBases> GetDapperDataList(bool isAdmin, string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("IsAdmin", isAdmin);
            var model = dp.ReadAll<CodeBases>(str_query, parm);
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
SELECT 
Id, IsAdmin , BaseNo, BaseName, DefaultValue, Remark , 
(SELECT count(*) FROM CodeDatas WHERE CodeDatas.BaseNo = CodeBases.BaseNo) AS Counts 
 FROM CodeBases 
";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// <summary>
    /// <param name="searchText">查詢文字</param>
    /// <returns></returns>
    private string GetSQLWhere(string searchText)
    {
        string str_query = "WHERE (IsAdmin = @IsAdmin) ";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += " AND  (";
            str_query += $"BaseNo LIKE '%{searchText}%'  OR ";
            str_query += $"BaseName LIKE '%{searchText}%'  OR ";
            str_query += $"DefaultValue LIKE '%{searchText}%'  OR ";
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
        return " ORDER BY  BaseNo";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(CodeBases model)
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
    /// 取得名稱
    /// <summary>
    /// <param name="dataNo">編號</param>
    /// <returns></returns>
    public string GetDataName(string dataNo)
    {
        string str_value = "";
        var model = repo.ReadSingle(m => m.BaseNo == dataNo);
        if (model != null) str_value = model.BaseName;
        return str_value;
    }
    /// <summary>
    /// 檢查 Id 是否存在
    /// <summary>
    /// <param name="id">主鍵值</param>
    /// <returns></returns>
    public bool IdExists(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        return (model != null);
    }
    #endregion
    #region 自定義事件及函數
    #endregion
}

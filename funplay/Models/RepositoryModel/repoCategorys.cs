using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Categorys CRUD
/// </summary>
public class z_repoCategorys : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<Categorys> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoCategorys()
    {
        repo = new EFGenericRepository<Categorys>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Categorys> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("ParentNo", SessionService.TagNo1);
            var model = dp.ReadAll<Categorys>(str_query, parm);
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
Id, IsEnabled, ParentNo, CategoryNo, CategoryName
, Remark FROM Categorys 
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
        string str_query = " WHERE (ParentNo = @ParentNo) ";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += " AND (";
            str_query += $"ParentNo LIKE '%{searchText}%'  OR ";
            str_query += $"CategoryNo LIKE '%{searchText}%'  OR ";
            str_query += $"CategoryName LIKE '%{searchText}%'  OR ";
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
        return " ORDER BY  CategoryNo";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(Categorys model)
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
    public void SetCategoryData(int id)
    {
        SessionService.TagId1 = id;
        SessionService.TagNo1 = "";
        SessionService.TagName1 = "";
        var model = repo.ReadSingle(m => m.Id == id);
        if (model != null)
        {
            SessionService.TagNo1 = model.CategoryNo;
            SessionService.TagName1 = model.CategoryName;
        }
        if (!string.IsNullOrEmpty(SessionService.TagNo1))
            PrgService.SubHeader = $"{SessionService.TagNo1} {SessionService.TagName1}";
        else
            PrgService.SubHeader = "";
    }
    #endregion
}

using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// MapPositions CRUD
/// </summary>
public class z_repoMapPositions : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<MapPositions> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoMapPositions()
    {
        repo = new EFGenericRepository<MapPositions>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<MapPositions> GetDapperDataUserList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(true, searchText);
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("TargetCode", "UserNo");
            parm.Add("TargetNo", UserService.UserNo);
            var model = dp.ReadAll<MapPositions>(str_query, parm);
            return model;
        }
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<MapPositions> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(false, searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<MapPositions>(str_query);
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
Id, TargetCode, TargetNo, TitleName, ContactName
, ContactTel, ContactEmail, ContactAddress, Latitude, Longitude
, Remark FROM MapPositions 
";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// <summary>
    /// <param name="isUser">使用者</param>
    /// <param name="searchText">查詢文字</param>
    /// <returns></returns>
    private string GetSQLWhere(bool isUser, string searchText)
    {
        string str_query = (isUser) ? " WHERE (TargetCode = @TargetCode AND TargetNo = @TargetNo) " : "";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += (isUser) ? "AND (" : " WHERE (";
            str_query += $"TargetCode LIKE '%{searchText}%'  OR ";
            str_query += $"TargetNo LIKE '%{searchText}%'  OR ";
            str_query += $"TitleName LIKE '%{searchText}%'  OR ";
            str_query += $"ContactName LIKE '%{searchText}%'  OR ";
            str_query += $"ContactTel LIKE '%{searchText}%'  OR ";
            str_query += $"ContactEmail LIKE '%{searchText}%'  OR ";
            str_query += $"ContactAddress LIKE '%{searchText}%'  OR ";
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
        return " ORDER BY  TargetCode,TargetNo , TitleName";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(MapPositions model)
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
    #endregion
}

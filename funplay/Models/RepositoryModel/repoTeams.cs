using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Teams CRUD
/// </summary>
public class z_repoTeams : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<Teams> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoTeams()
    {
        repo = new EFGenericRepository<Teams>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Teams> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<Teams>(str_query);
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
SELECT Teams.Id, Teams.SortNo, Teams.TeamNo, Teams.TeamName, Teams.EngName, 
Teams.GenderCode, vi_CodeGender.CodeName, Teams.DeptName, Teams.TitleName, 
Teams.TwitterUrl, Teams.FacebookUrl, Teams.LinkedinUrl, Teams.InstagramUrl, 
Teams.SkypeUrl, Teams.ContactEmail, Teams.DetailText, Teams.Remark
FROM Teams 
LEFT OUTER JOIN vi_CodeGender ON Teams.GenderCode = vi_CodeGender.CodeNo 
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
        string str_query = "";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += " WHERE (";
            str_query += $"Teams.TeamNo LIKE '%{searchText}%'  OR ";
            str_query += $"Teams.TeamName LIKE '%{searchText}%'  OR ";
            str_query += $"Teams.EngName LIKE '%{searchText}%'  OR ";
            str_query += $"Teams.DeptName LIKE '%{searchText}%'  OR ";
            str_query += $"Teams.TitleName LIKE '%{searchText}%'  OR ";
            str_query += $"Teams.TwitterUrl LIKE '%{searchText}%'  OR ";
            str_query += $"Teams.FacebookUrl LIKE '%{searchText}%'  OR ";
            str_query += $"Teams.LinkedinUrl LIKE '%{searchText}%'  OR ";
            str_query += $"Teams.InstagramUrl LIKE '%{searchText}%'  OR ";
            str_query += $"Teams.SkypeUrl LIKE '%{searchText}%'  OR ";
            str_query += $"Teams.Remark LIKE '%{searchText}%'  ";
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
        return " ORDER BY  Teams.SortNo, Teams.TeamNo";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(Teams model)
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
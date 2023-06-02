using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Forums CRUD
/// </summary>
public class z_repoForums : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<Forums> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoForums()
    {
        repo = new EFGenericRepository<Forums>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="id">文章GUID</param>
    /// <returns></returns>
    public List<Forums> GetDapperDetailList(string id)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere();
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("ParentGuid", id);
            var model = dp.ReadAll<Forums>(str_query, parm);
            return model;
        }
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <paramref name="boardNo">版塊編號</param>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Forums> GetDapperDataList(string boardNo, string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("BoardNo", boardNo);
            parm.Add("ParentGuid", "");
            var model = dp.ReadAll<Forums>(str_query, parm);
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
SELECT Forums.Id, Forums.ParentGuid, Forums.ReplyGuid, Forums.BoardNo, 
Forums.IsEnabled, Forums.IsClosed, Forums.SubjectDate, Forums.UserNo, 
Users.UserName, Forums.SubjectName, Forums.SubjectContent, 
Forums.GuidNo, Forums.Remark , 
(SELECT count(*) FROM Forums AS Reply WHERE Reply.ParentGuid = Forums.GuidNo) AS ReplyCount 
FROM Forums 
LEFT OUTER JOIN Users ON Forums.UserNo = Users.UserNo 
";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// <summary>
    /// <returns></returns>
    private string GetSQLWhere()
    {
        string str_query = "";
        str_query += " WHERE ( Forums.ParentGuid = @ParentGuid ) ";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// <summary>
    /// <returns></returns>
    private string GetSQLWhereForm()
    {
        string str_query = "";
        str_query += " WHERE ( Forums.GuidNo = @GuidNo) ";
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
        str_query += " WHERE (";
        str_query += " Forums.BoardNo = @BoardNo  AND ";
        str_query += " Forums.ParentGuid = @ParentGuid)   ";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += $"AND (";
            str_query += $"Users.UserName LIKE '%{searchText}%'  OR ";
            str_query += $"Forums.SubjectName LIKE '%{searchText}%'  OR ";
            str_query += $"Forums.Remark LIKE '%{searchText}%'  ";
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
        return " ORDER BY  Forums.SubjectDate";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(Forums model)
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
    /// 取得文章主旨及內文
    /// <summary>
    /// <param name="id">文章 GUID</param>
    /// <returns></returns>
    public Forums GetForums(string id)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhereForm();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("GuidNo", id);
            var model = dp.ReadSingle<Forums>(str_query, parm);
            return model;
        }
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

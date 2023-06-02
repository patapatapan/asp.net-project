using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TodoLists CRUD
/// </summary>
public class z_repoTodoLists : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<TodoLists> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoTodoLists()
    {
        repo = new EFGenericRepository<TodoLists>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<TodoLists> GetDapperDataUserList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText, true);
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("UserNo", UserService.UserNo);
            var model = dp.ReadAll<TodoLists>(str_query, parm);
            return model;
        }
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<TodoLists> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText, false);
            str_query += GetSQLOrderBy();
            var model = dp.ReadAll<TodoLists>(str_query);
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
SELECT TodoLists.Id, TodoLists.IsFinished, TodoLists.UserNo, TodoLists.CodeNo, 
vi_TodoList.CodeName, TodoLists.TitleName, TodoLists.DeadlineDate, TodoLists.Remark
FROM TodoLists 
LEFT OUTER JOIN vi_TodoList ON TodoLists.CodeNo = vi_TodoList.CodeNo
";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// <summary>
    /// <param name="isUser">使用者</param>
    /// <param name="searchText">查詢文字</param>
    /// <returns></returns>
    private string GetSQLWhere(string searchText, bool isUser)
    {
        string str_query = "";
        if (isUser) str_query += " WHERE (TodoLists.UserNo = @UserNo) ";
        if (!string.IsNullOrEmpty(searchText))
        {
            if (string.IsNullOrEmpty(str_query))
                str_query += " WHERE (";
            else
                str_query += " AND (";
            str_query += $"TodoLists.UserNo LIKE '%{searchText}%'  OR ";
            str_query += $"vi_TodoList.CodeName LIKE '%{searchText}%'  OR ";
            str_query += $"TodoLists.TitleName LIKE '%{searchText}%'  OR ";
            str_query += $"TodoLists.Remark LIKE '%{searchText}%'  ";
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
        return " ORDER BY  TodoLists.DeadlineDate DESC ";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(TodoLists model)
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

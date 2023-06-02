using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Messages CRUD
/// </summary>
public class z_repoMessages : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<Messages> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoMessages()
    {
        repo = new EFGenericRepository<Messages>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Messages> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            var model = dp.ReadAll<Messages>(str_query);
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
SELECT Messages.Id, Messages.IsRead, Messages.CodeNo, vi_CodeMessage.CodeName,
Messages.SenderNo, Users.UserName AS SenderName,
Messages.ReceiverNo, Users_1.UserName AS ReceiverName, Messages.SendDate, 
Messages.SendTime, Messages.HeaderText, 
Messages.MessageText, Messages.Remark
FROM  Messages 
LEFT OUTER JOIN Users ON Messages.SenderNo = Users.UserNo 
LEFT OUTER JOIN Users AS Users_1 ON Messages.ReceiverNo = Users_1.UserNo 
LEFT OUTER JOIN vi_CodeMessage ON Messages.CodeNo = vi_CodeMessage.CodeNo 
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
            str_query += $"Messages.SenderNo LIKE '%{searchText}%'  OR ";
            str_query += $"Messages.SenderName LIKE '%{searchText}%'  OR ";
            str_query += $"Messages.ReceiverNo LIKE '%{searchText}%'  OR ";
            str_query += $"Messages.ReceiverName LIKE '%{searchText}%'  OR ";
            str_query += $"Messages.HeaderText LIKE '%{searchText}%'  OR ";
            str_query += $"Messages.Remark LIKE '%{searchText}%'  ";
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
        return " ORDER BY  Messages.CodeNo,Messages.SendDate,Messages.SendTime";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(Messages model)
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

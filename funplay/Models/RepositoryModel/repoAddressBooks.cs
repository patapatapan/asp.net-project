using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AddressBooks CRUD
/// </summary>
public class z_repoAddressBooks : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<AddressBooks> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoAddressBooks()
    {
        repo = new EFGenericRepository<AddressBooks>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<AddressBooks> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("UserNo", UserService.UserNo);
            var model = dp.ReadAll<AddressBooks>(str_query, parm);
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
SELECT AddressBooks.Id, AddressBooks.UserNo, AddressBooks.CodeNo, 
vi_CodeAddressBook.CodeName, AddressBooks.FirstName, AddressBooks.LastName, 
AddressBooks.EngName, AddressBooks.GenderCode, 
CASE GenderCode WHEN 'M' THEN '男' WHEN 'F' THEN '女' ELSE '' END AS GenderName, 
AddressBooks.Birthday, AddressBooks.CompName, AddressBooks.CompID, 
AddressBooks.DeptName, AddressBooks.TitleName, AddressBooks.CompTel, 
AddressBooks.ContactTel, AddressBooks.ContactEmail, AddressBooks.ContactAddress, 
AddressBooks.LineID, AddressBooks.FacebookID, AddressBooks.TwitterID, 
AddressBooks.InstagramID, AddressBooks.LinkedInID, AddressBooks.Remark
FROM AddressBooks 
LEFT OUTER JOIN vi_CodeAddressBook ON AddressBooks.CodeNo = vi_CodeAddressBook.CodeNo 
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
        string str_query = "WHERE (UserNo = @UserNo) ";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += " AND (";
            str_query += $"vi_CodeAddressBook.CodeName LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.FirstName LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.LastName LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.EngName LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.CompName LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.CompID LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.DeptName LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.TitleName LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.CompTel LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.ContactTel LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.ContactEmail LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.ContactAddress LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.LineID LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.FacebookID LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.TwitterID LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.InstagramID LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.LinkedInID LIKE '%{searchText}%'  OR ";
            str_query += $"AddressBooks.Remark LIKE '%{searchText}%'  ";
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
        return " ORDER BY  AddressBooks.FirstName , AddressBooks.LastName";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(AddressBooks model)
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

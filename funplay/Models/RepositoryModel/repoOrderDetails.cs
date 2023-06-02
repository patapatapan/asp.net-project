using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// OrderDetails CRUD
/// </summary>
public class z_repoOrderDetails : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<OrderDetails> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoOrderDetails()
    {
        repo = new EFGenericRepository<OrderDetails>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<OrderDetails> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<OrderDetails>(str_query);
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
SELECT OrderDetails.Id, OrderDetails.ParentNo, OrderDetails.VendorNo, 
OrderDetails.CategoryNo, Categorys.CategoryName, OrderDetails.ProdNo, 
OrderDetails.ProdName, OrderDetails.ProdSpec, OrderDetails.OrderPrice, 
OrderDetails.OrderQty, OrderDetails.OrderAmount, OrderDetails.Remark
FROM OrderDetails 
LEFT OUTER JOIN Categorys ON OrderDetails.CategoryNo = Categorys.CategoryNo 
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
            str_query += $"OrderDetails.CategoryName LIKE '%{searchText}%'  OR ";
            str_query += $"OrderDetails.CategoryNo LIKE '%{searchText}%'  OR ";
            str_query += $"OrderDetails.ProdNo LIKE '%{searchText}%'  OR ";
            str_query += $"OrderDetails.ProdName LIKE '%{searchText}%'  OR ";
            str_query += $"OrderDetails.ProdSpec LIKE '%{searchText}%'  OR ";
            str_query += $"OrderDetails.Remark LIKE '%{searchText}%'  ";
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
        return " ORDER BY  OrderDetails.ParentNo,OrderDetails.ProdNo";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(OrderDetails model)
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

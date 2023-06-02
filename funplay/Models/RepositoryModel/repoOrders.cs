using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Orders CRUD
/// </summary>
public class z_repoOrders : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<Orders> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoOrders()
    {
        repo = new EFGenericRepository<Orders>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Orders> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<Orders>(str_query);
            return model;
        }
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="isClosed">是否結案</param>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Orders> GetDapperDataList(bool isClosed, string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhereIsClose(searchText);
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("IsClosed", isClosed);
            var model = dp.ReadAll<Orders>(str_query);
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
SELECT Orders.Id, Orders.SheetNo, Orders.SheetDate, Orders.StatusCode, 
OrdersStatus.StatusName, Orders.IsClosed, Orders.IsValid, Orders.CustNo, 
Orders.CustName, Orders.PaymentNo, Payments.PaymentName, Orders.ShippingNo, 
Shippings.ShippingName, Orders.ReceiverName, Orders.ReceiverEmail, 
Orders.ReceiverAddress, Orders.OrderAmount, Orders.TaxAmount, 
Orders.TotalAmount, Orders.Remark, Orders.GuidNo
FROM  Orders 
LEFT OUTER JOIN Shippings ON Orders.ShippingNo = Shippings.ShippingNo 
LEFT OUTER JOIN Payments ON Orders.PaymentNo = Payments.PaymentNo 
LEFT OUTER JOIN OrdersStatus ON Orders.StatusCode = OrdersStatus.StatusNo 
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
            str_query += " WHERE ( ";
            str_query += $"Orders.SheetNo LIKE '%{searchText}%'  OR ";
            str_query += $"OrdersStatus.StatusName LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.CustNo LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.CustName LIKE '%{searchText}%'  OR ";
            str_query += $"Payments.PaymentName LIKE '%{searchText}%'  OR ";
            str_query += $"Shippings.ShippingName LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.ReceiverName LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.ReceiverEmail LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.ReceiverAddress LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.Remark LIKE '%{searchText}%'   ";
            str_query += ") ";
        }
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// <summary>
    /// <param name="searchText">查詢文字</param>
    /// <returns></returns>
    private string GetSQLWhereIsClose(string searchText)
    {
        string str_query = "WHERE (IsClosed = @IsClosed) ";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += " AND (";
            str_query += $"Orders.SheetNo LIKE '%{searchText}%'  OR ";
            str_query += $"OrdersStatus.StatusName LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.CustNo LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.CustName LIKE '%{searchText}%'  OR ";
            str_query += $"Payments.PaymentName LIKE '%{searchText}%'  OR ";
            str_query += $"Shippings.ShippingName LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.ReceiverName LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.ReceiverEmail LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.ReceiverAddress LIKE '%{searchText}%'  OR ";
            str_query += $"Orders.Remark LIKE '%{searchText}%'   ";
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
        return " ORDER BY  Orders.SheetNo DESC";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(Orders model)
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

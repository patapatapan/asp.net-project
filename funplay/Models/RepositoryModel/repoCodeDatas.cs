using Dapper;
using DocumentFormat.OpenXml.EMMA;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// CodeDatas CRUD
/// </summary>
public class z_repoCodeDatas : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<CodeDatas> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoCodeDatas()
    {
        repo = new EFGenericRepository<CodeDatas>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <paramref name="baseNo">類別代號</param>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<CodeDatas> GetDapperDataList(string baseNo, string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("BaseNo", baseNo);
            var model = dp.ReadAll<CodeDatas>(str_query, parm);
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
SELECT Id,IsEnabled,BaseNo,ParentNo,SortNo,CodeNo,CodeName,CodeValue,Remark 
FROM CodeDatas 
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
        string str_query = " WHERE ";
        str_query += $"BaseNo = @BaseNo ";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += "AND (";
            str_query += $"SortNo LIKE '%{searchText}%'  OR ";
            str_query += $"CodeNo LIKE '%{searchText}%'  OR ";
            str_query += $"CodeName LIKE '%{searchText}%'  OR ";
            str_query += $"CodeValue LIKE '%{searchText}%'  OR ";
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
        return " ORDER BY  ParentNo,SortNo,CodeNo";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(CodeDatas model)
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
    /// <summary>
    /// 取得 Code 值
    /// </summary>
    /// <param name="baseNo">分類代號</param>
    /// <param name="codeNo">主檔編號</param>
    /// <returns></returns>
    public string GetCodeValue(string baseNo, string codeNo)
    {
        var data = repo.ReadSingle(m => m.BaseNo == baseNo && m.CodeNo == codeNo);
        if (data != null) return (data.CodeValue == null) ? "" : data.CodeValue;
        return "";
    }

    /// <summary>
    /// 取得主檔資料列表
    /// </summary>
    /// <param name="baseNo">主檔代號</param>
    /// <returns></returns>
    public List<SelectListItem> GetBaseDataList(string baseNo)
    {
        var data = repo.ReadAll(m => m.BaseNo == baseNo)
                .OrderBy(m => m.SortNo)
                .ThenBy(m => m.CodeNo)
                .Select(u => new SelectListItem
                {
                    Text = u.CodeNo + " " + u.CodeName,
                    Value = u.CodeNo
                }).ToList();
        return (List<SelectListItem>)data;
    }
    #endregion
}

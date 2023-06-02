using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 模組主檔 CRUD
/// </summary>
public class z_repoModules : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// </summary>
    public IEFGenericRepository<Modules> repo;
    /// <summary>
    /// 建構子
    /// </summary>
    public z_repoModules()
    {
        repo = new EFGenericRepository<Modules>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// </summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Modules> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<Modules>(str_query);
            return model;
        }
    }
    /// <summary>
    /// 取得 SQL 欄位及表格名稱
    /// </summary>
    /// <returns></returns>
    private string GetSQLSelect()
    {
        string str_query = @"
SELECT Modules.Id, Modules.IsEnabled, Modules.IsWorkflow,Modules.RoleNo, 
Roles.RoleName, Modules.SortNo, Modules.ModuleNo, 
Modules.ModuleName, Modules.IconName, Modules.Remark
FROM  Modules 
LEFT OUTER JOIN Roles ON Modules.RoleNo = Roles.RoleNo 
";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// </summary>
    /// <param name="searchText"></param>
    /// <returns></returns>
    private string GetSQLWhere(string searchText)
    {
        string str_query = "";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += " WHERE ";
            str_query += $"(Modules.ModuleNo LIKE '%{searchText}%' OR ";
            str_query += $"Modules.ModuleName LIKE '%{searchText}%' OR ";
            str_query += $"Modules.RoleNo LIKE '%{searchText}%' OR ";
            str_query += $"Roles.RoleName LIKE '%{searchText}%' OR ";
            str_query += $"Modules.IconName LIKE '%{searchText}%' OR ";
            str_query += $"Modules.Remark LIKE '%{searchText}%') ";
        }
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 排序
    /// </summary>
    /// <returns></returns>
    private string GetSQLOrderBy()
    {
        return " ORDER BY  Modules.RoleNo , Modules.SortNo, Modules.ModuleNo";
    }
    /// <summary>
    /// 新增或修改
    /// </summary>
    /// <param name="model"></param>
    public void CreateEdit(Modules model)
    {
        repo.CreateEdit(model, model.Id);
    }
    /// <summary>
    /// 刪除
    /// </summary>
    /// <param name="id">Id</param>
    public void Delete(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        if (model != null) repo.Delete(model, true);
    }
    /// <summary>
    /// 取得名稱
    /// </summary>
    /// <param name="dataNo">編號</param>
    /// <returns></returns>
    public string GetDataName(string dataNo)
    {
        string str_value = "";
        var model = repo.ReadSingle(m => m.ModuleNo == dataNo);
        if (model != null) str_value = model.ModuleName;
        return str_value;
    }
    /// <summary>
    /// 取得名稱
    /// </summary>
    /// <param name="roleNo">角色</param>
    /// <param name="dataNo">編號</param>
    /// <returns></returns>
    public string GetDataName(string roleNo, string dataNo)
    {
        string str_value = "";
        var model = repo.ReadSingle(m => m.RoleNo == roleNo && m.ModuleNo == dataNo);
        if (model != null) str_value = model.ModuleName;
        return str_value;
    }
    /// <summary>
    /// 檢查 Id 是否存在
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IdExists(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        return (model != null);
    }
    #endregion
    #region 自定義事件及函數
    public List<Modules> GetModuleList(string roleNo)
    {
        using (z_repoSecuritys sec = new z_repoSecuritys())
        {
            List<Modules> module = new List<Modules>();
            var data = repo.ReadAll(m =>
                    m.RoleNo == roleNo &&
                    m.IsEnabled == true)
                .OrderBy(m => m.SortNo)
                .ThenBy(m => m.ModuleNo)
                .ToList();

            if (data != null)
            {
                foreach (var item in data)
                {
                    if (sec.IsModuleSecurity(item.ModuleNo))
                    {
                        module.Add(item);
                    }
                }
            }
            return module;
        }
    }
    #endregion
}
using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 程式主檔 CRUD
/// </summary>
public class z_repoPrograms : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// </summary>
    public IEFGenericRepository<Programs> repo;
    /// <summary>
    /// 建構子
    /// </summary>
    public z_repoPrograms()
    {
        repo = new EFGenericRepository<Programs>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// </summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Programs> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText, false, false);
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("IsEnabled", true);
            var model = dp.ReadAll<Programs>(str_query, parm);
            return model;
        }
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// </summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Programs> GetDapperDataListAll(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText, false, true);
            str_query += GetSQLOrderBy();
            var model = dp.ReadAll<Programs>(str_query);
            return model;
        }
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// </summary>
    /// <param name="roleNo">角色代號</param>
    /// <param name="moduleNo">模組代號</param>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Programs> GetDapperDataList(string roleNo, string moduleNo, string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText, true, false);
            str_query += " AND (Programs.ModuleNo = @ModuleNo)  ";
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("RoleNo", roleNo);
            parm.Add("ModuleNo", moduleNo);
            parm.Add("IsEnabled", true);
            var model = dp.ReadAll<Programs>(str_query, parm);
            return model;
        }
    }

    /// <summary>
    /// 取得 SQL 欄位及表格名稱
    /// </summary>
    /// <returns></returns>
    private string GetSQLSelect()
    {
        return @"
SELECT Programs.Id, Programs.IsEnabled, Programs.RoleNo, Roles.RoleName, 
Programs.ModuleNo, Modules.ModuleName, Programs.SortNo, 
Programs.PrgNo, Programs.PrgName, Programs.CodeNo, vi_CodeProgram.CodeName, 
Programs.AreaName, Programs.ControllerName, Programs.ActionName, 
Programs.ParmValue, Programs.Remark , 
(SELECT count(*) FROM Securitys WHERE PrgNo = Programs.PrgNo) AS Users 
FROM Programs 
LEFT OUTER JOIN Modules ON Programs.ModuleNo = Modules.ModuleNo 
LEFT OUTER JOIN vi_CodeProgram ON vi_CodeProgram.CodeNo = Programs.CodeNo 
LEFT OUTER JOIN Roles ON Programs.RoleNo = Roles.RoleNo  
";
    }

    private string GetSQLWhere(string searchText, bool roleNo, bool allData)
    {
        string str_query = " ";
        if (roleNo || !allData || !string.IsNullOrEmpty(searchText)) str_query += "WHERE  ";
        if (roleNo || !allData) str_query += " (";
        if (roleNo) { str_query += " Programs.RoleNo = @RoleNo AND "; }
        if (!allData) str_query += "Programs.IsEnabled = @IsEnabled ";
        if (roleNo || !allData) str_query += ") ";
        if (!string.IsNullOrEmpty(searchText))
        {
            if (roleNo || !allData)
                str_query += "AND (";
            else
                str_query += "(";
            str_query += $"Modules.ModuleName LIKE '%{searchText}%' OR ";
            str_query += $"Programs.PrgNo LIKE '%{searchText}%' OR ";
            str_query += $"Programs.PrgName LIKE '%{searchText}%' OR ";
            str_query += $"Programs.AreaName LIKE '%{searchText}%' OR ";
            str_query += $"Programs.ControllerName LIKE '%{searchText}%' OR ";
            str_query += $"Programs.ActionName LIKE '%{searchText}%' OR ";
            str_query += $"Programs.ParmValue LIKE '%{searchText}%' OR ";
            str_query += $"Roles.RoleNo LIKE '%{searchText}%' OR ";
            str_query += $"Programs.Remark LIKE '%{searchText}%') ";
        }
        return str_query;
    }

    /// <summary>
    /// 取得 SQL 排序
    /// </summary>
    /// <returns></returns>
    private string GetSQLOrderBy()
    {
        return " ORDER BY  Programs.RoleNo, Programs.SortNo , Programs.PrgNo";
    }
    /// <summary>
    /// 新增或修改
    /// </summary>
    /// <param name="model"></param>
    public void CreateEdit(Programs model)
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
        var model = repo.ReadSingle(m => m.PrgNo == dataNo);
        if (model != null) str_value = model.PrgName;
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
    /// <summary>
    /// 取得同角色同程式資料
    /// </summary>
    /// <param name="roleNo">角色編號</param>
    /// <param name="prgNo">程式編號</param>
    /// <returns></returns>
    public Programs GetData(string roleNo, string prgNo)
    {
        return repo.ReadSingle(m => m.RoleNo == roleNo && m.PrgNo == prgNo);
    }
    /// <summary>
    /// 取得模組編號
    /// </summary>
    /// <param name="prgNo">程式編號</param>
    /// <returns></returns>
    public string GetModuleNo(string roleNo, string prgNo)
    {
        string str_value = "";
        var data = repo.ReadSingle(m => m.RoleNo == roleNo && m.PrgNo == prgNo);
        if (data != null) str_value = data.ModuleNo;
        return str_value;
    }
    /// <summary>
    /// 取得程式列表
    /// </summary>
    /// <param name="roleNo">角色編號</param>
    /// <param name="moduleNo">模組編號</param>
    /// <returns></returns>
    public List<Programs> GetPrgList(string roleNo, string moduleNo)
    {
        using (z_repoSecuritys sec = new z_repoSecuritys())
        {
            List<Programs> programs = new List<Programs>();
            var data = repo.ReadAll(m =>
                m.RoleNo == roleNo &&
                m.ModuleNo == moduleNo &&
                m.IsEnabled == true)
            .OrderBy(m => m.SortNo)
            .ThenBy(m => m.PrgNo)
            .ToList();

            if (data != null)
            {
                foreach (var item in data)
                {
                    if (sec.IsProgramSecurity(moduleNo, item.PrgNo))
                    {
                        programs.Add(item);
                    }
                }
            }
            return programs;
        }
    }
    /// <summary>
    /// 取得副標題名稱
    /// </summary>
    /// <param name="moduleNo">模組代號</param>
    /// <param name="titleName">副標題</param>
    public void SetSubHeader(string moduleNo, string titleName)
    {
        string str_value = "";
        var model = repo.ReadSingle(m => m.PrgNo == moduleNo);
        if (model != null)
        {
            str_value = $"{titleName}：{moduleNo} {model.PrgName}";
        }
        PrgService.SubHeader = str_value;
    }
    #endregion
}
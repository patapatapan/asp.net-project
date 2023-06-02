using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 部門主檔 CRUD
/// </summary>
public class z_repoLogs : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// </summary>
    public IEFGenericRepository<Logs> repo;
    /// <summary>
    /// 建構子
    /// </summary>
    public z_repoLogs()
    {
        repo = new EFGenericRepository<Logs>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// </summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Logs> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            var model = dp.ReadAll<Logs>(str_query);
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
SELECT Logs.Id, Logs.LogDate, Logs.LogTime, Logs.CodeNo, vi_CodeLog.CodeName, 
Logs.UserNo, Logs.UserName, Logs.TargetNo, Logs.LogNo, Logs.LogQty, Logs.Remark
FROM Logs 
LEFT OUTER JOIN vi_CodeLog ON Logs.CodeNo = vi_CodeLog.CodeNo  
";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// </summary>
    /// <param name="searchText">查詢文字</param>
    /// <returns></returns>
    private string GetSQLWhere(string searchText)
    {
        string str_query = "";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += " WHERE  ";
            str_query += $"(Logs.UserNo LIKE '%{searchText}%' OR ";
            str_query += $"Logs.UserName LIKE '%{searchText}%' OR ";
            str_query += $"vi_CodeLog.CodeName LIKE '%{searchText}%' OR ";
            str_query += $"Logs.LogNo LIKE '%{searchText}%' OR ";
            str_query += $"Logs.Remark LIKE '%{searchText}%') ";
        }
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 排序
    /// </summary>
    /// <returns></returns>
    private string GetSQLOrderBy()
    {
        return " ORDER BY  Logs.LogDate , Logs.LogTime ";
    }
    /// <summary>
    /// 新增或修改
    /// </summary>
    /// <param name="model"></param>
    public void CreateEdit(Logs model)
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
    /// 事件記錄累計
    /// </summary>
    /// <param name="typeNo">Log 類別</param>
    /// <param name="targetNo">對象帳號</param>
    /// <param name="logNo">對象編號</param>
    public void EventLogCount(enLogType typeNo, string targetNo, string logNo)
    {
        //記一筆最後的時間,但次數要累加
        using (z_repoUsers users = new z_repoUsers())
        {
            string str_type_no = typeNo.ToString();
            var targetUser = users.repo.ReadSingle(m => m.UserNo == targetNo);
            var data = repo.ReadSingle(m =>
                    m.CodeNo == str_type_no &&
                    m.UserNo == UserService.UserNo &&
                    m.TargetNo == targetNo &&
                    m.LogNo == logNo);
            if (data != null)
            {
                data.LogDate = DateTime.Today;
                data.LogTime = DateTime.Now;
                data.LogQty += 1;
                repo.Update(data);
                repo.SaveChanges();
            }
            else
            {
                Logs newData = new Logs();
                newData.LogDate = DateTime.Today;
                newData.LogTime = DateTime.Now;
                newData.CodeNo = str_type_no;
                newData.UserNo = UserService.UserNo;
                newData.TargetNo = targetNo;
                newData.LogNo = logNo;
                newData.LogQty = 1;
                repo.Create(newData);
                repo.SaveChanges();
            }
        }
    }
    /// <summary>
    /// 事件記錄累計
    /// </summary>
    /// <param name="typeNo">Log 類別</param>
    /// <param name="targetNo">對象帳號</param>
    /// <param name="logNo">對象編號</param>
    public void EventLogDetail(enLogType typeNo, string targetNo, string logNo)
    {
        //記一筆最後的時間,但次數要累加
        using (z_repoUsers users = new z_repoUsers())
        {
            string str_type_no = typeNo.ToString();
            var targetUser = users.repo.ReadSingle(m => m.UserNo == targetNo);
            Logs newData = new Logs();
            newData.LogDate = DateTime.Today;
            newData.LogTime = DateTime.Now;
            newData.CodeNo = str_type_no;
            newData.UserNo = UserService.UserNo;
            newData.TargetNo = targetNo;
            newData.LogNo = logNo;
            newData.LogQty = 1;
            repo.Create(newData);
            repo.SaveChanges();
        }
    }
    #endregion
}
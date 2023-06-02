using Dapper;
using funplay.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// PropertyNames CRUD
/// </summary>
public class z_repoPropertyNames : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<PropertyNames> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoPropertyNames()
    {
        repo = new EFGenericRepository<PropertyNames>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<PropertyNames> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<PropertyNames>(str_query);
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
SELECT 
Id, PropName, DisplayName, Remark FROM PropertyNames 
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
            str_query += $"PropName LIKE '%{searchText}%'  OR ";
            str_query += $"DisplayName LIKE '%{searchText}%'  OR ";
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
        return " ORDER BY  PropName";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(PropertyNames model)
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
    /// 取得名稱
    /// <summary>
    /// <param name="dataNo">編號</param>
    /// <returns></returns>
    public string GetDataName(string dataNo)
    {
        string str_value = "";
        var model = repo.ReadSingle(m => m.PropName == dataNo);
        if (model != null) str_value = model.DisplayName;
        return str_value;
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
    /// <summary>
    /// 自動加入現在的屬性到資料庫中
    /// </summary>
    /// <returns></returns>
    public int AddPropertyName()
    {

        using (CodeBase code = new CodeBase())
        {
            using (z_repoPropertyNames propName = new z_repoPropertyNames())
            {
                int int_count = 0;
                List<dmColumnProperty> values = new List<dmColumnProperty>();
                List<dmColumnProperty> prop = new List<dmColumnProperty>();
                List<string> classList = new List<string>();
                classList = code.NameSpaceClasses();
                foreach (string className in classList)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        string str_meta = (i == 0) ? "z_meta" : "";
                        prop = code.GetClassPropertyList(className, str_meta);
                        foreach (dmColumnProperty item in prop)
                        {
                            if (item.ColumnName == item.DisplayName) continue;
                            if (string.IsNullOrEmpty(item.DisplayName)) continue;
                            if (item.DisplayName == "屬性名稱") continue;

                            var model = propName.repo.ReadSingle(m => m.PropName == item.ColumnName);
                            if (model == null)
                            {
                                int_count++;
                                PropertyNames propertyNames = new PropertyNames();
                                propertyNames.PropName = item.ColumnName;
                                propertyNames.DisplayName = item.DisplayName;
                                propertyNames.Remark = "";
                                propName.repo.Create(propertyNames);
                                propName.repo.SaveChanges();
                            }
                            //else
                            //{
                            //    if (string.IsNullOrEmpty(model.DisplayName))
                            //    {
                            //        model.DisplayName = item.DisplayName;
                            //        model.Remark = "";
                            //        propName.repo.Update(model);
                            //        propName.repo.SaveChanges();
                            //    }
                            //}
                        }
                    }
                }
                return int_count;
            }
        }
    }


    #endregion
}

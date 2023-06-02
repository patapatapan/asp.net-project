using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// 檢查欄位編號唯一值驗證
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class UniqueAttribute : ValidationAttribute
{
    /// <summary>
    /// 表格名稱
    /// </summary>
    public string TableName { get; set; }
    /// <summary>
    /// 主鍵欄位名稱
    /// </summary>
    public string KeyName { get; set; }
    /// <summary>
    /// 編號欄位名稱
    /// </summary>
    public string NoName { get; set; }

    /// <summary>
    /// 檢查欄位編號唯一值驗證
    /// </summary>
    /// <param name="tableName">表格名稱</param>
    /// <param name="keyName">主鍵欄位名稱</param>
    /// <param name="noName">編號欄位名稱</param>
    public UniqueAttribute(string tableName, string keyName, string noName)
    {
        TableName = tableName;
        KeyName = keyName;
        NoName = noName;
    }

    /// <summary>
    /// 檢查欄位編號唯一值驗證
    /// </summary>
    /// <param name="value">輸入值</param>
    /// <returns></returns>
    public override bool IsValid(object value)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_value = (value == null) ? "" : value.ToString();
            return dp.NoUnique(TableName, KeyName, NoName, str_value);
        }
    }
}
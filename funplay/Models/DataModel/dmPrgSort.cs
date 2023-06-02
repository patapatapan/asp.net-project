using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 程式排序
/// </summary>
public class dmPrgSort
{
    /// <summary>
    /// 程式編號
    /// </summary>
    public string SortNo { get; set; }
    /// <summary>
    /// 欄位名稱
    /// </summary>
    public string ColumnName { get; set; }
    /// <summary>
    /// 排序方向
    /// </summary>
    public enSortDirection SortDirection { get; set; }
    /// <summary>
    /// 預設欄位名稱
    /// </summary>
    public string DefaultColumnName { get; set; }
    /// <summary>
    /// 預設排序方向
    /// </summary>
    public enSortDirection DefaultSortDirection { get; set; }
}
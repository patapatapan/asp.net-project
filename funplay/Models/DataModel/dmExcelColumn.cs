using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Closedxml 套件設定欄位資訊用
/// </summary>
public class dmExcelColumn
{
    /// <summary>
    /// 欄位名稱
    /// </summary>
    public string Name { get; set; } = "";
    /// <summary>
    /// 欄位標題
    /// </summary>
    public string Caption { get; set; } = "";
    /// <summary>
    /// 欄位寛度
    /// </summary>
    public int Width { get; set; } = 10;
    /// <summary>
    /// 水平對齊方向
    /// </summary>
    public XLAlignmentHorizontalValues HorizontalAlignment { get; set; } = XLAlignmentHorizontalValues.Left;
    /// <summary>
    /// 垂直對齊方向
    /// </summary>
    public XLAlignmentVerticalValues VerticalAlignment { get; set; } = XLAlignmentVerticalValues.Center;
}
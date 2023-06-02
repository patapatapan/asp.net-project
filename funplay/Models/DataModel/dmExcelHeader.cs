using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class dmExcelHeader
{
    public int RowIndex { get; set; } = 0;
    public string Title { get; set; } = "";
    public int FontSize { get; set; } = 18;
    public bool FontBold { get; set; } = true;
    public int RowHeight { get; set; } = 30;
    public XLAlignmentHorizontalValues HorizontalAlignment { get; set; } = XLAlignmentHorizontalValues.Center;
    public XLAlignmentVerticalValues VerticalAlignment { get; set; } = XLAlignmentVerticalValues.Center;
}
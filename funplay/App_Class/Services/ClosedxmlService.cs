using ClosedXML.Excel;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Web;

/// <summary>
/// Closedxml 服務類別
/// </summary>
public class ClosedxmlService : BaseClass
{
    /// <summary>
    /// 最大文字
    /// </summary>
    public string StringMax { get { return "xxxxxxxxxx"; } }
    /// <summary>
    /// Excel 上傳路徑
    /// </summary>
    public string ExcelPath { get; set; }
    /// <summary>
    /// 建構子,預設Excel 上傳路徑 = ~/Files/Excel
    /// </summary>
    public ClosedxmlService()
    {
        ExcelPath = "~/Files/Excel";
    }
    /// <summary>
    /// 建構子 
    /// </summary>
    /// <param name="excelPath">Excel 上傳路徑</param>
    public ClosedxmlService(string excelPath)
    {
        ExcelPath = excelPath;
    }

    /// <summary>
    /// 取得動態資料欄位值
    /// </summary>
    /// <param name="item">動態資料列</param>
    /// <param name="columnName">欄位名稱</param>
    /// <returns></returns>
    public object GetDynamicItemValue(dynamic item, string columnName)
    {
        var dprow = (item as IDictionary<string, object>);
        var value = dprow.Where(m => m.Key == columnName).FirstOrDefault().Value;
        return (value == null) ? "" : value;
    }

    /// <summary>
    /// 建立新 Excel 檔
    /// </summary>
    /// <returns></returns>
    public XLWorkbook CreateNewFile()
    {
        return CreateNewFile("Sheet1");
    }

    /// <summary>
    /// 建立新 Excel 檔
    /// </summary>
    /// <param name="sheetName">Sheet 名稱</param>
    /// <returns></returns>
    public XLWorkbook CreateNewFile(string sheetName)
    {
        var wb = new XLWorkbook();
        IXLWorksheet ws = wb.Worksheets.Add(sheetName);
        return wb;
    }

    /// <summary>
    /// 儲存 Excel 檔
    /// </summary>
    /// <param name="excelWorkbook">Closedxml 物件</param>
    public void SaveFile(XLWorkbook excelWorkbook)
    {
        string str_path_name = "~/Files/Excel";
        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Files"))) Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files"));
        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/Excel"))) Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files/Excel"));
        string str_file_name = DateTime.Now.ToString("yyyyMMddHHmmssff");
        SaveFile(excelWorkbook, str_path_name, str_file_name);
    }

    /// <summary>
    /// 儲存 Excel 檔
    /// </summary>
    /// <param name="excelWorkbook">Closedxml 物件</param>
    /// <param name="pathName">路徑名稱 (例:~/Files/Excel)</param>
    /// <param name="fileName">檔案名稱 (例:abc)</param>
    public void SaveFile(XLWorkbook excelWorkbook, string pathName, string fileName)
    {
        string str_file = $"{pathName}/{fileName}.xlsx";
        string str_file_name = HttpContext.Current.Server.MapPath(str_file);
        excelWorkbook.SaveAs(str_file_name);
    }

    /// <summary>
    /// 下載 Excel 檔
    /// </summary>
    /// <param name="excelWorkbook">Closedxml 物件</param>
    public void DownloadFile(XLWorkbook excelWorkbook)
    {
        string str_file_name = DateTime.Now.ToString("yyyyMMddHHmmssff");
        DownloadFile(excelWorkbook, str_file_name);
    }

    /// <summary>
    /// 下載 Excel 檔
    /// </summary>
    /// <param name="excelWorkbook">Closedxml 物件</param>
    /// <param name="fileName">檔案名稱</param>
    public void DownloadFile(XLWorkbook excelWorkbook, string fileName)
    {
        string str_file_name = $"{fileName}.xlsx";
        string myName = HttpContext.Current.Server.UrlEncode(str_file_name);
        MemoryStream stream = new MemoryStream();
        excelWorkbook.SaveAs(stream);
        stream.Position = 0;

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + myName);
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.BinaryWrite(stream.ToArray());
        HttpContext.Current.Response.End();
    }

    /// <summary>
    /// 加入圖片到 Excel 中
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="filePath">路徑, 如 ~\Images\App\Logo.jpg</param>
    /// <param name="imageName">名稱,  如 Logo</param>
    /// <param name="cellPosition">位置, 如 A1</param>
    public void AddImage(ref IXLWorksheet ws, string filePath, string imageName, string cellPosition)
    {
        //var imagePath = @"~\Images\App\Logo.jpg";
        var imageLocalPath = HttpContext.Current.Server.MapPath(filePath);
        var image = ws.AddPicture(imageLocalPath)
            .MoveTo(ws.Cell(cellPosition))
            .Scale(1);
        image.Name = imageName;
        image.ScaleWidth(1);
        image.ScaleHeight(1);
    }

    /// <summary>
    /// 取得條件列表值
    /// </summary>
    /// <param name="data1">條件1值</param>
    /// <returns></returns>
    public string GetCondition(string data1)
    {
        if (string.IsNullOrEmpty(data1)) return "空白";
        if (data1 == StringMax) return "全部";
        return data1;
    }

    /// <summary>
    /// 取得條件列表值
    /// </summary>
    /// <param name="data1">條件1值</param>
    /// <param name="data2">條件2值</param>
    /// <returns></returns>
    public string GetCondition(string data1, string data2)
    {
        if (string.IsNullOrEmpty(data1) && data2 == StringMax) return "全部";
        string str_value = (string.IsNullOrEmpty(data1)) ? "空白" : data1;
        if (string.IsNullOrEmpty(data2))
        {
            str_value += " - ";
            str_value += (string.IsNullOrEmpty(data2)) ? "空白" : data2;
            str_value += (data2 == StringMax) ? "全部" : data2;
        }
        return str_value;
    }

    /// <summary>
    /// 設定 Excel 標題
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="rowIndex">第幾列</param>
    /// <param name="colSpan">合併左右欄位數</param>
    /// <param name="headerName">標題名稱</param>
    public void SetHeader(ref IXLWorksheet ws, int rowIndex, int colSpan, string headerName)
    {
        ws.Range(rowIndex, 1, rowIndex, colSpan).Merge();
        ws.Cell(rowIndex, 1).Value = headerName;
        ws.Cell(rowIndex, 1).Style.Font.SetFontSize(18);
        ws.Cell(rowIndex, 1).Style.Font.SetBold();
        ws.Cell(rowIndex, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        ws.Row(rowIndex).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        ws.Row(rowIndex).Height = 30;
    }

    /// <summary>
    /// 設定 Excel 標題
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="rowIndex">第幾列</param>
    /// <param name="colSpan">合併左右欄位數</param>
    /// <param name="header">標題資訊</param>
    public void SetHeader(ref IXLWorksheet ws, int rowIndex, int colSpan, dmExcelHeader header)
    {
        ws.Range(rowIndex, 1, rowIndex, colSpan).Merge();
        ws.Cell(rowIndex, 1).Value = header.Title;
        ws.Cell(rowIndex, 1).Style.Font.SetFontSize(header.FontSize);
        if (header.FontBold) ws.Cell(rowIndex, 1).Style.Font.SetBold();
        ws.Cell(rowIndex, 1).Style.Alignment.Horizontal = header.HorizontalAlignment;
        ws.Row(rowIndex).Style.Alignment.Vertical = header.VerticalAlignment;
        ws.Row(rowIndex).Height = header.RowHeight;
    }

    /// <summary>
    /// 設定 Excel 條件列
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="rowIndex">第幾列</param>
    /// <param name="columns">合併欄位數</param>
    /// <param name="textValue">條件列文字</param>
    public void SetCondition(ref IXLWorksheet ws, int rowIndex, int columns, string textValue)
    {
        ws.Range(rowIndex, 1, rowIndex, columns).Merge();
        ws.Cell(rowIndex, 1).Value = textValue;
        ws.Cell(rowIndex, 1).Style.Font.SetBold();
        ws.Cell(rowIndex, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
    }

    /// <summary>
    /// 設定 Cell 儲存格合併
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="y1">Y1</param>
    /// <param name="x1">X1</param>
    /// <param name="y2">Y2</param>
    /// <param name="x2">X2</param>
    public void SetCellMerge(ref IXLWorksheet ws, int y1, int x1, int y2, int x2)
    {
        ws.Range(y1, x1, y2, x2).Merge();
    }

    /// <summary>
    /// 設定 Excel 條件列
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="rowIndex">第幾列</param>
    /// <param name="columns">合併欄位數</param>
    /// <param name="textValue">條件列文字</param>
    public void SetCondition(ref IXLWorksheet ws, int rowIndex, int columns, dmExcelHeader header)
    {
        ws.Range(rowIndex, 1, rowIndex, columns).Merge();
        ws.Cell(rowIndex, 1).Value = header.Title;
        if (header.FontBold) ws.Cell(rowIndex, 1).Style.Font.SetBold();
        ws.Cell(rowIndex, 1).Style.Alignment.Horizontal = header.HorizontalAlignment;
        ws.Cell(rowIndex, 1).Style.Alignment.Vertical = header.VerticalAlignment;
    }

    /// <summary>
    /// 設定標題列
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="rowIndex">第幾列</param>
    /// <param name="captions">標題列名稱</param>
    /// <param name="widths">標題列欄位寛度</param>
    public void SetCaption(ref IXLWorksheet ws, int rowIndex, List<string> captions, List<int> widths)
    {
        for (int i = 0; i < widths.Count; i++)
        {
            ws.Column(i + 1).Width = widths[i];
        }
        for (int i = 0; i < captions.Count; i++)
        {
            ws.Cell(rowIndex, (i + 1)).Value = captions[i];
        }
    }

    /// <summary>
    /// 設定標題列
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="rowIndex">第幾列</param>
    /// <param name="columns">欄位陣列</param>
    public void SetCaption(ref IXLWorksheet ws, int rowIndex, List<dmExcelColumn> columns)
    {
        int index = 0;
        foreach (dmExcelColumn column in columns)
        {
            index++;
            ws.Column(index).Width = column.Width;
            ws.Cell(rowIndex, index).Value = column.Caption;
            ws.Cell(rowIndex, index).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }
    }

    /// <summary>
    /// 設定標題列
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="rowIndex">第幾列</param>
    /// <param name="captions">標題列名稱</param>
    public void SetCaption(ref IXLWorksheet ws, int rowIndex, List<string> captions)
    {
        for (int i = 0; i < captions.Count; i++)
        {
            ws.Cell(rowIndex, (i + 1)).Value = captions[i];
        }
    }

    /// <summary>
    /// 設定框線
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="y1">y1</param>
    /// <param name="x1">x1</param>
    /// <param name="y2">y2</param>
    /// <param name="x2">x2</param>
    public void SetBorder(ref IXLWorksheet ws, int y1, int x1, int y2, int x2)
    {
        ws.Range(y1, x1, y2, x2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        ws.Range(y1, x1, y2, x2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        ws.Range(y1, x1, y2, x2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        ws.Range(y1, x1, y2, x2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    }

    /// <summary>
    /// 設定資料值
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="rowIndex">第幾列</param>
    /// <param name="item">欄位</param>
    /// <param name="columns">欄位陣列</param>
    public void SetCellValue(ref IXLWorksheet ws, int rowIndex, dynamic item, List<dmExcelColumn> columns)
    {
        int index = 0;
        foreach (dmExcelColumn column in columns)
        {
            index++;
            ws.Cell(rowIndex, index).Value = GetDynamicItemValue(item, column.Name);
            ws.Cell(rowIndex, index).Style.Alignment.Horizontal = column.HorizontalAlignment;
        }
    }

    /// <summary>
    /// 設定Cell 儲存格資料值
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="y1">第幾列</param>
    /// <param name="x1">第幾欄</param>
    /// <param name="value">資料值</param>
    public void SetCellValue(ref IXLWorksheet ws, int y1, int x1, string value)
    {
        ws.Cell(y1, x1).Value = value;
    }

    /// <summary>
    /// 設定Cell 儲存格對齊方式
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="y1">第幾列</param>
    /// <param name="x1">第幾欄</param>
    /// <param name="hAlign">水平對齊方式</param>
    /// <param name="vAlign">垂直對齊方式</param>
    public void SetCellAlignment(ref IXLWorksheet ws, int y1, int x1, XLAlignmentHorizontalValues hAlign, XLAlignmentVerticalValues vAlign)
    {
        ws.Cell(y1, x1).Style.Alignment.Horizontal = hAlign;
        ws.Cell(y1, x1).Style.Alignment.Vertical = vAlign;
    }

    /// <summary>
    /// 設定Cell 儲存格對齊方式
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="y1">第幾列</param>
    /// <param name="x1">開始欄</param>
    /// <param name="x2">結束欄</param>
    /// <param name="step">每次增加數</param>
    /// <param name="hAlign">水平對齊方式</param>
    /// <param name="vAlign">垂直對齊方式</param>
    public void SetCellAlignment(ref IXLWorksheet ws, int y1, int x1, int x2, int step, XLAlignmentHorizontalValues hAlign, XLAlignmentVerticalValues vAlign)
    {
        for (int i = x1; i <= x2; i += step)
        {
            ws.Cell(y1, i).Style.Alignment.Horizontal = hAlign;
            ws.Cell(y1, i).Style.Alignment.Vertical = vAlign;
        }
    }

    /// <summary>
    /// 設定 Cell 儲存格文字角度
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="y1">第幾列</param>
    /// <param name="x1">第幾欄</param>
    /// <param name="angle">角度</param>
    public void SetCellAngle(ref IXLWorksheet ws, int y1, int x1, int angle)
    {
        ws.Cell(y1, x1).Style.Alignment.SetTextRotation(angle);
    }

    /// <summary>
    /// 設定 Cell 儲存格文字粗體
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="y1">第幾列</param>
    /// <param name="x1">第幾欄</param>
    public void SetCellBold(ref IXLWorksheet ws, int y1, int x1)
    {
        ws.Cell(y1, x1).Style.Font.SetBold();
    }

    /// <summary>
    /// 連續設定 Cell 儲存格文字粗體
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="y1">第幾列</param>
    /// <param name="x1">開始欄</param>
    /// <param name="x2">結束欄</param>
    /// <param name="step">每次增加數</param>
    public void SetCellBold(ref IXLWorksheet ws, int y1, int x1, int x2, int step)
    {
        for (int i = x1; i <= x2; i += step)
        {
            ws.Cell(y1, i).Style.Font.SetBold();
        }
    }

    /// <summary>
    /// 設定 row 列高
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="row">row 列</param>
    /// <param name="height">高度</param>
    public void SetRowHeight(ref IXLWorksheet ws, int row, int height)
    {
        ws.Row(row).Height = height;
    }

    /// <summary>
    /// 設定 Cell 儲存格字型大小
    /// </summary>
    /// <param name="ws">WorkSheet 物件</param>
    /// <param name="y1">Y1</param>
    /// <param name="x1">X1</param>
    /// <param name="size">字型大小</param>
    public void SetFontSize(ref IXLWorksheet ws, int y1, int x1, int size)
    {
        ws.Cell(y1, x1).Style.Font.SetFontSize(size);
    }

    public void Sample()
    {
        //using ClosedXML.Excel;
        //using Dapper;
        //using DevExpress.XtraCharts.Native;
        //using DocumentFormat.OpenXml.EMMA;
        //using DocumentFormat.OpenXml.Spreadsheet;
        //using DocumentFormat.OpenXml.Wordprocessing;
        //using sunrayweb.Models;
        //using System;
        //using System.Collections.Generic;
        //using System.Linq;
        //using System.Web;
        //using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

        //public partial class ExportExcels : BaseClass
        //{
        //    public string PURX001(qmStockDetail model)
        //    {
        //        using (ClosedxmlService cs = new ClosedxmlService())
        //        {
        //            if (string.IsNullOrEmpty(model.StockNo1)) model.StockNo1 = "";
        //            if (string.IsNullOrEmpty(model.StockNo2)) model.StockNo2 = cs.StringMax;
        //            if (string.IsNullOrEmpty(model.ProdNo1)) model.ProdNo1 = "";
        //            if (string.IsNullOrEmpty(model.ProdNo2)) model.ProdNo2 = cs.StringMax;
        //            var result = GetModelList(model);
        //            if (result == null || result.Count == 0) return "無資料可匯出!!";
        //            return ExportToExcel(model, result);
        //        }
        //    }

        //    private string ExportToExcel(qmStockDetail model, List<dynamic> result)
        //    {
        //        using (ClosedxmlService cs = new ClosedxmlService())
        //        {
        //            int int_row = 1;
        //            var wb = cs.CreateNewFile("出廠衣物明細表");
        //            var ws = wb.Worksheets.First();

        //            //設定報表標題
        //            int_row = 1;
        //            cs.SetHeader(ref ws, int_row, 10, "尚磊科技股份有限公司");

        //            //設定報表名稱
        //            int_row++;
        //            cs.SetHeader(ref ws, int_row, 10, "出廠衣物明細表");

        //            //設定條件列
        //            string str_cust_name = cs.GetDynamicItemValue(result[0], "cust_name");
        //            string str_text = $"客戶編號：{model.CustNo1}  客戶名稱：{str_cust_name}";
        //            int_row++;
        //            cs.SetCondition(ref ws, int_row, 10, str_text);
        //            str_text = $"進貨日期：{model.StockDate1.ToString("yyyy/MM/dd")} - {model.StockDate2.ToString("yyyy/MM/dd")}     ";
        //            str_text += $"進貨單號：{cs.GetCondition(model.StockNo1, model.StockNo2)}     ";
        //            str_text += $"條碼編號：{cs.GetCondition(model.ProdNo1, model.ProdNo2)} ";
        //            int_row++;
        //            cs.SetCondition(ref ws, int_row, 10, str_text);

        //            //設定欄位標題
        //            List<string> captionList = new List<string>()
        //            {
        //                "衣物條碼" , "部門名稱" , "工號" , "姓名" , "衣物尺寸" , "累計清洗次數" ,
        //                "進貨日期" , "出貨日期" , "衣物種類" , "顏色"
        //            };
        //            List<int> widthList = new List<int>()
        //            {
        //                15 , 10 , 10 , 10 , 10 , 14 ,
        //                10 , 20 , 10, 10
        //            };
        //            //設定欄位對齊方式(1 = 左 , 2 = 右)
        //            List<int> alignList = new List<int>()
        //            {
        //                1,1,1,1,1,2,
        //                1,1,1,1
        //            };

        //            int_row++;
        //            cs.SetCaption(ref ws, int_row, captionList, widthList);

        //            int int_y1 = int_row;
        //            int int_x1 = 1;
        //            int int_y2 = int_y1;
        //            int int_x2 = 10;
        //            //設定資料列
        //            foreach (var item in result)
        //            {
        //                int_row++;
        //                ws.Cell(int_row, 1).Value = cs.GetDynamicItemValue(item, "prod_no");
        //                ws.Cell(int_row, 2).Value = cs.GetDynamicItemValue(item, "dept_name");
        //                ws.Cell(int_row, 3).Value = cs.GetDynamicItemValue(item, "emp_no");
        //                ws.Cell(int_row, 4).Value = cs.GetDynamicItemValue(item, "emp_name");
        //                ws.Cell(int_row, 5).Value = cs.GetDynamicItemValue(item, "cabinet_no");
        //                ws.Cell(int_row, 6).Value = cs.GetDynamicItemValue(item, "wash_count");
        //                ws.Cell(int_row, 7).Value = cs.GetDynamicItemValue(item, "sheet_date");
        //                ws.Cell(int_row, 8).Value = cs.GetDynamicItemValue(item, "date_checkout");
        //                ws.Cell(int_row, 9).Value = cs.GetDynamicItemValue(item, "category_name");
        //                ws.Cell(int_row, 10).Value = cs.GetDynamicItemValue(item, "color_name");

        //                //設定對齊方式
        //                for (int i = 0; i < alignList.Count; i++)
        //                {
        //                    ws.Cell(int_row, (i + 1)).Style.Alignment.Horizontal = (alignList[i] == 1) ? XLAlignmentHorizontalValues.Left : XLAlignmentHorizontalValues.Right;
        //                }
        //            }

        //            //設定框線
        //            int_y2 = int_row;
        //            cs.SetBorder(ref ws, int_y1, int_x1, int_y2, int_x2);

        //            // 自適應欄寬
        //            //ws.Columns().AdjustToContents();
        //            cs.DownloadFile(wb);
        //            return "匯出成功!!";
        //        }
        //    }

        //    private List<dynamic> GetModelList(qmStockDetail model)
        //    {
        //        using (DapperRepository dp = new DapperRepository())
        //        {
        //            string str_query = @"
        //SELECT 
        //z_pur_stocked.cust_no, z_pur_stocked.cust_name, z_pur_stocked_detail.prod_no, 
        //z_pur_stocked_detail.dept_name, z_pur_stocked_detail.emp_no, z_pur_stocked_detail.emp_name, 
        //z_pur_stocked_detail.size_no, z_pur_stocked_detail.cabinet_no,z_pur_stocked.sheet_date ,
        //z_pur_stocked_detail.date_checkout, 
        //z_pur_stocked_detail.category_name ,z_pur_stocked_detail.color_name , 
        //dbo.fn_get_wash_count(z_pur_stocked_detail.prod_no , z_pur_stocked_detail.date_checkout) AS wash_count 
        //FROM z_pur_stocked 
        //INNER JOIN z_pur_stocked_detail ON z_pur_stocked.rowid = z_pur_stocked_detail.parentid 
        //LEFT OUTER JOIN z_bas_product ON z_pur_stocked_detail.prod_no = z_bas_product.prod_no 
        //WHERE z_pur_stocked.cust_no = @cust_no1 AND 
        //z_pur_stocked.sheet_no BETWEEN @sheet_no1 AND @sheet_no2 AND 
        //z_pur_stocked_detail.prod_no BETWEEN @prod_no1 AND @prod_no2 AND 
        //z_pur_stocked.sheet_date BETWEEN @sheet_date1 AND @sheet_date2 
        //ORDER BY z_pur_stocked.sheet_no , z_pur_stocked_detail.dept_name, z_pur_stocked_detail.user_no
        //";
        //            string stock_date1 = model.StockDate1.ToString("yyyy-MM-dd");
        //            string stock_date2 = model.StockDate2.ToString("yyyy-MM-dd");

        //            DynamicParameters parm = new DynamicParameters();
        //            parm.Add("cust_no1", model.CustNo1);
        //            parm.Add("sheet_date1", stock_date1);
        //            parm.Add("sheet_date2", stock_date2);
        //            parm.Add("sheet_no1", model.StockNo1);
        //            parm.Add("sheet_no2", model.StockNo2);
        //            parm.Add("prod_no1", model.ProdNo1);
        //            parm.Add("prod_no2", model.ProdNo2);
        //            return dp.ReadAll(str_query, parm);
        //        }
        //    }
        //}
    }
}
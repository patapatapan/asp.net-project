using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 報表檔案列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> ReportFileList()
    {
        var data = new List<SelectListItem>();
        string path = "~/Reports";
        string reportPath = HttpContext.Current.Server.MapPath(path);
        var files = Directory.GetFiles(reportPath, "*.cs", SearchOption.AllDirectories).ToList();
        foreach (var item in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(item);
            data.Add(new SelectListItem() { Value = fileName, Text = fileName });
        }
        return data;
    }
}
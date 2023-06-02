using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class dmFileUpload
{
    public string AreaName { get; set; }
    public string ControllerName { get; set; }
    public string ActionName { get; set; }
    public int Page { get; set; }
    public enPriorParmIdType ParmType { get; set; }
    public int IdIntParm { get; set; }
    public string IdStringParm { get; set; }
    public HttpPostedFileBase FileName { get; set; }
}
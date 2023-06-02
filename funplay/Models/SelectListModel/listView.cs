using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 性別列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> ViewList()
    {
        var data = new List<SelectListItem>()
        {
            new SelectListItem() { Value = "Index" , Text = "Index"},
            new SelectListItem() { Value = "Create" , Text = "Create"},
            new SelectListItem() { Value = "CreateEdit" , Text = "CreateEdit"},
            new SelectListItem() { Value = "Edit" , Text = "Edit"},
            new SelectListItem() { Value = "Delete" , Text = "Delete"},
            new SelectListItem() { Value = "Detail" , Text = "Detail"},
            new SelectListItem() { Value = "Upload" , Text = "Upload"},
            new SelectListItem() { Value = "UploadImage" , Text = "UploadImage"},
            new SelectListItem() { Value = "UploadFile" , Text = "UploadFile"},
            new SelectListItem() { Value = "Login" , Text = "Login"},
            new SelectListItem() { Value = "Scheduler" , Text = "Scheduler"},
            new SelectListItem() { Value = "Message" , Text = "Message"}
        };
        return data;
    }
}
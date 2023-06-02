using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// View 範本列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> TemplateList()
    {
        var data = new List<SelectListItem>()
        {
            new SelectListItem() { Value = "Index" , Text = "Index"},
            new SelectListItem() { Value = "CreateEdit1" , Text = "CreateEdit1"},
            new SelectListItem() { Value = "CreateEdit2" , Text = "CreateEdit2"},
            new SelectListItem() { Value = "CreateEdit3" , Text = "CreateEdit3"},
            new SelectListItem() { Value = "Detail1" , Text = "Detail1"},
            new SelectListItem() { Value = "Detail2" , Text = "Detail2"},
            new SelectListItem() { Value = "Detail3" , Text = "Detail3"}
        };
        return data;
    }
}
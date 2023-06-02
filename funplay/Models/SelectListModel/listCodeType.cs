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
    public List<SelectListItem> CodeTypeList()
    {
        var data = new List<SelectListItem>()
        {
            new SelectListItem() { Value = "A" , Text = "模型 (Metadata , Repository)"},
            new SelectListItem() { Value = "B" , Text = "控制器及視圖 (Controller , View)"}
        };
        return data;
    }
}
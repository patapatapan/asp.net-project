using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 區域列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> AreaList()
    {
        using (CodeBase code = new CodeBase())
        {
            List<string> lists = code.GetAreaList();
            var data = lists.Select(u => new SelectListItem
            {
                Text = u,
                Value = u
            }).ToList();
            return data;
        }
    }
}

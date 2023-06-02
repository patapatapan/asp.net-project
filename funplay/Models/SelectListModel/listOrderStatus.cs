using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 訂單狀態列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> OrderStatusList()
    {
        using (z_repoOrdersStatus model = new z_repoOrdersStatus())
        {
            var data = model.repo.ReadAll()
                .OrderBy(m => m.StatusNo)
                .Select(u => new SelectListItem
                {
                    Text = u.StatusNo + " " + u.StatusName,
                    Value = u.StatusNo
                }).ToList();
            return data;
        }
    }
}

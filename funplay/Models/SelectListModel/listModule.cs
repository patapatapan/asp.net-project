using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 模組列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> ModuleList()
    {
        using (z_repoModules model = new z_repoModules())
        {
            var data = model.repo.ReadAll()
                .OrderBy(m => m.ModuleNo)
                .Select(u => new SelectListItem
                {
                    Text = u.ModuleNo + " " + u.ModuleName,
                    Value = u.ModuleNo
                }).ToList();
            return data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 部門列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> TitleList()
    {
        using (z_repoTitles model = new z_repoTitles())
        {
            var data = model.repo.ReadAll()
                .OrderBy(m => m.TitleNo)
                .Select(u => new SelectListItem
                {
                    Text = u.TitleNo + " " + u.TitleName,
                    Value = u.TitleNo
                }).ToList();
            return data;
        }
    }
}

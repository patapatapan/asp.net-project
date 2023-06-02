using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 基本主檔列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> BaseCodeList()
    {
        using (z_repoCodeBases model = new z_repoCodeBases())
        {
            var data = model.repo.ReadAll()
                .OrderBy(m => m.BaseNo)
                .Select(u => new SelectListItem
                {
                    Text = u.BaseNo + " " + u.BaseName,
                    Value = u.BaseNo
                }).ToList();
            return data;
        }
    }
}

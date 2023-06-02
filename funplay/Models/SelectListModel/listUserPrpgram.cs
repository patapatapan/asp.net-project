using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 員工程式列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> UserProgramList()
    {
        using (z_repoPrograms model = new z_repoPrograms())
        {
            var data = model.repo.ReadAll(m =>
                m.RoleNo == "User" && m.IsEnabled == true && m.CodeNo != "M")
                .OrderBy(m => m.PrgNo)
                .Select(u => new SelectListItem
                {
                    Text = u.PrgNo + " " + u.PrgName,
                    Value = u.PrgNo
                }).ToList();
            return data;
        }
    }
}
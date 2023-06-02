using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 員工列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> UserList()
    {
        using (z_repoUsers model = new z_repoUsers())
        {
            var data = model.repo.ReadAll(m => m.RoleNo == "User")
                .OrderBy(m => m.UserNo)
                .Select(u => new SelectListItem
                {
                    Text = u.UserNo + " " + u.UserName,
                    Value = u.UserNo
                }).ToList();
            return data;
        }
    }
}
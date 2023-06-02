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
    public List<SelectListItem> DepartmentList()
    {
        using (z_repoDepartments model = new z_repoDepartments())
        {
            var data = model.repo.ReadAll()
                .OrderBy(m => m.DeptNo)
                .Select(u => new SelectListItem
                {
                    Text = u.DeptNo + " " + u.DeptName,
                    Value = u.DeptNo
                }).ToList();
            return data;
        }
    }
}

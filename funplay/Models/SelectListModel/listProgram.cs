using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 程式列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> ProgramList()
    {
        using (z_repoPrograms model = new z_repoPrograms())
        {
            var data = model.repo.ReadAll()
                .OrderBy(m => m.PrgNo)
                .Select(u => new SelectListItem
                {
                    Text = u.PrgNo + " " + u.PrgName,
                    Value = u.PrgNo
                }).ToList();
            return data;
        }
    }

    /// <summary>
    /// 程式列表(只有未建立的程式)
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> ProgramNewList()
    {
        using (CodeBase code = new CodeBase())
        {
            using (z_repoPrograms prg = new z_repoPrograms())
            {
                List<SelectListItem> model = new List<SelectListItem>();
                var prgModel = prg.repo.ReadAll().OrderBy(m => m.PrgNo).ToList();
                foreach (var item in prgModel)
                {
                    if (!code.ControllerFileExists(item.AreaName, item.ControllerName))
                    {
                        string str_text = $"{item.PrgNo} {item.PrgName}";
                        model.Add(new SelectListItem() { Text = str_text, Value = item.PrgNo });
                    }
                }
                return model;
            }
        }
    }
}

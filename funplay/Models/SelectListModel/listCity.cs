using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 縣市列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> CityList()
    {
        using (z_repoCitys model = new z_repoCitys())
        {
            var data = model.repo.ReadAll()
                .OrderBy(m => m.SortNo)
                .ThenBy(m => m.CityName)
                .Select(u => new SelectListItem
                {
                    Text = u.CityName,
                    Value = u.CityName
                }).ToList();
            return data;
        }
    }
}

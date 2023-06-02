using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 縣市區域列表
    /// </summary>
    /// <param name="cityName">縣市名稱</param>
    /// <returns></returns>
    public List<SelectListItem> CityAreaList(string cityName)
    {
        using (z_repoCityAreas model = new z_repoCityAreas())
        {
            var data = model.repo.ReadAll()
                .Where(x => x.CityName == cityName)
                .OrderBy(m => m.AreaName)
                .Select(u => new SelectListItem
                {
                    Text = u.AreaName,
                    Value = u.AreaName
                }).ToList();
            return data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 性別列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> GenderList()
    {
        using (z_repoCodeDatas data = new z_repoCodeDatas())
        {
            return data.GetBaseDataList("Gender");
        }
    }
}
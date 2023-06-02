using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 基本主檔資料列表
    /// </summary>
    /// <param name="baseNo">主檔代號</param>
    /// <returns></returns>
    public List<SelectListItem> BaseDataList(string baseNo)
    {
        using (z_repoCodeDatas model = new z_repoCodeDatas())
        {
            var data = model.repo.ReadAll(m => m.BaseNo == baseNo)
                .OrderBy(m => m.SortNo)
                .ThenBy(m => m.CodeNo)
                .Select(u => new SelectListItem
                {
                    Text = u.CodeNo + " " + u.CodeName,
                    Value = u.CodeNo
                }).ToList();
            return (List<SelectListItem>)data;
        }
    }
}

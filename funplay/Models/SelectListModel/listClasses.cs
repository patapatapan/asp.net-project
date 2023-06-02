using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 類別列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> ClassAllList()
    {
        using (CodeBase code = new CodeBase())
        {
            List<string> lists = code.NameSpaceClasses();
            var data = lists.Select(u => new SelectListItem
            {
                Text = u,
                Value = u
            }).OrderBy(m => m.Value).ToList();
            data.Insert(0, new SelectListItem() { Text = "請選擇", Value = "" });
            return data;
        }
    }
    public List<SelectListItem> ClassNewMetadataList()
    {
        using (CodeBase code = new CodeBase())
        {
            List<SelectListItem> data = new List<SelectListItem>();
            List<SelectListItem> lists = ClassAllList();
            foreach (var item in lists)
            {
                if (!code.MetaFileExists(item.Value))
                {
                    if (!item.Value.Contains("Entities"))
                    {
                        data.Add(new SelectListItem() { Text = item.Text, Value = item.Value });
                    }
                }
            }
            data.Insert(0, new SelectListItem() { Text = "請選擇", Value = "" });
            return data;
        }
    }

    public List<SelectListItem> ClassNewRepositoryList()
    {
        using (CodeBase code = new CodeBase())
        {
            List<SelectListItem> data = new List<SelectListItem>();
            List<SelectListItem> lists = ClassAllList();
            foreach (var item in lists)
            {
                if (!code.RepoFileExists(item.Value))
                {
                    if (!item.Value.Contains("Entities"))
                    {
                        data.Add(new SelectListItem() { Text = item.Text, Value = item.Value });
                    }
                }
            }
            data.Insert(0, new SelectListItem() { Text = "請選擇", Value = "" });
            return data;
        }
    }
}

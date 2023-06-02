using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 序號列表, 如 1 , 5 , 3 = { "001","002","003","004","005"}
    /// </summary>
    /// <param name="startNumber">開始數字</param>
    /// <param name="totalCount">數字筆數</param>
    /// <param name="length">數字長度</param>
    /// <returns></returns>
    public List<SelectListItem> SortNoList(int startNumber, int totalCount, int length)
    {
        List<SelectListItem> data = new List<SelectListItem>();
        for (int i = startNumber; i <= totalCount; i++)
        {
            data.Add(new SelectListItem()
            {
                Text = i.ToString().PadLeft(length, '0'),
                Value = i.ToString().PadLeft(length, '0')
            });
        }
        return data;
    }
}

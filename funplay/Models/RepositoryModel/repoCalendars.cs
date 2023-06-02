using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class z_repoCalendars : BaseClass
{
    public IEFGenericRepository<Calendars> repo;
    public z_repoCalendars()
    {
        repo = new EFGenericRepository<Calendars>(new dbEntities());
    }

    /// <summary>
    /// 取得指定對象類別的對象編號行事曆
    /// </summary>
    /// <param name="targetCode">對象類別</param>
    /// <param name="targetNo">對象編號</param>
    /// <returns></returns>
    public List<dmCalendarEvent> GetTargetCalendar(string targetCode, string targetNo)
    {
        List<dmCalendarEvent> events = new List<dmCalendarEvent>();
        var model = repo
           .ReadAll(m => m.TargetCode == targetCode && m.TargetNo == targetNo)
           .ToList();
        if (model != null)
        {
            foreach (var item in model)
            {
                dmCalendarEvent data = new dmCalendarEvent();
                data.title = item.SubjectName;
                data.id = item.Id;
                data.groupId = 0;
                data.start = DateTime.Parse(item.StartDate.ToString("yyyy-MM-dd") + " " + item.StartTime).ToString("yyyy-MM-dd HH:mm:ss");
                data.end = DateTime.Parse(item.EndDate.ToString("yyyy-MM-dd") + " " + item.EndTime).ToString("yyyy-MM-dd HH:mm:ss");
                data.allDay = item.IsFullday;
                events.Add(data);
            }
        }
        return events;
    }

    /// <summary>
    /// 取得指定id的行事曆
    /// </summary>
    /// <param name="rowId">行程 RowID</param>
    /// <returns></returns>
    public Calendars GetTargetCalendar(int id)
    {
        return repo.ReadSingle(m => m.Id == id);
    }
}
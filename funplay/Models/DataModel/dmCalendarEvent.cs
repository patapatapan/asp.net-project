using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class dmCalendarEvent
{
    public int id { get; set; }
    public int groupId { get; set; }
    public string title { get; set; }
    public string url { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public bool allDay { get; set; }
    public string description { get; set; }
}
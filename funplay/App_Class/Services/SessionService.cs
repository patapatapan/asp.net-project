using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

public static class SessionService
{
    public static int CalendarID { get { return GetIntValue("CalendarID", 0); } set { SetValue("CalendarID", value); } }
    public static int KeyValue { get { return GetIntValue("KeyValue"); } set { SetValue("KeyValue", value); } }
    public static int TagId1 { get { return GetIntValue("TagId1"); } set { SetValue("TagId1", value); } }
    public static int TagId2 { get { return GetIntValue("TagId2"); } set { SetValue("TagId2", value); } }
    public static int TagId3 { get { return GetIntValue("TagId3"); } set { SetValue("TagId3", value); } }
    public static int TagId4 { get { return GetIntValue("TagId4"); } set { SetValue("TagId4", value); } }
    public static int TagId5 { get { return GetIntValue("TagId5"); } set { SetValue("TagId5", value); } }
    public static string TagNo1 { get { return GetValue("TagNo1"); } set { SetValue("TagNo1", value); } }
    public static string TagNo2 { get { return GetValue("TagNo2"); } set { SetValue("TagNo2", value); } }
    public static string TagNo3 { get { return GetValue("TagNo3"); } set { SetValue("TagNo3", value); } }
    public static string TagNo4 { get { return GetValue("TagNo4"); } set { SetValue("TagNo4", value); } }
    public static string TagNo5 { get { return GetValue("TagNo5"); } set { SetValue("TagNo5", value); } }
    public static string TagName1 { get { return GetValue("TagName1"); } set { SetValue("TagName1", value); } }
    public static string TagName2 { get { return GetValue("TagName2"); } set { SetValue("TagName2", value); } }
    public static string TagName3 { get { return GetValue("TagName3"); } set { SetValue("TagName3", value); } }
    public static string TagName4 { get { return GetValue("TagName4"); } set { SetValue("TagName4", value); } }
    public static string TagName5 { get { return GetValue("TagName5"); } set { SetValue("TagName5", value); } }

    public static string GetValue(string sessionName)
    {
        return GetValue(sessionName, "");
    }
    public static string GetValue(string sessionName, string defaultValue)
    {
        object obj_value = HttpContext.Current.Session[sessionName];
        if (obj_value == null) return defaultValue;
        return obj_value.ToString();
    }

    public static bool GetBoolValue(string sessionName)
    {
        return GetBoolValue(sessionName, false);
    }

    public static bool GetBoolValue(string sessionName, bool defaultValue)
    {
        object obj_value = HttpContext.Current.Session[sessionName];
        if (obj_value == null) return defaultValue;
        return (bool)obj_value;
    }

    public static int GetIntValue(string sessionName)
    {
        return GetIntValue(sessionName, 0);
    }

    public static int GetIntValue(string sessionName, int defaultValue)
    {
        object obj_value = HttpContext.Current.Session[sessionName];
        if (obj_value == null) return defaultValue;
        return (int)obj_value;
    }

    public static void SetValue(string sessionName, object value)
    {
        HttpContext.Current.Session[sessionName] = value;
    }

    public static void Init()
    {
        CalendarID = 0;
        KeyValue = 0;
        TagId1 = 0;
        TagId2 = 0;
        TagId3 = 0;
        TagId4 = 0;
        TagId5 = 0;
        TagNo1 = "";
        TagNo2 = "";
        TagNo3 = "";
        TagNo4 = "";
        TagNo5 = "";
        TagName1 = "";
        TagName2 = "";
        TagName3 = "";
        TagName4 = "";
        TagName5 = "";
    }
}
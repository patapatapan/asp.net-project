using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

public class AppSettingsService : BaseClass
{
    public bool GetAppConfigBoolValue(string configName, bool defaultValue)
    {
        object obj_value = WebConfigurationManager.AppSettings[configName];
        if (obj_value == null) return defaultValue;
        string str_value = obj_value.ToString();
        return (str_value == "1");
    }

    public string GetAppConfigStringValue(string configName, string defaultValue)
    {
        object obj_value = WebConfigurationManager.AppSettings[configName];
        if (obj_value == null) return defaultValue;
        string str_value = obj_value.ToString();
        return str_value;
    }


}
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public static class T4Helpers
{
    public static bool IsHidden(string viewDataTypeName, string propertyName)
    {
        bool value = false;
        Type typeModel = Type.GetType(viewDataTypeName);
        if (typeModel != null)
        {
            PropertyInfo pi = typeModel.GetProperty(propertyName);
            Attribute attr = pi.GetCustomAttribute<Attribute>();
            value = attr != null;
        }
        return value;
    }

    public static bool IsRequired(string viewDataTypeName, string propertyName)
    {
        bool isRequired = false;

        Type typeModel = Type.GetType(viewDataTypeName);
        if (typeModel != null)
        {
            PropertyInfo pi = typeModel.GetProperty(propertyName);
            Attribute attr = pi.GetCustomAttribute<RequiredAttribute>();
            isRequired = attr != null;
        }

        return isRequired;
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

public static class DataTableExtensions
{
    public static List<T> ToList<T>(this DataTable table) where T : new()
    {
        IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
        List<T> result = new List<T>();

        foreach (var row in table.Rows)
        {
            var item = CreateItemFromRow<T>((DataRow)row, properties);
            result.Add(item);
        }

        return result;
    }

    public static List<T> ToList<T>(this DataTable table, Dictionary<string, string> mappings) where T : new()
    {
        IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
        List<T> result = new List<T>();
        foreach (var row in table.Rows)
        {
            var item = CreateItemFromRow<T>((DataRow)row, properties, mappings);
            result.Add(item);
        }
        return result;
    }

    private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
    {
        T item = new T();
        string ErrorMessage = "";
        foreach (var property in properties)
        {
            try
            {
                property.SetValue(item, row[property.Name], null);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        return item;
    }

    private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties, Dictionary<string, string> mappings) where T : new()
    {
        T item = new T();
        string ErrorMessage = "";
        foreach (var property in properties)
        {
            if (mappings.ContainsKey(property.Name))
            {
                try
                {
                    property.SetValue(item, row[mappings[property.Name]], null);
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }
            }
        }
        return item;
    }
}
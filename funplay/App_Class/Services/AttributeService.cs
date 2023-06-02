using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// 屬性值管理 Repository
/// </summary>
public class AttributeService : BaseClass
{
    /// <summary>
    /// 取得屬性預設值
    /// </summary>
    /// <typeparam name="T">Metadata 類別</typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public object GetDefaultValue<T>(string propName)
    {
        using (CodeBase code = new CodeBase())
        {
            object obj_value = null;
            PropertyInfo[] myMetadataInfo = typeof(T).GetProperties();
            if (myMetadataInfo != null)
            {
                PropertyInfo metaName = myMetadataInfo.Where(m => m.Name == propName).FirstOrDefault();
                if (metaName != null)
                {
                    DefaultAttribute defaults = (DefaultAttribute)Attribute.GetCustomAttribute(metaName, typeof(DefaultAttribute));
                    if (defaults != null) obj_value = code.GetDefaultValue(defaults.DefaultValueType, defaults.DefaultValue);
                }
            }
            return obj_value;
        }
    }

    /// <summary>
    /// 取得屬性 DropdownList 類別名稱
    /// </summary>
    /// <typeparam name="T">Metadata 類別</typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public string GetDropdownClass<T>(string propName)
    {
        string str_value = "";
        object obj_attribute = TypeDescriptor.GetProperties(typeof(T))[propName];
        if (obj_attribute != null)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[propName].Attributes;
            ColumnAttribute propAttribute = (ColumnAttribute)attributes[typeof(ColumnAttribute)];
            if (propAttribute != null) str_value = propAttribute.DropdownClass;
        }
        return str_value;
    }

    /// <summary>
    /// 取得屬性 Hidden 是否為隱藏欄位
    /// </summary>
    /// <typeparam name="T">Metadata 類別</typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public bool GetHidden<T>(string propName)
    {
        bool bln_value = false;
        object obj_attribute = TypeDescriptor.GetProperties(typeof(T))[propName];
        if (obj_attribute != null)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[propName].Attributes;
            ColumnAttribute propAttribute = (ColumnAttribute)attributes[typeof(ColumnAttribute)];
            if (propAttribute != null) bln_value = propAttribute.Hidden;
        }
        return bln_value;
    }

    /// <summary>
    /// 取得屬性 CheckBox 是否為隱藏欄位
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public bool GetCheckBox<T>(string propName)
    {
        bool bln_value = false;
        object obj_attribute = TypeDescriptor.GetProperties(typeof(T))[propName];
        if (obj_attribute != null)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[propName].Attributes;
            ColumnAttribute propAttribute = (ColumnAttribute)attributes[typeof(ColumnAttribute)];
            if (propAttribute != null) bln_value = propAttribute.CheckBox;
        }
        return bln_value;
    }

    /// 取得顯示的名稱
    /// </summary>
    /// <typeparam name="T">Metadata 類別</typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public string GetDisplayName<T>(string propName)
    {
        string str_value = "";
        object obj_attribute = TypeDescriptor.GetProperties(typeof(T))[propName];
        if (obj_attribute != null)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[propName].Attributes;
            DisplayAttribute propAttribute = (DisplayAttribute)attributes[typeof(DisplayAttribute)];
            if (propAttribute != null) str_value = propAttribute.Name;
        }
        return str_value;
    }

    /// <summary>
    /// 取得屬性是否為主鍵 Key
    /// </summary>
    /// <typeparam name="T">Metadata 類別</typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public bool GetKey<T>(string propName)
    {
        bool bln_value = false;
        object obj_attribute = TypeDescriptor.GetProperties(typeof(T))[propName];
        if (obj_attribute != null)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[propName].Attributes;
            KeyAttribute propAttribute = (KeyAttribute)attributes[typeof(KeyAttribute)];
            if (propAttribute != null) bln_value = true;
        }
        return bln_value;
    }

    /// <summary>
    /// 取得屬性是否為必需輸入欄位 Required
    /// </summary>
    /// <typeparam name="T">Metadata 類別</typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public bool GetRequired<T>(string propName)
    {
        bool bln_value = false;
        object obj_attribute = TypeDescriptor.GetProperties(typeof(T))[propName];
        if (obj_attribute != null)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[propName].Attributes;
            RequiredAttribute propAttribute = (RequiredAttribute)attributes[typeof(RequiredAttribute)];
            if (propAttribute != null) bln_value = true;
        }
        return bln_value;
    }

    /// 取得顯示的名稱
    /// </summary>
    /// <typeparam name="T">Metadata 類別</typeparam>
    /// <param name="propName">屬性名稱</param>
    /// <returns></returns>
    public string GetDataFormatString<T>(string propName)
    {
        string str_value = "";
        object obj_attribute = TypeDescriptor.GetProperties(typeof(T))[propName];
        if (obj_attribute != null)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[propName].Attributes;
            DisplayFormatAttribute propAttribute = (DisplayFormatAttribute)attributes[typeof(DisplayFormatAttribute)];
            if (propAttribute != null) str_value = propAttribute.DataFormatString;
        }
        return str_value;
    }
}
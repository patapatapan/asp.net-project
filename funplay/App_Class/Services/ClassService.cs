using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// 類別服務類別
/// </summary>
public static class ClassService
{
    /// <summary>
    /// 取得屬性預設值
    /// </summary>
    /// <example>
    /// 在屬性中設定
    /// [DefaultValue("U001")]
    /// public string StudentNo { get; set; }
    /// 在程式中設定
    /// StudentNo = ClassService.GetDefaultValue<Student> ("StudentNo").ToString();
    /// </example>
    /// <typeparam name="T">類別</typeparam>
    /// <param name="propertyName">屬性名稱</param>
    /// <returns></returns>
    public static object GetDefaultValue<T>(string propertyName)
    {
        object defaultValue = null;
        Type type = typeof(T);
        AttributeCollection attributes = TypeDescriptor.GetProperties(type)[propertyName].Attributes;
        DefaultValueAttribute myAttribute = (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
        PropertyInfo info = type.GetProperties().Where(x => x.Name == propertyName).FirstOrDefault();
        if (info != null)
        {
            string str_type = info.PropertyType.Name;
            string str_value = myAttribute.Value.ToString();
            if (str_type == "String") defaultValue = str_value;
            if (str_type == "Int32") defaultValue = (int)myAttribute.Value;
            if (str_type == "Decimal") defaultValue = (decimal)myAttribute.Value;
            if (str_type == "Boolean") defaultValue = (bool)myAttribute.Value;
            if (str_type == "DateTime")
            {
                if (str_value == "Today") defaultValue = DateTime.Today;
                if (str_value == "Now") defaultValue = DateTime.Now;
            }
            //if (str_type == "enColor") defaultValue = (enColor)Enum.Parse(typeof(enColor), str_value);
        }
        return defaultValue;
    }
}
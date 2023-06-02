using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;

public class BaseClass : IDisposable
{
    #region 解構子
    private bool disposed = false;
    private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

    /// <summary>
    /// 解構子,實現IDisposable中的Dispose方法
    /// </summary>
    public void Dispose()
    {
        //必須為true
        Dispose(true);
        //通知垃圾回收機制不再調用終端子（析構器）
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// 解構子
    /// </summary>
    /// <param name="disposing">disposing</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;
        //解構時要執行的其它程式
        if (disposing)
        {
            handle.Dispose();
        }
        //讓類別知道自己已經被釋放
        disposed = true;
    }
    /// <summary>
    /// BaseClass 解構子
    /// </summary>
    ~BaseClass()
    {
        //必須為false
        Dispose(false);
    }
    #endregion
    #region 屬性預設值
    /// <summary>
    /// 取得屬性預設值
    /// </summary>
    /// <example>
    /// 在屬性中設定
    /// [DefaultValue(true)]
    /// public bool IsValid { get; set; }
    /// 在建構子中設定
    /// 如 IsValid = (bool)GetDefaultValue("IsValid");
    /// </example>
    /// <param name="propertyName">屬性名稱</param>
    /// <returns></returns>
    public object GetDefaultValue(string propertyName)
    {
        object defaultValue = null;
        Type type = this.GetType();
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
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// 設定欄位預設值屬性
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class DefaultAttribute : Attribute
{
    public enDefaultValueType DefaultValueType { get; set; }
    public string DefaultValue { get; set; } = "";

    /// <summary>
    /// 設定欄位預設值屬性
    /// </summary>
    public DefaultAttribute()
    {
    }
}



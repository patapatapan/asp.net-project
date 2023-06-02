using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// 設定欄位客製化屬性
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ColumnAttribute : Attribute
{
    public bool Hidden { get; set; } = false;
    public bool Readonly { get; set; } = false;
    public bool CheckBox { get; set; } = false;
    public string DropdownClass { get; set; } = "";

    /// <summary>
    /// 欄位屬性設定
    /// </summary>
    public ColumnAttribute()
    {
    }
}



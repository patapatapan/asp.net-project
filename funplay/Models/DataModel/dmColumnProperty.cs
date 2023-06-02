using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class dmColumnProperty
{
    [Display(Name = "類別名稱")]
    [StringLength(50)]
    public string ClassName { get; set; }
    [Display(Name = "欄位名稱")]
    [StringLength(50)]
    public string ColumnName { get; set; }
    [Display(Name = "欄位型別")]
    public string ColumnType { get; set; }
    [Display(Name = "完整型別")]
    public string FullType { get; set; }
    [Display(Name = "允許Null")]
    public string AllowNull { get; set; }
    [Display(Name = "顯示文字")]
    public string DisplayName { get; set; }
    [Display(Name = "欄位格式")]
    public string DataFormat { get; set; }
    [Display(Name = "主索引鍵")]
    public bool IsKeyColumn { get; set; }
    [Display(Name = "欄位隱藏")]
    public bool IsHidden { get; set; }
    [Display(Name = "CheckBox")]
    public bool IsCheckBox { get; set; }
    [Display(Name = "必輸欄位")]
    public bool IsRequired { get; set; }
    [Display(Name = "預設值")]
    public string DefaultValue { get; set; }
    [Display(Name = "下拉選單來源類別")]
    public string DropdownClass { get; set; }

}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaPrograms))]
    public partial class Programs
    {
        [NotMapped]
        [Display(Name = "角色名稱")]
        public string RoleName { get; set; }
        [NotMapped]
        [Display(Name = "類別名稱")]
        public string CodeName { get; set; }
        [NotMapped]
        [Display(Name = "模組名稱")]
        public string ModuleName { get; set; }
        [NotMapped]
        [Display(Name = "人數")]
        public int Users { get; set; }
    }
}

public abstract class z_metaPrograms
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsEnabled { get; set; }
    [Display(Name = "角色")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "User")]
    public string RoleNo { get; set; }
    [Display(Name = "模組代號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ModuleNo { get; set; }
    [Display(Name = "排序")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string SortNo { get; set; }
    [Display(Name = "程式代號")]
    [Required(ErrorMessage = "欄位不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string PrgNo { get; set; }
    [Display(Name = "程式名稱")]
    [Required(ErrorMessage = "欄位不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string PrgName { get; set; }
    [Display(Name = "類別")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string CodeNo { get; set; }
    [Display(Name = "區域")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "User")]
    public string AreaName { get; set; }
    [Display(Name = "控制器")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ControllerName { get; set; }
    [Display(Name = "動作")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "Index")]
    public string ActionName { get; set; }
    [Display(Name = "參數")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ParmValue { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}
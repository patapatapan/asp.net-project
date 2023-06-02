using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaModules))]
    public partial class Modules
    {
        [NotMapped]
        [Display(Name = "角色名稱")]
        public string RoleName { get; set; }
    }
}

public abstract class z_metaModules
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsEnabled { get; set; }
    [Display(Name = "簽核")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_False, DefaultValue = "")]
    public bool IsWorkflow { get; set; }
    [Display(Name = "角色")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string RoleNo { get; set; }
    [Display(Name = "排序")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string SortNo { get; set; }
    [Display(Name = "模組代號")]
    [Required(ErrorMessage = "模組代號不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ModuleNo { get; set; }
    [Display(Name = "模組名稱")]
    [Required(ErrorMessage = "模組名稱不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ModuleName { get; set; }
    [Display(Name = "圖示名稱")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string IconName { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}

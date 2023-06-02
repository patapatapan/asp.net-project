using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaSecuritys))]
    public partial class Securitys
    {
        [NotMapped]
        [Display(Name = "角色名稱")]
        public string RoleName { get; set; }
        [NotMapped]
        [Display(Name = "使用者")]
        public string TargetName { get; set; }
        [NotMapped]
        [Display(Name = "模組名稱")]
        public string ModuleName { get; set; }
        [NotMapped]
        [Display(Name = "程式名稱")]
        public string PrgName { get; set; }
    }
}

public abstract class z_metaSecuritys
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "角色代號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string RoleNo { get; set; }
    [Display(Name = "使用者")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string TargetNo { get; set; }
    [Display(Name = "模組")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ModuleNo { get; set; }
    [Display(Name = "程式代號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string PrgNo { get; set; }
    [Display(Name = "新增")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsAdd { get; set; }
    [Display(Name = "修改")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsEdit { get; set; }
    [Display(Name = "刪除")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsDelete { get; set; }
    [Display(Name = "審核")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsConfirm { get; set; }
    [Display(Name = "解除")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsUndo { get; set; }
    [Display(Name = "作廢")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsInvalid { get; set; }
    [Display(Name = "上傳")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsUpload { get; set; }
    [Display(Name = "下載")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsDownload { get; set; }
    [Display(Name = "列印")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsPrint { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}
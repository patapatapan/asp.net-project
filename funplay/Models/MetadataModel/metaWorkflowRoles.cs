using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaWorkflowRoles))]
    public partial class WorkflowRoles
    {
    }
}

public abstract class z_metaWorkflowRoles
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "角色編號")]
    [Required(ErrorMessage = "不可空白!!")]
    [Unique("WorkflowRoles", "Id", "RoleNo", ErrorMessage = "資料重覆輸入!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string RoleNo { get; set; }
    [Display(Name = "角色名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string RoleName { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}

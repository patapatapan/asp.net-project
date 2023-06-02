using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaOrdersStatus))]
    public partial class OrdersStatus
    {
    }
}

public abstract class z_metaOrdersStatus
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "屬性名稱")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public bool IsPayed { get; set; }
    [Display(Name = "結案")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_False, DefaultValue = "")]
    public bool IsClosed { get; set; }
    [Display(Name = "屬性名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string StatusNo { get; set; }
    [Display(Name = "屬性名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string StatusName { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}

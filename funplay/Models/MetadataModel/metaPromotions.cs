using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaPromotions))]
    public partial class Promotions
    {
    }
}

public abstract class z_metaPromotions
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "排序")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string SortNo { get; set; }
    [Display(Name = "商品編號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ProdNo { get; set; }
    [Display(Name = "開始時間")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Date_Now, DefaultValue = "")]
    public DateTime StartTime { get; set; }
    [Display(Name = "結束時間")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Date_Now, DefaultValue = "")]
    public DateTime EndTime { get; set; }
    [Display(Name = "銷售單價")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public int SalePrice { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaOrderDetails))]
    public partial class OrderDetails
    {
        [NotMapped]
        [Display(Name = "廠商名稱")]
        public string VendorName { get; set; }
        [NotMapped]
        [Display(Name = "分類名稱")]
        public string CategoryName { get; set; }
    }
}

public class z_metaOrderDetails
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "父階編號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ParentNo { get; set; }
    [Display(Name = "廠商編號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string VendorNo { get; set; }
    [Display(Name = "分類編號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string CategoryNo { get; set; }
    [Display(Name = "商品編號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ProdNo { get; set; }
    [Display(Name = "商品名稱")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ProdName { get; set; }
    [Display(Name = "商品規格")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ProdSpec { get; set; }
    [Display(Name = "單價")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public int OrderPrice { get; set; }
    [Display(Name = "數量")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public int OrderQty { get; set; }
    [Display(Name = "小計")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public int OrderAmount { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}
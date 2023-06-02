using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaProducts))]
    public partial class Products
    {
        [NotMapped]
        [Display(Name = "廠商名稱")]
        public string VendorName { get; set; }
        [NotMapped]
        [Display(Name = "商品分類")]
        public string CategoryName { get; set; }
    }
}

public abstract class z_metaProducts
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_False, DefaultValue = "")]
    public bool IsEnabled { get; set; }
    [Display(Name = "商品編號")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "")]
    public string ProdNo { get; set; }
    [Display(Name = "商品名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "")]
    public string ProdName { get; set; }
    [Display(Name = "條碼編號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "")]
    public string BarcodeNo { get; set; }
    [Display(Name = "廠商編號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "")]
    public string VendorNo { get; set; }
    [Display(Name = "分類編號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "")]
    public string CategoryNo { get; set; }
    [Display(Name = "成本單價")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public int CostPrice { get; set; }
    [Display(Name = "銷售單價")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public int SalePrice { get; set; }
    [Display(Name = "折扣單價")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public int DiscountPrice { get; set; }
    [Display(Name = "商品內容")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "")]
    public string ContentText { get; set; }
    [Display(Name = "明細說明")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "")]
    public string DetailText { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "")]
    public string Remark { get; set; }
}

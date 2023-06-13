using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaCarts))]
    public partial class Carts
    {
    }
}

public partial class z_metaCarts
{
    [Key]
    public int Id { get; set; }
    //[Display(Name = "批號")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string LotNo { get; set; }
    //[Display(Name = "會員編號")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string MemberNo { get; set; }
    //[Display(Name = "商品編號")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string ProdNo { get; set; }
    //[Display(Name = "商品名稱")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string ProdName { get; set; }
    //[Display(Name = "商品規格")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string ProdSpec { get; set; }
    //[Display(Name = "數量")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    //public int OrderQty { get; set; }
    //[Display(Name = "單價")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    //public int OrderPrice { get; set; }
    //[Display(Name = "小計金額")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    //public int OrderAmount { get; set; }
    //[Display(Name = "建立時間")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.Date_NowTime, DefaultValue = "")]
    //public System.DateTime CreateTime { get; set; }
    //[Display(Name = "備註")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string Remark { get; set; }
}
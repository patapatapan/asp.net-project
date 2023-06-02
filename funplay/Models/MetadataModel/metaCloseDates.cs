using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaCloseDates))]
    public partial class CloseDates
    {
    }
}

public abstract class z_metaCloseDates
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "分類代號")]
    [Required(ErrorMessage = "分類代號不可空白!!")]
    [Unique("CloseDates", "Id", "CodeNo", ErrorMessage = "資料重覆輸入!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string CodeNo { get; set; }
    [Display(Name = "開始日期")]
    [Required(ErrorMessage = "開始日期不可空白!!")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Date_Now, DefaultValue = "")]
    public DateTime StartDate { get; set; }
    [Display(Name = "結束日期")]
    [Required(ErrorMessage = "結束日期不可空白!!")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Date_Now, DefaultValue = "")]
    public DateTime EndDate { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}

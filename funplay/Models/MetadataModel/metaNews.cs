using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaNews))]
    public partial class News
    {
        [NotMapped]
        [Display(Name = "類別名稱")]
        public string CodeName { get; set; }
    }
}

public abstract class z_metaNews
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "類別代號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string CodeNo { get; set; }
    [Display(Name = "發佈日期")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Date_Now, DefaultValue = "")]
    public DateTime PublishDate { get; set; }
    [Display(Name = "主要標題")]
    [Required(ErrorMessage = "主要標題不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string HeaderName { get; set; }
    [Display(Name = "明細說明")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string DetailText { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}

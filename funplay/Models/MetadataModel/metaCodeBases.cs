using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaCodeBases))]
    public partial class CodeBases
    {
        [NotMapped]
        [Display(Name = "明細數")]
        public int Counts { get; set; }
    }
}

public class z_metaCodeBases
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "後台管理")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_False, DefaultValue = "")]
    public bool IsAdmin { get; set; }
    [Display(Name = "代號")]
    [Required(ErrorMessage = "代號不可空白!!")]
    [Unique("CodeBases", "Id", "CodeNo", ErrorMessage = "資料重覆輸入!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string BaseNo { get; set; }
    [Display(Name = "名稱")]
    [Required(ErrorMessage = "名稱不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string BaseName { get; set; }
    [Display(Name = "預設值")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string DefaultValue { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}
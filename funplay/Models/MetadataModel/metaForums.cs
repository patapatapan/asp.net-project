using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaForums))]
    public partial class Forums
    {
        [NotMapped]
        [Display(Name = "發佈人員")]
        public string UserName { get; set; }
        [NotMapped]
        [Display(Name = "回覆數")]
        public string ReplyCount { get; set; }
        [NotMapped]
        [Display(Name = "發佈時間")]
        public string SubjectTime
        {
            get
            {
                if (SubjectDate == null) return "";
                return SubjectDate.ToString("yyyy/MM/dd HH:mm");
            }
        }
    }
}

public abstract class z_metaForums
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "父階編號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ParentGuid { get; set; }
    [Display(Name = "回覆編號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ReplyGuid { get; set; }
    [Display(Name = "版面編號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string BoardNo { get; set; }
    [Display(Name = "啟用")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_False, DefaultValue = "")]
    public bool IsEnabled { get; set; }
    [Display(Name = "結案")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_False, DefaultValue = "")]
    public bool IsClosed { get; set; }
    [Display(Name = "發佈時間")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Custom, DefaultValue = "")]
    public System.DateTime SubjectDate { get; set; }
    [Display(Name = "發佈帳號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string UserNo { get; set; }
    [Display(Name = "主旨")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string SubjectName { get; set; }
    [Display(Name = "內容明細")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string SubjectContent { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
    [Display(Name = "唯一值")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string GuidNo { get; set; }
}

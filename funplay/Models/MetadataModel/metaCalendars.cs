using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaCalendars))]
    public partial class Calendars
    {
        [NotMapped]
        [Display(Name = "類別名稱")]
        public int CodeName { get; set; }
        [NotMapped]
        [Display(Name = "開始日期")]
        public string EventStart { get { return (StartDate == null) ? "1911/01/01" : StartDate.ToString("yyyy/MM/dd"); } }
        [NotMapped]
        [Display(Name = "結束日期")]
        public string EventEnd { get { return (EndDate == null) ? "1911/01/01" : EndDate.ToString("yyyy/MM/dd"); } }
        [NotMapped]
        [Display(Name = "時始小時")]
        public string StartHour { get { return (string.IsNullOrEmpty(StartTime)) ? "00" : StartTime.Substring(0, 2); } }
        [NotMapped]
        [Display(Name = "時始分鐘")]
        public string StartMinute { get { return (string.IsNullOrEmpty(StartTime)) ? "00" : StartTime.Substring(3, 2); } }
        [NotMapped]
        [Display(Name = "時始小時")]
        public string EndHour { get { return (string.IsNullOrEmpty(EndTime)) ? "00" : EndTime.Substring(0, 2); } }
        [NotMapped]
        [Display(Name = "時始分鐘")]
        public string EndMinute { get { return (string.IsNullOrEmpty(EndTime)) ? "00" : EndTime.Substring(3, 2); } }
    }
}

public abstract class z_metaCalendars
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "對象類別")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string TargetCode { get; set; }
    [Display(Name = "對象代號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string TargetNo { get; set; }
    [Display(Name = "類別")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string CodeNo { get; set; }
    [Display(Name = "主旨")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string SubjectName { get; set; }
    [Display(Name = "開始日期")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public System.DateTime StartDate { get; set; }
    [Display(Name = "開始時間")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Date_Today, DefaultValue = "")]
    public string StartTime { get; set; }
    [Display(Name = "結束日期")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Date_Today, DefaultValue = "")]
    public System.DateTime EndDate { get; set; }
    [Display(Name = "結束時間")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string EndTime { get; set; }
    [Display(Name = "行程顏色")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ColorName { get; set; }
    [Display(Name = "全天行程")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public bool IsFullday { get; set; }
    [Display(Name = "地點名稱")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string PlaceName { get; set; }
    [Display(Name = "連絡人")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ContactName { get; set; }
    [Display(Name = "連絡電話")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ContactTel { get; set; }
    [Display(Name = "地點地址")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string PlaceAddress { get; set; }
    [Display(Name = "會議室號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string RoomNo { get; set; }
    [Display(Name = "攜帶物品")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ResourceText { get; set; }
    [Display(Name = "詳細內容")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Description { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}
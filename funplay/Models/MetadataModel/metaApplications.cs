using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaApplications))]
    public partial class Applications
    {
    }
}

public abstract class z_metaApplications
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_False, DefaultValue = "")]
    public bool IsEnabled { get; set; }
    [Display(Name = "除錯模式")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_False, DefaultValue = "")]
    public bool IsDebug { get; set; }
    [Display(Name = "應用程式名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string AppName { get; set; }
    [Display(Name = "應用程式版本")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string AppVersion { get; set; }
    [Display(Name = "啟用加密")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_False, DefaultValue = "")]
    public bool EncryptionMode { get; set; }
    [Display(Name = "設計公司")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string PowerBy { get; set; }
    [Display(Name = "預設語言")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string LanguageNo { get; set; }
    [Display(Name = "GoogleMap Key")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string GoogleMapKey { get; set; }
    [Display(Name = "郵件寄件名稱")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string MailSenderName { get; set; }
    [Display(Name = "郵件寄件郵件")]
    [EmailAddress(ErrorMessage = "電子信箱格式不正確!!")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string MailSenderEmail { get; set; }
    [Display(Name = "郵件收件名稱")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string MailReceiverName { get; set; }
    [Display(Name = "郵件收件郵件")]
    [EmailAddress(ErrorMessage = "電子信箱格式不正確!!")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string MailReceiverEmail { get; set; }
    [Display(Name = "應用程式密碼")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string MailAppPassword { get; set; }
    [Display(Name = "郵件伺服器")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string MailHostUrl { get; set; }
    [Display(Name = "郵件埠號")]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N0}")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public int MailHostPort { get; set; }
    [Display(Name = "郵件啟用SSL")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_False, DefaultValue = "")]
    public bool MailUseSSL { get; set; }
    [Display(Name = "網站位址")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string WebSiteUrl { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = true, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaUsers))]
    public partial class Users
    {
        [NotMapped]
        [Display(Name = "角色名稱")]
        public string RoleName { get; set; }
        [NotMapped]
        [Display(Name = "部門名稱")]
        public string DeptName { get; set; }
        [NotMapped]
        [Display(Name = "職務名稱")]
        public string TitleName { get; set; }
        [NotMapped]
        [Display(Name = "性別")]
        public string GenderName { get; set; }
        [NotMapped]
        [Display(Name = "類別")]
        public string CodeName { get; set; }
        [NotMapped]
        [Display(Name = "程式數")]
        public int Programs { get; set; }
        [NotMapped]
        [Display(Name = "照片")]
        public string UserImage { get; set; }
    }
}

public abstract class z_metaUsers
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "合法")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Boolean_True, DefaultValue = "")]
    public bool IsValid { get; set; }

    // 不需要檢核的資料欄位全部隱藏
    //[Display(Name = "代號")]
    //[Required(ErrorMessage = "代號不可空白!!")]
    //[Unique("Users", "Id", "UserNo", ErrorMessage = "資料重覆輸入!!")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string UserNo { get; set; }
    [Display(Name = "名稱")]
    [Required(ErrorMessage = "名稱不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string UserName { get; set; }
    [Display(Name = "登入密碼")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Password { get; set; }
    //[Display(Name = "類別")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string CodeNo { get; set; }
    [Display(Name = "角色代號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string RoleNo { get; set; }
    //[Display(Name = "性別")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string GenderCode { get; set; }
    //[Display(Name = "部門代號")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string DeptNo { get; set; }
    //[Display(Name = "職務代號")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string TitleNo { get; set; }
    [Display(Name = "出生日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Date_Today, DefaultValue = "")]
    public Nullable<System.DateTime> Birthday { get; set; }
    //[Display(Name = "到職日期")]
    //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.Date_Today, DefaultValue = "")]
    //public Nullable<System.DateTime> OnboardDate { get; set; }
    //[Display(Name = "離職日期")]
    //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.Date_Today, DefaultValue = "")]
    //public Nullable<System.DateTime> LeaveDate { get; set; }
    //[Display(Name = "電子信箱")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string ContactEmail { get; set; }
    //[Display(Name = "連絡電話")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string ContactTel { get; set; }
    //[Display(Name = "連絡地址")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string ContactAddress { get; set; }
    [Display(Name = "驗證碼")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ValidateCode { get; set; }
    //[Display(Name = "備註")]
    //[Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    //[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    //public string Remark { get; set; }
}
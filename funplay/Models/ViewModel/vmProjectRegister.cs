using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


public class vmProjectRegister
{
    [Required(ErrorMessage = "姓名不可空白!!")]
    public string UserName { get; set; }
    //[Required(ErrorMessage = "生日不可空白!!")]
    //[DataType(DataType.Date)]
    //public DateTime Birthday { get; set; }
    [Required(ErrorMessage = "電子信箱不可空白!!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "連絡電話不可空白!!")]
    public string Tel { get; set; }
    [Required(ErrorMessage = "帳號不可空白!!")]
    public string Account { get; set; }
    [Required(ErrorMessage = "密碼不可空白!!")]
    [MinLength(4,ErrorMessage = "密碼不可小於4個字元")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    //確認密碼
    [Required(ErrorMessage = "確認密碼不可空白!!")]
    [MinLength(4, ErrorMessage = "確認密碼不可小於4個字元")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "確認密碼與密碼不符")]
    public string ConfirmPassword { get; set; }
}

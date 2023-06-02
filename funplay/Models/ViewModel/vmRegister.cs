using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class vmRegister
{
    [Display(Name = "登入帳號")]
    [Required(ErrorMessage = "登入帳號不可空白!!")]
    public string UserNo { get; set; }
    [Display(Name = "登入姓名")]
    [Required(ErrorMessage = "登入姓名不可空白!!")]
    public string UserName { get; set; }
    [Display(Name = "登人密碼")]
    [Required(ErrorMessage = "登人密碼不可空白!!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Display(Name = "確認密碼")]
    [Required(ErrorMessage = "欄位不可空白!!")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "確認密碼與新的密碼不相同!!")]
    public string ConfirmPassword { get; set; }
    [Required(ErrorMessage = "電子信箱不可空白!!")]
    [EmailAddress(ErrorMessage = "電子信箱格式錯誤!!")]
    public string ContactEmail { get; set; }
    [Display(Name = "記住我")]
    public bool RememberMe { get; set; }
}
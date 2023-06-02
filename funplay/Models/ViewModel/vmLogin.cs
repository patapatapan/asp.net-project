using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class vmLogin
{
    [Display(Name = "登入帳號")]
    [Required(ErrorMessage = "欄位不可空白!!")]
    public string UserNo { get; set; }
    [Display(Name = "登人密碼")]
    [Required(ErrorMessage = "欄位不可空白!!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
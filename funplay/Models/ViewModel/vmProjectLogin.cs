﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


public class vmProjectLogin
{
    [Required(ErrorMessage = "欄位不可空白!!")]
    //表單傳入的值若為空 則回傳錯誤訊息
    public string Account { get; set; }
    [Required(ErrorMessage = "欄位不可空白!!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

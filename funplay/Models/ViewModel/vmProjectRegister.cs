using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


public class vmProjectRegister
{
    [Required(ErrorMessage = "欄位不可空白!!")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "欄位不可空白!!")]
    [DataType(DataType.Date)]
    public string Birthday { get; set; }
    [Required(ErrorMessage = "欄位不可空白!!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "欄位不可空白!!")]
    public string Tel { get; set; }
    [Required(ErrorMessage = "欄位不可空白!!")]
    public string Account { get; set; }
    [Required(ErrorMessage = "欄位不可空白!!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

public class vmContact
{
    [Display(Name = "您的姓名")]
    [Required(ErrorMessage = "您的姓名不可空白!!")]
    public string ContactorName { get; set; }
    [Display(Name = "電子信箱")]
    [Required(ErrorMessage = "電子信箱不可空白!!")]
    [EmailAddress(ErrorMessage = "電子信箱格式錯誤!!")]
    public string ContactorEmail { get; set; }
    [Display(Name = "訊息主旨")]
    [Required(ErrorMessage = "訊息主旨不可空白!!")]
    public string ContactorSubject { get; set; }
    [Display(Name = "訊息內文")]
    [Required(ErrorMessage = "訊息內文不可空白!!")]
    public string ContactorMessage { get; set; }
}
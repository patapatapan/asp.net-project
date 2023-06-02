using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class dmUserList
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "選取")]
    public bool IsChecked { get; set; }
    [Display(Name = "使用者編號")]
    public string UserNo { get; set; }
    [Display(Name = "姓名")]
    public string UserName { get; set; }
}
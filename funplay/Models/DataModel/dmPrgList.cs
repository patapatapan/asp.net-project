using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class dmPrgList
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "選取")]
    public bool IsChecked { get; set; }
    [Display(Name = "程式編號")]
    public string PrgNo { get; set; }
    [Display(Name = "程式名稱")]
    public string PrgName { get; set; }
}
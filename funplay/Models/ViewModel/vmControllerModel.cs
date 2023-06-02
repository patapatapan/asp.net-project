using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class vmControllerModel
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "區域名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    public string AreaName { get; set; }
    [Display(Name = "控制器名稱")]
    public string ControllerName { get; set; }
    [Display(Name = "類別名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    public string ClassName { get; set; }
    [Display(Name = "程式代號")]
    public string PrgNo { get; set; }
    [Display(Name = "程式名稱")]
    public string PrgName { get; set; }
    [Display(Name = "資料夾名稱")]
    public string FolderName { get; set; }
    [Display(Name = "檔案名稱")]
    public string FileName { get; set; }
    [Display(Name = "產生結果")]
    public string TextResult { get; set; }
}
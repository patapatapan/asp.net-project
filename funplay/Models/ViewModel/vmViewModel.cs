using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class vmViewModel
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "區域名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    public string AreaName { get; set; }
    [Display(Name = "控制器名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    public string ControllerName { get; set; }
    [Display(Name = "網頁名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    public string ViewName { get; set; }
    [Display(Name = "刪除顯示欄位")]
    public string DeleteConfirmColumns { get; set; }
    [Display(Name = "主索引鍵")]
    public string KeyColumn { get; set; }
    [Display(Name = "範本名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    public string TemplateName { get; set; }
    [Display(Name = "類別名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    public string ClassName { get; set; }
    [Display(Name = "主版頁面")]
    [Required(ErrorMessage = "不可空白!!")]
    public string LayoutName { get; set; }
    [Display(Name = "資料夾名稱")]
    public string FolderName { get; set; }
    [Display(Name = "檔案名稱")]
    public string FileName { get; set; }
    [Display(Name = "產生結果")]
    public string TextResult { get; set; }
}
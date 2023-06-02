using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class vmMetadataModel
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "類別名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    public string ClassName { get; set; }
    [Display(Name = "主鍵欄位")]
    public string KeyColumn { get; set; }
    [Display(Name = "資料夾名稱")]
    public string FolderName { get; set; }
    [Display(Name = "檔案名稱")]
    public string FileName { get; set; }
    [Display(Name = "必輸欄位")]
    public string RequiredColumns { get; set; }
    [Display(Name = "唯一編號欄位")]
    public string UniqueNoColumn { get; set; }
    [Display(Name = "產生結果")]
    public string TextResult { get; set; }
}
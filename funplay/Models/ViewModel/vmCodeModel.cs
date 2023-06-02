using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class vmCodeModel
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
    [Display(Name = "產生分類")]
    [Required(ErrorMessage = "不可空白!!")]
    public string TypeNo { get; set; }
    [Display(Name = "主鍵欄位")]
    public string KeyColumn { get; set; }
    [Display(Name = "編號欄位")]
    public string NoColumn { get; set; }
    [Display(Name = "名稱欄位")]
    public string NameColumn { get; set; }
    [Display(Name = "排序欄位")]
    public string SortColumns { get; set; }
    [Display(Name = "報表檔名")]
    public string ReportFileName { get; set; }
    [Display(Name = "程式代號")]
    public string PrgNo { get; set; }
    [Display(Name = "程式名稱")]
    public string PrgName { get; set; }
    [Display(Name = "必輸欄位")]
    public string RequiredColumns { get; set; }
    [Display(Name = "編輯欄數")]
    public int BlockCount { get; set; }
    [Display(Name = "資料夾名稱")]
    public string FolderName { get; set; }
    [Display(Name = "網頁名稱")]
    public string ViewName { get; set; }
    [Display(Name = "範本名稱")]
    public string TemplateName { get; set; }
    [Display(Name = "主版頁面")]
    [Required(ErrorMessage = "不可空白!!")]
    public string LayoutName { get; set; }
    [Display(Name = "檔案名稱")]
    public string FileName { get; set; }
    [Display(Name = "Metadata 結果")]
    public string MetadataResult { get; set; }
    [Display(Name = "Repository 結果")]
    public string RepositoryResult { get; set; }
    [Display(Name = "Controller 結果")]
    public string ControllerResult { get; set; }
    [Display(Name = "Index View 結果")]
    public string IndexViewResult { get; set; }
    [Display(Name = "CreateEdit View 結果")]
    public string CreateEditViewResult { get; set; }
}

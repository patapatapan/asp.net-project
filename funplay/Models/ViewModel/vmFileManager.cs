using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

public class vmFileManager
{
    [Key]
    public int Id { get; set; } = 0;
    [Display(Name = "路徑")]
    public string PathName { get; set; } = "";
    public List<FileInfo> FileInfoList { get; set; }
}
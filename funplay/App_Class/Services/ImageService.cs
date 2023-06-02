using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

public static class ImageService
{
    /// <summary>
    /// 檔案路徑
    /// </summary>
    public static string FilePath { get; set; } = @"~/Images";
    /// <summary>
    /// 檔案名稱
    /// </summary>
    public static string FileName { get; set; } = "none";
    /// <summary>
    /// 檔案副檔名
    /// </summary>
    public static string FileExtension { get; set; } = "jpg";
    /// <summary>
    /// 變更檔名
    /// </summary>
    public static bool ChangeFileName { get; set; } = true;
    /// <summary>
    /// 檔案設定
    /// </summary>
    /// <param name="filePath">檔案路徑</param>
    public static void FileConfig(string filePath)
    {
        FileConfig(filePath, "");
    }
    /// <summary>
    /// 檔案設定
    /// </summary>
    /// <param name="filePath">檔案路徑</param>
    /// <param name="fileExtension">檔案副檔名</param>
    public static void FileConfig(string filePath, string fileExtension)
    {
        FilePath = filePath;
        FileName = "";
        FileExtension = fileExtension;
        ChangeFileName = false;
        CheckPath(filePath);
    }
    /// <summary>
    /// 檔案設定
    /// </summary>
    /// <param name="filePath">檔案路徑</param>
    /// <param name="fileName">檔案名稱</param>
    /// <param name="fileExtension">檔案副檔名</param>
    public static void FileConfig(string filePath, string fileName, string fileExtension)
    {
        FilePath = filePath;
        FileName = fileName;
        FileExtension = fileExtension;
        ChangeFileName = true;
        CheckPath(filePath);
    }
    /// <summary>
    /// 檔案路徑
    /// </summary>
    /// <param name="filePath"></param>
    private static void CheckPath(string filePath)
    {
        string path = HttpContext.Current.Server.MapPath(filePath);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    }
    /// <summary>
    /// 檔案上傳時另存到指定位置
    /// </summary>
    /// <param name="file">上傳的檔案物件</param>
    /// <param name="filePath">另存的路徑,如 ~/Images/User</param>
    /// <param name="fileName">另存的檔名,如 001.jpg</param>
    /// <returns></returns>
    public static string FileUpload(HttpPostedFileBase file)
    {
        string str_message = string.Empty;
        if (file != null)
        {
            if (file.ContentLength > 0)
            {
                try
                {
                    string str_file_name = "";
                    if (ChangeFileName)
                        str_file_name = string.Format("{0}.{1}", FileName, FileExtension);
                    else
                        str_file_name = Path.GetFileName(file.FileName);

                    string str_full_name = Path.Combine(HttpContext.Current.Server.MapPath(FilePath), str_file_name);
                    if (File.Exists(str_full_name)) File.Delete(str_full_name);
                    file.SaveAs(str_full_name);
                }
                catch (Exception ex)
                {
                    str_message = ex.Message;
                }
            }
        }
        return str_message;
    }
}
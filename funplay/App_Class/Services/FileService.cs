using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

public class FileService : BaseClass
{
    public List<FileInfo> GetFileInfoList(string pathName)
    {
        string str_path = HttpContext.Current.Server.MapPath(pathName);
        DirectoryInfo dirInfo = new DirectoryInfo(str_path);
        List<FileInfo> model = new List<FileInfo>();
        if (dirInfo != null)
        {
            var fileInfos = dirInfo.GetFiles();
            if (fileInfos != null) model = fileInfos.ToList();
        }
        return model;
    }

    public string GetExtensionName(string fileName)
    {
        string str_file = HttpContext.Current.Server.MapPath(fileName);
        return MimeMapping.GetMimeMapping(str_file);
    }
}
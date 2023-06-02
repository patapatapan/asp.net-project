using funplay.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// 語系資源服務
/// </summary>
public static class ResourceService
{
    /// <summary>
    /// 以鍵值取得 Common 語系內容
    /// </summary>
    /// <param name="key">鍵值</param>
    /// <returns></returns>
    public static string Common(string type)
    {
        return new ResourceManager(typeof(resCommon)).GetString(type);
    }
    /// <summary>
    /// 以鍵值取得 Common 語系內容
    /// </summary>
    /// <param name="key">鍵值</param>
    /// <returns></returns>
    public static string Column(string type)
    {
        return new ResourceManager(typeof(resColumn)).GetString(type);
    }
    /// <summary>
    /// 以鍵值取得 Common 語系內容
    /// </summary>
    /// <param name="key">鍵值</param>
    /// <returns></returns>
    public static string Message(string key)
    {
        return new ResourceManager(typeof(resMessage)).GetString(key);
    }
    /// <summary>
    /// 以鍵值取得 Common 語系內容
    /// </summary>
    /// <param name="key">鍵值</param>
    /// <returns></returns>
    public static string Program(string key)
    {
        return new ResourceManager(typeof(resProgram)).GetString(key);
    }
}
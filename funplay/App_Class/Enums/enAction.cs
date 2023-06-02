using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 控制器的動作
/// </summary>
public enum enAction
{
    /// <summary>
    /// 首頁
    /// </summary>
    Home,
    /// <summary>
    /// 列表
    /// </summary>
    Index,
    /// <summary>
    /// 列表
    /// </summary>
    List,
    /// <summary>
    /// 匯入
    /// </summary>
    Import,
    /// <summary>
    /// 取消
    /// </summary>
    Cancel,
    /// <summary>
    /// 結案
    /// </summary>
    Close,
    /// <summary>
    /// 行事曆
    /// </summary>
    Calendar,
    /// <summary>
    /// 新增
    /// </summary>
    Create,
    /// <summary>
    /// 複製
    /// </summary>
    Copy,
    /// <summary>
    /// 修改
    /// </summary>
    Edit,
    /// <summary>
    /// 刪除
    /// </summary>
    Delete,
    /// <summary>
    /// 明細
    /// </summary>
    Detail,
    /// <summary>
    /// 開啟
    /// </summary>
    Open,
    /// <summary>
    /// 密碼
    /// </summary>
    Password,
    /// <summary>
    /// 列印
    /// </summary>
    Print,
    /// <summary>
    /// 上傳
    /// </summary>
    Upload,
    /// <summary>
    /// 上傳檔案
    /// </summary>
    UploadFile,
    /// <summary>
    /// 上傳圖片
    /// </summary>
    UploadImage,
    /// <summary>
    /// 上傳圖庫
    /// </summary>
    UploadImages,
    /// <summary>
    /// 下載
    /// </summary>
    Download,
    /// <summary>
    /// 搜尋
    /// </summary>
    Find,
    /// <summary>
    /// 選取
    /// </summary>
    Select,
    /// <summary>
    /// 查詢
    /// </summary>
    Search,
    /// <summary>
    /// 排序
    /// </summary>
    Sort,
    /// <summary>
    /// 存檔
    /// </summary>
    Save,
    /// <summary>
    /// 返回
    /// </summary>
    Return,
    /// <summary>
    /// 返回列表
    /// </summary>
    ReturnIndex,
    /// <summary>
    /// 返回首頁
    /// </summary>
    ReturnHome,
    /// <summary>
    /// 返回明細
    /// </summary>
    ReturnDetail
}
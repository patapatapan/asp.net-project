using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// 事件服務類別
/// </summary>
public static class ActionService
{
    /// <summary>
    /// Row ID
    /// </summary>
    public static int RowId { get; set; }
    /// <summary>
    /// Row Data
    /// </summary>
    public static string RowData { get; set; }
    /// <summary>
    /// 取得預設 Home 名稱
    /// </summary>
    public static string Home { get { return "Home"; } }
    public static string HomeName { get { return GetActionName(enAction.Home); } }
    /// <summary>
    /// 取得預設 Home 名稱
    /// </summary>
    public static string Cancel { get { return "Cancel"; } }
    public static string CancelName { get { return GetActionName(enAction.Cancel); } }
    /// <summary>
    /// 取得預設 Init 名稱
    /// </summary>
    /// <returns></returns>
    public static string Init { get { return "Init"; } }
    /// <summary>
    /// 取得預設 Index 名稱
    /// </summary>
    /// <returns></returns>
    public static string Index { get { return "Index"; } }
    public static string IndexName { get { return GetActionName(enAction.Index); } }
    /// <summary>
    /// Html Editor
    /// </summary>
    public static string HtmlEditor { get { return "HtmlEditor"; } }
    /// <summary>
    /// 取得預設 Index 名稱
    /// </summary>
    /// <returns></returns>
    public static string Import { get { return "Import"; } }
    public static string ImportName { get { return GetActionName(enAction.Import); } }
    /// <summary>
    /// 取得預設 List 名稱
    /// </summary>
    /// <returns></returns>
    public static string List { get { return "List"; } }
    public static string ListName { get { return GetActionName(enAction.List); } }
    /// <summary>
    /// 取得預設 CreateEdit 名稱
    /// </summary>
    public static string CreateEdit { get { return "CreateEdit"; } }
    /// <summary>
    /// 取得預設 Create 名稱
    /// </summary>
    /// <returns></returns>
    public static string Create { get { return "Create"; } }
    public static string CreateName { get { return GetActionName(enAction.Create); } }
    /// <summary>
    /// 取得預設 Copy 名稱
    /// </summary>
    /// <returns></returns>
    public static string Copy { get { return "Copy"; } }
    public static string CopyName { get { return GetActionName(enAction.Copy); } }
    /// <summary>
    /// 取得預設 Close 名稱
    /// </summary>
    /// <returns></returns>
    public static string Close { get { return "Close"; } }
    public static string CloseName { get { return GetActionName(enAction.Close); } }
    /// <summary>
    /// 取得預設 Edit 名稱
    /// </summary>
    /// <returns></returns>
    public static string Edit { get { return "Edit"; } }
    public static string EditName { get { return GetActionName(enAction.Edit); } }
    /// <summary>
    /// 取得預設 Delete 名稱
    /// </summary>
    /// <returns></returns>
    public static string Delete { get { return "Delete"; } }
    public static string DeleteName { get { return GetActionName(enAction.Delete); } }
    /// <summary>
    /// 取得預設 Delete OnClick 程式
    /// </summary>
    /// <returns></returns>
    public static string DeleteOnClick { get { return string.Format("return confirm('{0}');", "是否確定要刪除?"); } }
    /// <summary>
    /// 取得預設 Detail 名稱
    /// </summary>
    /// <returns></returns>
    public static string Detail { get { return "Detail"; } }
    public static string DetailName { get { return GetActionName(enAction.Detail); } }
    /// <summary>
    /// 取得預設 Description 名稱
    /// </summary>
    /// <returns></returns>
    public static string Description { get { return "Description"; } }
    /// <summary>
    /// 取得預設 Open 名稱
    /// </summary>
    /// <returns></returns>
    public static string Open { get { return "Open"; } }
    public static string OpenName { get { return GetActionName(enAction.Open); } }
    /// <summary>
    /// 取得預設 Select 名稱
    /// </summary>
    /// <returns></returns>
    public static string Select { get { return "Select"; } }
    public static string SelectName { get { return GetActionName(enAction.Select); } }
    /// <summary>
    /// 取得預設 Sort 名稱
    /// </summary>
    /// <returns></returns>
    public static string Sort { get { return "Sort"; } }
    public static string SortName { get { return GetActionName(enAction.Sort); } }
    /// <summary>
    /// 取得預設 Spec 名稱
    /// </summary>
    /// <returns></returns>
    public static string Spec { get { return "Spec"; } }
    /// <summary>
    /// 取得預設 Property 名稱
    /// </summary>
    /// <returns></returns>
    public static string Property { get { return "Property"; } }
    public static string Image { get { return "Image"; } }
    /// <summary>
    /// 取得預設 Upload 名稱
    /// </summary>
    /// <returns></returns>
    public static string Password { get { return "Password"; } }
    public static string PasswordName { get { return GetActionName(enAction.Password); } }
    /// <summary>
    /// 取得預設 Upload 名稱
    /// </summary>
    /// <returns></returns>
    public static string Upload { get { return "Upload"; } }
    public static string UploadName { get { return GetActionName(enAction.Upload); } }
    /// <summary>
    /// 取得預設 UploadImage 名稱
    /// </summary>
    /// <returns></returns>
    public static string UploadImage { get { return "UploadImage"; } }
    public static string UploadImageName { get { return GetActionName(enAction.UploadImage); } }
    /// <summary>
    /// 取得預設 UploadImages 名稱
    /// </summary>
    /// <returns></returns>
    public static string UploadImages { get { return "UploadImages"; } }
    public static string UploadImagesName { get { return GetActionName(enAction.UploadImages); } }
    /// <summary>
    /// 取得預設 Download 名稱
    /// </summary>
    /// <returns></returns>
    public static string Download { get { return "Download"; } }
    public static string DownloadName { get { return GetActionName(enAction.Download); } }
    /// <summary>
    /// 取得預設 Save 名稱
    /// </summary>
    public static string SaveName { get { return GetActionName(enAction.Save); } }
    /// <summary>
    /// 取得預設 Return 名稱
    /// </summary>
    public static string Return { get { return GetActionName(enAction.Return); } }
    /// <summary>
    /// 取得預設 ReturnDetail 名稱
    /// </summary>
    public static string ReturnDetail { get { return GetActionName(enAction.ReturnDetail); } }
    /// <summary>
    /// 取得預設 ReturnIndex 名稱
    /// </summary>
    public static string ReturnIndex { get { return GetActionName(enAction.ReturnIndex); } }
    /// <summary>
    /// 取得預設 ReturnHome 名稱
    /// </summary>
    public static string ReturnHome { get { return GetActionName(enAction.ReturnHome); } }
    public static string Search { get { return "Search"; } }
    /// <summary>
    /// 取得預設 SearchText 名稱
    /// </summary>
    /// <returns></returns>
    public static string SearchText { get { return "search_text"; } }
    /// <summary>
    /// 取得目前的 Area 名稱
    /// </summary>
    /// <returns></returns>
    public static string Area
    {
        get
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"];
            if (routeValues != null) return (string)routeValues;
            return string.Empty;
        }
    }
    /// <summary>
    /// 取得目前的 Controller 名稱
    /// </summary>
    /// <returns></returns>
    public static string Controller
    {
        get
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            if (routeValues.ContainsKey("controller")) return (string)routeValues["controller"];
            return string.Empty;
        }
    }

    /// <summary>
    /// 取得目前 Action 名稱
    /// </summary>
    /// <returns></returns>
    public static string Action
    {
        get
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            if (routeValues.ContainsKey("action")) return (string)routeValues["action"];
            return string.Empty;
        }
    }
    /// <summary>
    /// 取得目前 id 參數名稱
    /// </summary>
    /// <returns></returns>
    public static string Id
    {
        get
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            if (routeValues.ContainsKey("id")) return (string)routeValues["id"];
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("id"))
                return HttpContext.Current.Request.QueryString["id"];
            return string.Empty;
        }
    }

    public static string PathName { get; set; } = "";
    public static string TargetCode { get; set; } = "";
    public static string TargetNo { get; set; } = "";
    public static string PriorArea { get; set; } = "";
    public static string PriorController { get; set; } = "";
    public static string PriorAction { get; set; } = "";
    public static string PriorTableName { get; set; } = "";
    public static string PriorKeyName { get; set; } = "";
    public static int PriorKeyValue { get; set; } = 0;
    public static string PriorTextName { get; set; } = "";
    public static string PriorTextValue { get; set; } = "";
    public static enPriorParmIdType PriorParmIdType { get; set; } = enPriorParmIdType.None;
    public static object PriorParmIdValue { get; set; }
    public static string PriorImageFolder { get; set; } = "";
    /// <summary>
    /// 設定行事曆事件
    /// </summary>
    /// <param name="targetCode"></param>
    /// <param name="targetNo"></param>
    /// <param name="subHeader"></param>
    public static void SetCalendarAction(string targetCode, string targetNo, string subHeader)
    {
        TargetCode = targetCode;
        TargetNo = targetNo;
        PrgService.SubHeader = subHeader;
    }
    /// <summary>
    /// 設定上一個事件的資訊
    /// </summary>
    /// <param name="areaName">Area</param>
    /// <param name="controllerName">Controller</param>
    /// <param name="actionName">Action</param>
    /// <param name="parmType">Parameter Type</param>
    /// <param name="parmValue">Parameter Value</param>
    /// <param name="imageFolder">Image Folder</param>
    public static void SetPriorAction(string areaName, string controllerName, string actionName, enPriorParmIdType parmType, object parmValue, string imageFolder)
    {
        PriorArea = areaName;
        PriorController = controllerName;
        PriorAction = actionName;
        PriorParmIdType = parmType;
        PriorParmIdValue = parmValue;
        PriorImageFolder = imageFolder;
    }
    /// <summary>
    /// 設定上一個事件更新資料
    /// </summary>
    /// <param name="tableName">Table Name</param>
    /// <param name="keyName">Key Column Name</param>
    /// <param name="keyValue">Key Value</param>
    /// <param name="textName">Text Column Name</param>
    /// <param name="textValue">Text Value</param>
    public static void SetPriorUpdate(string tableName, string keyName, int keyValue, string textName, string textValue)
    {
        PriorTableName = tableName;
        PriorKeyName = keyName;
        PriorKeyValue = keyValue;
        PriorTextName = textName;
        PriorTextValue = textValue;
    }

    /// <summary>
    /// 取得動作中文名稱
    /// </summary>
    /// <param name="actionName">動作名稱</param>
    /// <returns></returns>
    public static string GetActionName(enAction actionName)
    {
        string str_action = "未定義";
        if (actionName == enAction.Create) str_action = "新增";
        if (actionName == enAction.Cancel) str_action = "取消";
        if (actionName == enAction.Close) str_action = "結案";
        if (actionName == enAction.Copy) str_action = "複製";
        if (actionName == enAction.Calendar) str_action = "行事曆";
        if (actionName == enAction.Delete) str_action = "刪除";
        if (actionName == enAction.Detail) str_action = "明細";
        if (actionName == enAction.Download) str_action = "下載";
        if (actionName == enAction.Edit) str_action = "修改";
        if (actionName == enAction.Find) str_action = "查詢";
        if (actionName == enAction.Home) str_action = "首頁";
        if (actionName == enAction.Index) str_action = "列表";
        if (actionName == enAction.Import) str_action = "匯入";
        if (actionName == enAction.List) str_action = "列表";
        if (actionName == enAction.Open) str_action = "開啟";
        if (actionName == enAction.Password) str_action = "密碼";
        if (actionName == enAction.Print) str_action = "列印";
        if (actionName == enAction.Save) str_action = "異動存檔";
        if (actionName == enAction.Select) str_action = "選取";
        if (actionName == enAction.Search) str_action = "搜尋";
        if (actionName == enAction.Sort) str_action = "排序";
        if (actionName == enAction.Return) str_action = "返回";
        if (actionName == enAction.ReturnIndex) str_action = "返回列表";
        if (actionName == enAction.ReturnHome) str_action = "返回首頁";
        if (actionName == enAction.ReturnDetail) str_action = "返回明細";
        if (actionName == enAction.Upload) str_action = "上傳";
        if (actionName == enAction.UploadFile) str_action = "上傳檔案";
        if (actionName == enAction.UploadImage) str_action = "上傳圖片";
        if (actionName == enAction.UploadImages) str_action = "上傳圖庫";
        return str_action;
    }

    public static string SetErrorMessage(ModelStateDictionary modelState)
    {
        string str_message = "";
        if (modelState.Count() > 0)
        {
            foreach (var item in modelState)
            {
                if (item.Value.Errors.Count > 0)
                {
                    foreach (var data in item.Value.Errors)
                    {
                        str_message += $"<b>{data.ErrorMessage}<br>";
                    }
                }
            }
        }
        return str_message;
    }
    public static string SetErrorMessage<T>(ModelStateDictionary modelState)
    {
        string str_message = "";
        if (modelState.Count() > 0)
        {
            foreach (var item in modelState)
            {
                if (item.Value.Errors.Count > 0)
                {
                    using (AttributeService attr = new AttributeService())
                    {
                        foreach (var data in item.Value.Errors)
                        {
                            string str_name = attr.GetDisplayName<T>(item.Key);
                            if (string.IsNullOrEmpty(str_name)) str_name = item.Key;
                            str_message += $"<b>{str_name}：{data.ErrorMessage}<br>";
                        }
                    }
                }
            }
        }
        return str_message;
    }
}
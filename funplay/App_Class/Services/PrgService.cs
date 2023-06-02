using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 程式相關服務
/// </summary>
public static class PrgService
{
    /// <summary>
    /// 角色編號
    /// </summary>
    public static string RoleNo { get; set; }
    /// <summary>
    /// 程式編號
    /// </summary>
    public static string ModuleNo { get; set; }
    /// <summary>
    /// 程式編號
    /// </summary>
    public static string PrgNo { get; set; }
    /// <summary>
    /// 程式名稱
    /// </summary>
    public static string PrgName { get; set; }
    /// <summary>
    /// 程式資訊
    /// </summary>
    public static string PrgInfo
    {
        get
        {
            if (string.IsNullOrEmpty(PrgNo)) return PrgName;
            return string.Format("{0} {1}", PrgNo, PrgName);
        }
    }
    /// <summary>
    /// 副標題
    /// </summary>
    public static string SubHeader { get; set; } = "";
    /// <summary>
    /// Id 參數值
    /// </summary>
    public static string Id { get; set; } = "";
    /// <summary>
    /// Name 參數值
    /// </summary>
    public static string Name { get; set; } = "";
    /// <summary>
    /// Row Id 參數值
    /// </summary>
    public static int RowId { get; set; } = 0;
    /// <summary>
    /// 已選取的 Row Id 參數值
    /// </summary>
    public static int SelectedId { get; set; } = 0;
    /// <summary>
    /// SearchText
    /// </summary>
    public static string SearchText { get; set; } = "";
    /// <summary>
    /// 是否為排序模式
    /// </summary>
    public static bool SortMode { get; set; } = true;
    /// <summary>
    /// 排序欄位
    /// </summary>
    public static string SortColumn { get; set; } = "";
    /// <summary>
    /// 排序方式
    /// </summary>
    public static enSortDirection SortDirection { get; set; } = enSortDirection.ASC;
    /// <summary>
    /// FormId Id 參數值
    /// </summary>
    public static int FormId { get; set; } = 0;
    /// <summary>
    /// No 參數值
    /// </summary>
    public static string No { get; set; } = "";
    /// <summary>
    /// Pno 參數值
    /// </summary>
    public static string Pno { get; set; } = "";
    /// <summary>
    /// Tag1 參數值
    /// </summary>
    public static string Tag1 { get; set; } = "";
    /// <summary>
    /// Tag2 參數值
    /// </summary>
    public static string Tag2 { get; set; } = "";
    /// <summary>
    /// Tag3 參數值
    /// </summary>
    public static string Tag3 { get; set; } = "";
    /// <summary>
    /// Tag4 參數值
    /// </summary>
    public static string Tag4 { get; set; } = "";
    /// <summary>
    /// 區域名稱
    /// </summary>
    public static string Area { get; set; }
    /// <summary>
    /// 控制器名稱
    /// </summary>
    public static string Controller { get; set; }
    /// <summary>
    /// 動作名稱
    /// </summary>
    public static string Action { get; set; }
    /// <summary>
    /// 參數名稱
    /// </summary>
    public static string ParameterName { get; set; }
    /// <summary>
    /// 參數值
    /// </summary>
    public static string ParameterValue { get; set; }
    /// <summary>
    /// 總頁數
    /// </summary>
    public static int PageCount { get; set; }
    /// <summary>
    /// 目前頁數
    /// </summary>
    public static int PageNumber { get; set; }
    /// <summary>
    /// 程式是否合法
    /// </summary>
    public static bool IsValid
    {
        get
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(PrgNo)) isValid = false;
            if (string.IsNullOrEmpty(Controller)) isValid = false;
            if (string.IsNullOrEmpty(Action)) isValid = false;
            return isValid;
        }
    }
    /// <summary>
    /// 卡片寛度
    /// </summary>
    public static enCardSize CardSize { get; set; }
    /// <summary>
    /// 卡片寛度的 CSS 名稱
    /// </summary>
    public static string CardSizeCss
    {
        get
        {
            string str_cass = "card-size-max";
            if (CardSize == enCardSize.Small) str_cass = "card-size-small";
            if (CardSize == enCardSize.Medium) str_cass = "card-size-medium";
            if (CardSize == enCardSize.Large) str_cass = "card-size-large";
            if (CardSize == enCardSize.Max) str_cass = "card-size-max";
            return str_cass;
        }
    }
    public static bool PrgInit(string prgNo)
    {
        using (z_repoPrograms repos = new z_repoPrograms())
        {
            SubHeader = "";
            SelectedId = 0;
            var data = repos.GetData(UserService.RoleNo, prgNo);
            if (data == null) return false;
            prgNo = data.PrgNo;
            PrgName = data.PrgName;
            return true;
        }
    }

    /// <summary>
    /// 初始化作業
    /// </summary>
    public static void Init()
    {
        Init("", enCardSize.Max);
    }
    /// <summary>
    /// 初始化作業
    /// </summary>
    /// <param name="prgName">程式名稱</param>
    public static void Init(string prgName)
    {
        Init();
        PrgName = prgName;
    }
    /// <summary>
    /// 初始化作業
    /// </summary>
    /// <param name="prgName">程式名稱</param>
    /// <param name="actionName">動作名稱</param>
    /// <param name="cardSize">卡片寛度</param>
    public static void Init(string prgName, enAction actionName, enCardSize cardSize)
    {
        Init("", cardSize);
        PrgName = prgName;
        Action = ActionService.GetActionName(actionName);
    }
    /// <summary>
    /// 初始化作業
    /// </summary>
    /// <param name="prgName">程式名稱</param>
    /// <param name="actionName">動作名稱</param>
    /// <param name="cardSize">卡片寛度</param>
    public static void Init(string prgName, string actionName, enCardSize cardSize)
    {
        Init("", cardSize);
        PrgName = prgName;
        Action = actionName;
    }
    /// <summary>
    /// 初始化作業
    /// </summary>
    /// <param name="prgNo">程式編號</param>
    /// <param name="prgName">程式名稱</param>
    /// <param name="controllerName">控制器名稱</param>
    /// <param name="actionName">動作名稱</param>
    public static void Init(string prgNo, string prgName, string areaName, string controllerName, string actionName, enCardSize cardSize)
    {
        Init(prgNo, cardSize);
        RoleNo = UserService.RoleNo;
        PrgNo = prgNo;
        PrgName = prgName;
        Area = areaName;
        Controller = controllerName;
        Action = actionName;
        ParameterName = "";
        ParameterValue = "";
    }
    /// <summary>
    /// 設定程式代號
    /// </summary>
    /// <param name="prgNo">程式代號</param>
    /// <param name="prgName">程式名稱</param>
    public static void SetProgramNoName(string prgNo, string prgName)
    {
        RoleNo = ActionService.Area;
        PrgNo = prgNo;
        PrgName = prgName;
    }

    /// <summary>
    /// 自定程式名稱
    /// </summary>
    /// <param name="subHeader">自定程式名稱</param>
    public static void SetProgram(string subHeader)
    {
        SubHeader = subHeader;
    }

    /// <summary>
    /// 設定程式代號
    /// </summary>
    /// <param name="prgNo">程式代號</param>
    public static void SetProgram(string roleNo, string prgNo)
    {
        using (z_repoPrograms repos = new z_repoPrograms())
        {
            var data = repos.GetData(roleNo, prgNo);
            if (data != null)
            {
                RoleNo = roleNo;
                PrgName = data.PrgName;
                Area = data.AreaName;
                Controller = data.ControllerName;
                Action = data.ActionName;
                ParameterName = "id";
                ParameterValue = data.ParmValue;
                SubHeader = "";
            }
            else
            {
                SubHeader = prgNo;
            }
        }
    }

    /// <summary>
    /// 初始化作業
    /// </summary>
    /// <param name="prgNo">程式編號</param>
    /// <param name="cardSize">卡片寛度</param>
    public static void Init(string prgNo, enCardSize cardSize)
    {
        using (z_repoPrograms prg = new z_repoPrograms())
        {
            RoleNo = UserService.RoleNo;
            PrgNo = prgNo;
            PrgName = "";
            SubHeader = "";
            Id = "";
            Name = "";
            RowId = 0;
            FormId = 0;
            Controller = "";
            Area = "";
            Action = "";
            ParameterName = "";
            ParameterValue = "";
            var data = prg.GetData(UserService.RoleNo, prgNo);
            if (data != null)
            {
                PrgName = data.PrgName;
                Area = data.AreaName;
                Controller = data.ControllerName;
                Action = data.ActionName;
                ParameterName = "id";
                ParameterValue = data.ParmValue;
            }
            CardSize = cardSize;
        }
    }

    /// <summary>
    /// 設定動作資訊
    /// </summary>
    /// <param name="actionName">動作名稱</param>
    /// <param name="cardSize">卡片寛度</param>
    public static void SetAction(enAction actionName)
    {
        Action = ActionService.GetActionName(actionName);
    }

    /// <summary>
    /// 設定程式資訊
    /// </summary>
    public static void SetProgram()
    {
        using (z_repoPrograms prg = new z_repoPrograms())
        {
            string str_prg_no = ActionService.Controller.Split('_')[0];
            string str_module_no = "";
            if (string.IsNullOrEmpty(str_prg_no)) str_prg_no = "Dashboard";
            var data = prg.repo.ReadSingle(m => m.PrgNo == str_prg_no);
            if (data != null) str_module_no = data.ModuleNo;
            RoleNo = UserService.RoleNo;
            ModuleNo = str_module_no;
            PrgNo = str_prg_no;
            PrgName = (data.PrgName == null) ? "未命名" : data.PrgName;
            SubHeader = "";
        }
    }

    /// <summary>
    /// 設定程式資訊
    /// </summary>
    /// <param name="prgNo">程式代號</param>
    /// <param name="prgName">程式名稱</param>
    public static void SetProgram(string roleNo, string prgNo, string prgName)
    {
        using (z_repoPrograms prg = new z_repoPrograms())
        {
            string str_module_no = "";
            if (string.IsNullOrEmpty(prgNo)) prgNo = "Dashboard";
            var data = prg.repo.ReadSingle(m => m.PrgNo == prgNo);
            if (data != null) str_module_no = data.ModuleNo;
            RoleNo = roleNo;
            ModuleNo = str_module_no;
            PrgNo = prgNo;
            PrgName = prgName;
            SubHeader = "";
        }
    }

    /// <summary>
    /// 設定動作資訊
    /// </summary>
    /// <param name="actionName">動作名稱</param>
    /// <param name="cardSize">卡片寛度</param>
    public static void SetAction(enAction actionName, enCardSize cardSize)
    {
        Action = ActionService.GetActionName(actionName);
        CardSize = cardSize;
    }

    /// <summary>
    /// 設定動作資訊
    /// </summary>
    /// <param name="actionName">動作名稱</param>
    /// <param name="cardSize">卡片寛度</param>
    public static void SetAction(string actionName, enCardSize cardSize)
    {
        Action = actionName;
        CardSize = cardSize;
    }

    /// <summary>
    /// 設定動作資訊及頁數資訊
    /// </summary>
    /// <param name="actionName">動作名稱</param>
    /// <param name="cardSize">卡片寛度</param>
    /// <param name="pageNumber">目前頁數</param>
    /// <param name="pageCount">總頁數</param>
    public static void SetAction(enAction actionName, enCardSize cardSize, int pageNumber, int pageCount)
    {
        Action = ActionService.GetActionName(actionName);
        CardSize = cardSize;
        PageNumber = pageNumber;
        PageCount = pageCount;
    }

    /// <summary>
    /// 設定動作資訊及頁數資訊
    /// </summary>
    /// <param name="prgName">程式名稱</param>
    /// <param name="actionName">動作名稱</param>
    /// <param name="cardSize">卡片寛度</param>
    /// <param name="pageNumber">目前頁數</param>
    /// <param name="pageCount">總頁數</param>
    public static void SetAction(string prgName, enAction actionName, enCardSize cardSize, int pageNumber, int pageCount)
    {
        PrgName = prgName;
        Action = ActionService.GetActionName(actionName);
        CardSize = cardSize;
        PageNumber = pageNumber;
        PageCount = pageCount;
    }

    /// <summary>
    /// 設定動作資訊及頁數資訊
    /// </summary>
    /// <param name="actionName">動作名稱</param>
    /// <param name="cardSize">卡片寛度</param>
    /// <param name="pageNumber">目前頁數</param>
    /// <param name="pageCount">總頁數</param>
    public static void SetAction(string actionName, enCardSize cardSize, int pageNumber, int pageCount)
    {
        Action = actionName;
        CardSize = cardSize;
        PageNumber = pageNumber;
        PageCount = pageCount;
    }

    public static void SetProgramSecurity()
    {
        using (z_repoSecuritys repos = new z_repoSecuritys())
        {
            repos.SetUserSecurity(PrgNo);
        }
    }

    public static bool IsProgramSecurity(enSecurtyMode securtyMode)
    {
        return IsProgramSecurity(securtyMode, 0);
    }

    public static bool IsProgramSecurity(enSecurtyMode securtyMode, int rowId)
    {
        //除錯模式
        if (AppService.DebugMode) return true;

        //沒有程式權限即不通過驗證 
        if (securtyMode == enSecurtyMode.None) return true;
        if (securtyMode == enSecurtyMode.Index && UserService.UserSecurity.Id < 0) return true;
        if (securtyMode == enSecurtyMode.Add && UserService.UserSecurity.IsAdd == false) return false;
        if (securtyMode == enSecurtyMode.CreateEdit && rowId == 0 && UserService.UserSecurity.IsAdd == false) return false;
        if (securtyMode == enSecurtyMode.CreateEdit && rowId > 0 && UserService.UserSecurity.IsEdit == false) return false;
        if (securtyMode == enSecurtyMode.Confirm && UserService.UserSecurity.IsConfirm == false) return false;
        if (securtyMode == enSecurtyMode.Invalid && UserService.UserSecurity.IsInvalid == false) return false;
        if (securtyMode == enSecurtyMode.Delete && UserService.UserSecurity.IsDelete == false) return false;
        if (securtyMode == enSecurtyMode.Download && UserService.UserSecurity.IsDownload == false) return false;
        if (securtyMode == enSecurtyMode.Edit && UserService.UserSecurity.IsEdit == false) return false;
        if (securtyMode == enSecurtyMode.Print && UserService.UserSecurity.IsPrint == false) return false;
        if (securtyMode == enSecurtyMode.Undo && UserService.UserSecurity.IsUndo == false) return false;
        if (securtyMode == enSecurtyMode.Upload && UserService.UserSecurity.IsUpload == false) return false;
        return true;
    }

    /// <summary>
    /// 取得欄位排序圖示
    /// </summary>
    /// <returns></returns>
    public static string GetSortIcon(string columnName)
    {
        string str_icon = "";
        if (SortMode)
        {
            //str_icon = "◆";
            if (SortColumn == columnName)
            {
                if (SortDirection == enSortDirection.ASC) str_icon = "▲";
                if (SortDirection == enSortDirection.DESC) str_icon = "▼";
            }
        }
        return str_icon;
    }
}
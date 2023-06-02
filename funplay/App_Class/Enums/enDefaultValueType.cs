using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 預設值類型
/// </summary>
public enum enDefaultValueType
{
    /// <summary>
    /// 使用者自定義預設值
    /// </summary>
    String_Custom,
    /// <summary>
    /// 登入帳號
    /// </summary>
    String_UserNo,
    /// <summary>
    /// 登入姓名
    /// </summary>
    String_UserName,
    /// <summary>
    /// 登入角色代號
    /// </summary>
    String_RoleNo,
    /// <summary>
    /// 登入角色名稱
    /// </summary>
    String_RoleName,
    /// <summary>
    /// 空白
    /// </summary>
    String_Space,
    /// <summary>
    /// 公司代號
    /// </summary>
    String_CompanyNo,
    /// <summary>
    /// 公司名稱
    /// </summary>
    String_CompanyName,
    /// <summary>
    /// 公司簡稱
    /// </summary>
    String_CompanyShortName,
    /// <summary>
    /// Y
    /// </summary>
    String_Y,
    /// <summary>
    /// N
    /// </summary>
    String_N,
    /// <summary>
    /// Guid
    /// </summary>
    String_Guid,
    /// <summary>
    /// 使用者自定義預設值
    /// </summary>
    Int_Custom,
    /// <summary>
    /// 0
    /// </summary>
    Int_0,
    /// <summary>
    /// 1
    /// </summary>
    Int_1,
    /// <summary>
    /// 10
    /// </summary>
    Int_10,
    /// <summary>
    /// 100
    /// </summary>
    Int_100,
    /// <summary>
    /// 今年
    /// </summary>
    Int_Year,
    /// <summary>
    /// 本月
    /// </summary>
    Int_Month,
    /// <summary>
    /// 今日
    /// </summary>
    Int_Day,
    /// <summary>
    /// 使用者自定義預設值
    /// </summary>
    Decimal_Custom,
    /// <summary>
    /// 0
    /// </summary>
    Decimal_0,
    /// <summary>
    /// 1
    /// </summary>
    Decimal_1,
    /// <summary>
    /// 10
    /// </summary>
    Decimal_10,
    /// <summary>
    /// 100
    /// </summary>
    Decimal_100,
    /// <summary>
    /// 使用者自定義預設值
    /// </summary>
    Date_Custom,
    /// <summary>
    /// 系統日期
    /// </summary>
    Date_Today,
    /// <summary>
    /// 系統日期時間
    /// </summary>
    Date_Now,
    /// <summary>
    /// 系統時間
    /// </summary>
    Date_NowTime,
    /// <summary>
    /// 今年第一天
    /// </summary>
    Date_ThisYearFirst,
    /// <summary>
    /// 今年最後一天
    /// </summary>
    Date_ThisYearLast,
    /// <summary>
    /// 去年第一天
    /// </summary>
    Date_PriorYearFirst,
    /// <summary>
    /// 去年最後一天
    /// </summary>
    Date_PriorYearLast,
    /// <summary>
    /// 明年第一天
    /// </summary>
    Date_NextYearFirst,
    /// <summary>
    /// 明年最後一天
    /// </summary>
    Date_NextYearLast,
    /// <summary>
    /// 本月第一天
    /// </summary>
    Date_ThisMonthFirst,
    /// <summary>
    /// 本月最後一天
    /// </summary>
    Date_ThisMonthLast,
    /// <summary>
    /// 上月第一天
    /// </summary>
    Date_PriorMonthFirst,
    /// <summary>
    /// 上月最後一天
    /// </summary>
    Date_PriorMonthLast,
    /// <summary>
    /// 下月第一天
    /// </summary>
    Date_NextMonthFirst,
    /// <summary>
    /// 下月最後一天
    /// </summary>
    Date_NextMonthLast,
    /// <summary>
    /// 今天起算加幾天, 幾天用 DefaultValue 設定 , 1 = 加 1 天 , -1 = 減 1 天
    /// </summary>
    Date_AddDays,
    /// <summary>
    /// 使用者自定義預設值
    /// </summary>
    Boolean_Custom,
    /// <summary>
    /// 真
    /// </summary>
    Boolean_True,
    /// <summary>
    /// 假
    /// </summary>
    Boolean_False

}
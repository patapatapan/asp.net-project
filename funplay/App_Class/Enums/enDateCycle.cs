using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 時間週期枚舉類型
/// </summary>
public enum enDateCycle
{
    /// <summary>
    /// 年
    /// </summary>
    Year,
    /// <summary>
    /// 月
    /// </summary>
    Month,
    /// <summary>
    /// 日
    /// </summary>
    Day,
    /// <summary>
    /// 時
    /// </summary>
    Hour,
    /// <summary>
    /// 分
    /// </summary>
    Minute,
    /// <summary>
    /// 秒
    /// </summary>
    Second,
    /// <summary>
    /// 本年一整年
    /// </summary>
    FullYear,
    /// <summary>
    /// 去年一整年
    /// </summary>
    PriorFullYear,
    /// <summary>
    /// 明年一整年
    /// </summary>
    NextFullYear,
    /// <summary>
    /// 本月一整月
    /// </summary>
    FullMonth,
    /// <summary>
    /// 上月一整月
    /// </summary>
    PriorFullMonth,
    /// <summary>
    /// 下月一整月
    /// </summary>
    NextFullMonth
}
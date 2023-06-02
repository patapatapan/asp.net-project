using Dapper;
using funplay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// 檢查日期範圍驗證
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class DateRangeAttribute : ValidationAttribute
{
    public int PriorDays { get; set; } = 0;
    public int NextDays { get; set; } = 0;
    public enDateCycle DateCycle { get; set; } = enDateCycle.Day;

    public DateRangeAttribute(enDateCycle dateCycle, int priorDays, int nextDays)
    {
        DateCycle = dateCycle;
        PriorDays = priorDays;
        NextDays = nextDays;
    }

    /// <summary>
    /// 檢查日期範圍驗證
    /// </summary>
    /// <param name="value">輸入值</param>
    /// <returns></returns>
    public override bool IsValid(object value)
    {
        if (value == null)
        {
            ErrorMessage = "輸入日期不可空白!!";
            return false;
        }
        string str_value = value.ToString();
        DateTime dtm_date = DateTime.MinValue;
        if (!DateTime.TryParse(str_value, out dtm_date)) dtm_date = DateTime.MinValue;
        if (dtm_date == DateTime.MinValue)
        {
            ErrorMessage = "輸入日期格式不正確!!";
            return false;
        }
        DateTime dtm_first = DateTime.Today.ezMonthFirstDate();
        DateTime dtm_start = DateTime.MinValue;
        DateTime dtm_end = DateTime.MinValue;
        if (DateCycle == enDateCycle.Year) { dtm_start = DateTime.Today.AddYears(PriorDays); dtm_end = DateTime.Today.AddYears(NextDays); }
        if (DateCycle == enDateCycle.Month) { dtm_start = DateTime.Today.AddMonths(PriorDays); dtm_end = DateTime.Today.AddMonths(NextDays); }
        if (DateCycle == enDateCycle.Day) { dtm_start = DateTime.Today.AddDays(PriorDays); dtm_end = DateTime.Today.AddDays(NextDays); }
        if (DateCycle == enDateCycle.Hour) { dtm_start = DateTime.Today.AddHours(PriorDays); dtm_end = DateTime.Today.AddHours(NextDays); }
        if (DateCycle == enDateCycle.Minute) { dtm_start = DateTime.Today.AddMinutes(PriorDays); dtm_end = DateTime.Today.AddMinutes(NextDays); }
        if (DateCycle == enDateCycle.Second) { dtm_start = DateTime.Today.AddSeconds(PriorDays); dtm_end = DateTime.Today.AddSeconds(NextDays); }
        if (DateCycle == enDateCycle.FullYear) { dtm_start = dtm_first.ezYearFirstDate(); dtm_end = dtm_first.ezYearLastDate(); }
        if (DateCycle == enDateCycle.PriorFullYear) { dtm_start = dtm_first.AddYears(-1).ezYearFirstDate(); dtm_end = dtm_first.AddYears(-1).ezYearFirstDate(); }
        if (DateCycle == enDateCycle.NextFullYear) { dtm_start = dtm_first.AddYears(1).ezYearFirstDate(); dtm_end = dtm_first.AddYears(1).ezYearFirstDate(); }
        if (DateCycle == enDateCycle.FullMonth) { dtm_start = dtm_first.ezMonthFirstDate(); dtm_end = dtm_first.ezMonthLastDate(); }
        if (DateCycle == enDateCycle.PriorFullMonth) { dtm_start = dtm_first.AddMonths(-1).ezMonthFirstDate(); dtm_end = dtm_first.AddMonths(-1).ezMonthLastDate(); }
        if (DateCycle == enDateCycle.NextFullMonth) { dtm_start = dtm_first.AddMonths(1).ezMonthFirstDate(); dtm_end = dtm_first.AddMonths(1).ezMonthLastDate(); }
        if (dtm_date < dtm_start || dtm_date > dtm_end)
        {
            if (DateCycle == enDateCycle.Hour || DateCycle == enDateCycle.Minute || DateCycle == enDateCycle.Second)
                ErrorMessage = $"輸入日期 : {dtm_date.ToString("yyyy/MM/dd HH:mm:ss")} 需在日期範圍 {dtm_start.ToString("yyyy/MM/dd HH:mm:ss")} - {dtm_end.ToString("yyyy/MM/dd HH:mm:ss")} 內!!";
            else
                ErrorMessage = $"輸入日期 : {dtm_date.ToString("yyyy/MM/dd")} 需在日期範圍 {dtm_start.ToString("yyyy/MM/dd")} - {dtm_end.ToString("yyyy/MM/dd")} 內!!";
            return false;
        }
        return true;
    }
}
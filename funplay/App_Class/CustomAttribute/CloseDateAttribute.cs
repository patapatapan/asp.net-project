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
/// 檢查結帳日期驗證
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class CloseDateAttribute : ValidationAttribute
{
    /// <summary>
    /// 關帳系統代碼
    /// </summary>
    public string CodeNo { get; set; }

    /// <summary>
    /// 檢查結帳日期驗證
    /// </summary>
    /// <param name="codeNo">關帳系統代碼</param>
    public CloseDateAttribute(string codeNo)
    {
        CodeNo = codeNo;
    }

    /// <summary>
    /// 判斷是否符合驗證
    /// </summary>
    /// <param name="value">輸入值</param>
    /// <returns></returns>
    public override bool IsValid(object value)
    {
        using (z_repoCloseDates repos = new z_repoCloseDates())
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
            var model = repos.repo
                .ReadAll(m => m.CodeNo == CodeNo)
                .OrderByDescending(m => m.StartDate)
                .FirstOrDefault();
            if (model == null) return true;
            DateTime dtm_start = model.StartDate;
            DateTime dtm_end = model.EndDate;
            if (dtm_date < dtm_start || dtm_date > dtm_end)
            {
                ErrorMessage = $"輸入日期 : {dtm_date.ToString("yyyy/MM/dd")} 需在關帳日期 {dtm_start.ToString("yyyy/MM/dd")} - {dtm_end.ToString("yyyy/MM/dd")} 內!!";
                return false;
            }
            return true;
        }
    }
}
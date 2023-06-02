using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public static class HtmlTagHelper
{
    /// <summary>
    /// 強化 Html HyperLink Tag
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="url">網址</param>
    /// <param name="innerHtml">文字</param>
    /// <param name="htmlAttributes">屬性</param>
    /// <param name="dataAttributes">參數</param>
    /// <returns>
    /// @Html.HyperLinkHelper("http://www.example.com", "Example!")
    /// @Html.HyperLinkHelper("http://www.example.com", "Example!", new { title = "Example" })
    /// @Html.HyperLinkHelper("http://www.example.com", "Example!", new { target = "_blank" }, new { id = 1 })
    /// </returns>
    public static MvcHtmlString HyperLinkHelper(this HtmlHelper htmlHelper, string url, object innerHtml, object htmlAttributes = null, object dataAttributes = null)
    {
        var link = new TagBuilder("a");
        link.MergeAttribute("href", url);
        link.InnerHtml = innerHtml.ToString();
        link.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

        if (dataAttributes != null)
        {
            var values = new RouteValueDictionary(dataAttributes);
            foreach (var value in values)
            {
                link.MergeAttribute("data-" + value.Key, value.Value.ToString());
            }
        }
        return MvcHtmlString.Create(link.ToString(TagRenderMode.Normal));
    }

    /// <summary>
    /// 標題文字加上排序圖示
    /// 範例:
    /// View:
    /// var sortUrl = Url.Action(ActionService.Sort, ActionService.Controller, new { area = ActionService.Area });
    /// @Html.LabelIconFor(sortUrl, "prod_no" , Html.LabelFor(model => model.Detail.First().prod_no).ToString())
    /// ---------------------------
    /// Controller:
    /// public ActionResult Sort(string id)
    /// {
    ///     PrgService.SetSort(id);
    ///     return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area});
    /// }
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="sortUrl"></param>
    /// <param name="columnName"></param>
    /// <param name="labelName"></param>
    /// <returns></returns>
    public static MvcHtmlString LabelIconFor(this HtmlHelper htmlHelper, string sortUrl, string columnName, string labelName)
    {
        string str_url = string.Format("{0}/{1}", sortUrl, columnName);
        string str_icon = PrgService.GetSortIcon(columnName);
        var link = new TagBuilder("a");
        link.MergeAttribute("href", str_url);
        link.InnerHtml = labelName + str_icon;
        return MvcHtmlString.Create(link.ToString(TagRenderMode.Normal));
    }

    /// <summary>
    /// 資料在資料庫中有斷行符號，但從controller撈出後View的呈現卻沒辦法呈現斷行
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="html"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
    {
        var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
        var model = html.Encode(metadata.Model).Replace("\r", "").Replace("\n", "<br />");
        if (String.IsNullOrEmpty(model)) return MvcHtmlString.Empty; return MvcHtmlString.Create(model);
    }
}

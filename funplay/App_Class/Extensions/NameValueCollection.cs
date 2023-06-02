using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 擴充方法
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// Permutation
    /// </summary>
    /// <param name="optionValueSet">選項值</param>
    /// <returns></returns>
    public static IEnumerable<NameValueCollection> ezPermutation
        (this IDictionary<string, IEnumerable<string>> optionValueSet)
    {
        var candicateKeys = new Stack<string>(optionValueSet.Keys);
        foreach (NameValueCollection nvc in optionValueSet.ezPermutation(candicateKeys))
        {
            yield return nvc;
        }
    }

    private static IEnumerable<NameValueCollection> ezPermutation
        (this IDictionary<string, IEnumerable<string>> optionValueSet, Stack<string> candicateKeys)
    {
        string key = candicateKeys.Pop();
        IEnumerable<string> values = optionValueSet[key];

        if (candicateKeys.Count > 0)
        {
            foreach (NameValueCollection nvc in optionValueSet.ezPermutation(candicateKeys))
            {
                foreach (string value in values)
                {
                    nvc[key] = value;
                    yield return nvc;
                }
            }
        }
        else
        {
            foreach (string value in values)
            {
                NameValueCollection nvc = new NameValueCollection();
                nvc[key] = value;
                yield return nvc;
            }
        }
    }

    /// <summary>
    /// 查詢陣列總計組合數
    /// </summary>
    /// <param name="nvc"></param>
    /// <returns></returns>
    public static string ezTextContent(this NameValueCollection nvc)
    {
        StringBuilder sb = new StringBuilder();
        string sep = "";
        foreach (string key in nvc.Keys)
        {
            sb.Append(sep).Append(key).Append("=").Append(nvc[key]);
            sep = ", ";
        }
        return sb.ToString();

        // 範例：
        // var optionValueSet = new Dictionary<string , IEnumerable<string>>()
        // {</para>
        //    {"口味", new[] {"烏龍綠", "檸檬紅茶", "仙草蜜", "百香綠", "梅子綠"}},
        //    {"甜度", new[] {"正常", "少糖", "無糖"}},
        //    {"冰塊", new[] {"正常", "少冰", "無冰"}}
        // };
        // int count = 0;
        // foreach (NameValueCollection nvc in optionValueSet.Permutation())
        // {
        //     count++;
        //     Console.WriteLine(nvc.TextContent());
        // }
        // Console.WriteLine("共 " + count + " 種組合");
    }
}
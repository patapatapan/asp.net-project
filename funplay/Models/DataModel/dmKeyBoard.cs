using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class dmKeyBoard : BaseClass
{
    public List<string> KeyList1 { get; set; }
    public List<string> KeyList2 { get; set; }
    public List<string> KeyList3 { get; set; }
    public List<string> KeyList4 { get; set; }
    public dmKeyBoard()
    {
        KeyList1 = new List<string>() { "ㄅ", "ㄉ", " ", " ", "ㄓ", " ", " ", "ㄚ", "ㄞ", "ㄢ", "ㄦ" };
        KeyList2 = new List<string>() { "ㄆ", "ㄊ", "ㄍ", "ㄐ", "ㄔ", "ㄗ", "一", "ㄛ", "ㄟ", "ㄣ", " " };
        KeyList3 = new List<string>() { "ㄇ", "ㄋ", "ㄎ", "ㄑ", "ㄕ", "ㄘ", "ㄨ", "ㄚ", "ㄜ", "ㄠ", "ㄤ" };
        KeyList4 = new List<string>() { "ㄈ", "ㄌ", "ㄏ", "ㄒ", "ㄖ", "ㄙ", "ㄩ", "ㄝ", "ㄡ", "ㄥ", " " };
    }
}
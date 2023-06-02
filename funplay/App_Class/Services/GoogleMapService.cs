using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GoogleMapService : BaseClass
{
    #region 建構子
    /// <summary>
    /// GoogleMap 建構子
    /// </summary>
    public GoogleMapService()
    {
        using (z_repoApplications app = new z_repoApplications())
        {
            var model = app.GetEnabledApplication();
            GoogleMapKey = model.GoogleMapKey;
        }
    }
    #endregion
    #region 屬性
    /// <summary>
    /// 訊息文字
    /// </summary>
    public string GoogleMapKey { get; set; }
    #endregion
}
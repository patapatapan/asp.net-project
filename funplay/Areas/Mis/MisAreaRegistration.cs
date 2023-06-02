using System.Web.Mvc;

namespace funplay.Areas.Mis
{
    public class MisAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Mis";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Mis_default",
                "Mis/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "funplay.Areas.Mis.Controllers" }
            );
        }
    }
}
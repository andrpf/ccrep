using System.Web.Http;
using System.Web.Mvc;

namespace CcRep.Areas.System
{
    public class SystemAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "System";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                name: "systemApi",
                routeTemplate: "System/api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );
            context.MapRoute(
                "System_default",
                "System/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
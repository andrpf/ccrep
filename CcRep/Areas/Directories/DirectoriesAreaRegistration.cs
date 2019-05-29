using System.Web.Http;
using System.Web.Mvc;

namespace CcRep.Areas.Directories
{
    public class DirectoriesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Directories";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                name: "directoriesApi",
                routeTemplate: "Directories/api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );

            context.MapRoute(
                "Directories_default",
                "Directories/{controller}/{action}/{id}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
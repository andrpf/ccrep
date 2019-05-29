using System.Web.Http;
using System.Web.Mvc;

namespace CcRep.Areas.Reports
{
    public class ReportsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Reports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                name: "reportsApi",
                
                routeTemplate: "Reports/api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );
            context.MapRoute(
                "Report_default",
                "Reports/{controller}/{action}/{idRep}",
                new { action = "IndexCurrent" },
                new { controller = "Transactions", action = "IndexCurrent|IndexArch|IndexDupl" }
            );
            context.MapRoute(
                "Reports_default",
                "Reports/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            


        }
    }
}
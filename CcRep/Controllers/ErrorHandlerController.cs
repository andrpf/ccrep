using System.Web.Mvc;

namespace CcRep.Controllers
{
    public class ErrorHandlerController : Controller
    {
        // GET: ErrorHandler
        public ActionResult Index()
        {
            return Content("df");
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}
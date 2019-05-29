using CcRep.Components.Mailing;
using CcRep.Managers.Mail;
using CcRep.Models;
using CcRep.Models._dc;
using CcRep.Models._mail;
using CcRep.Models.dic;
using Ganss.XSS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CcRep.Controllers
{
    public class HomeController : Controller
    {
        CcRepContext db = new CcRepContext();

        private CcRepUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<CcRepUserManager>(); }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize]
        public async Task<ActionResult> ContactUs(ContactUs model)
        {
            var san = new HtmlSanitizer();
            
            var message = new Message();
            message.Title = san.Sanitize(model.Theme);
            message.Body = san.Sanitize(model.Body);

            var user = UserManager.FindById(User.Identity.GetUserId());

            var mailSender = new MailManager();

            bool result = await mailSender.SendMessageFromUser(user, message);

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
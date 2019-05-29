using CcRep.Areas.System.ViewModels;
using CcRep.Components.Auth;
using CcRep.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity.Validation;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CcRep.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private CcRepUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<CcRepUserManager>(); }
        }

        //Redirection to SSO ActiveDirectory-auth server
        public ActionResult Login(string returnUrl)
        {
            bool logged = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!logged)
            {
                SsoAuthProvider authProvider = GetAuthProvider(StateHashSource.GenerateNew);
                Uri redirectLink = authProvider.GenerateRedirToAuthUri();
                authProvider.setStateKeyToResponse();

                ViewBag.redirectUrl = redirectLink.ToString();
                return PartialView("Redir");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> LoginComplete(string code)
        {
            if (code == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SsoAuthProvider provider = GetAuthProvider(StateHashSource.FromRequest);

            string token = await provider.requestAccessTokenByAuthCode(code);

            if (token != null)
            {
                try
                {
                    ActiveDirctoryUser userInfo = await provider.RequestUserData(token);

                    CcRepUser user = await UserManager.FindByNameAsync(userInfo.username);

                    if (user == null)
                    {
                        user = new CcRepUser { UserName = userInfo.username, Email = userInfo.email, FullName = userInfo.name };

                        string userId = await UserManager.CreateWithClaims(user);
                    }

                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                    DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Home");
                }
                catch (CantGetUserInfo exception)
                {
                    throw new HttpException("Bad access token parameter - " + exception.Message);
                }
                catch (DbEntityValidationException exception)
                {
                    throw new HttpException($"Ошибка! ({exception.Message})");
                    //return Content(ObjectInfo.Print(exception.EntityValidationErrors).ToString());
                }
            }

            throw new HttpException("Wrong parameters!");
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([Bind(Include = "UserName, Locked")] AddUser modelView)
        {
            if (ModelState.IsValid)
            {
                CcRepUser ExistingUser = await UserManager.FindByNameAsync(modelView.UserName);

                if (ExistingUser != null)
                {
                    TempData["Flash"] = "Данный пользователь уже существует в базе данных";

                    return RedirectToAction("AddUser");
                }

                var provider = GetAuthProvider(null);

                var finded = await provider.RequestUserDataByName(modelView.UserName);

                if (finded is null)
                {
                    TempData["Flash"] = "Введенный пользователь не был найден в ActiveDirectory";

                    return RedirectToAction("AddUser");
                }


                CcRepUser NewUser = new CcRepUser()
                {
                    UserName = finded.username,
                    Locked = modelView.Locked,
                    Email = finded.email,
                    FullName = finded.name,
                    SecurityStamp = Guid.NewGuid().ToString()
                };


                string userId = await UserManager.CreateWithClaims(NewUser);

                return RedirectToAction("ManageUserAccess", "CcRepUsers", new { id = userId, Area = "System" });
                

            }

            return new HttpStatusCodeResult(404);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            AddUser model = new AddUser();

            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private SsoAuthProvider GetAuthProvider(StateHashSource ?stateLocation)
        {
            return new SsoAuthProvider(stateLocation.GetValueOrDefault())
            {
                AppKey = WebConfigurationManager.AppSettings["AppKey"],
                AppName = WebConfigurationManager.AppSettings["AppName"],
                AuthServHost = WebConfigurationManager.AppSettings["AuthServerHost"],
                AuthServPath = WebConfigurationManager.AppSettings["AuthServerPath"],
                AuthEndpointTokenPath = WebConfigurationManager.AppSettings["AuthEndpointToken"],
                AuthEndpointProfilePath = WebConfigurationManager.AppSettings["AuthEndpointProfile"],
                AuthEndpointUserFindPath = WebConfigurationManager.AppSettings["AuthEndpointUserFind"],
                redirectAfterAuthPath = WebConfigurationManager.AppSettings["backToAppRedirPath"],
            };
        }
    }
}
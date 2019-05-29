using CcRep.Areas.System.ViewModels;
using CcRep.Models;
using CcRep.Models._dc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CcRep.Areas.System.Controllers
{
    public class CcRepUsersController : Controller
    {
        private CcRepContext db = new CcRepContext();

        private CcRepUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<CcRepUserManager>(); }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public ActionResult ManageUserAccess(string Id)
        {
            using (db)
            {
                CcRepUser UserModel = db.Users.Find(Id);

                if (UserModel != null)
                {
                    UserAccessAssignment ViewModel = new UserAccessAssignment(UserModel, db);

                    return View(ViewModel);
                }
            }

            return new HttpStatusCodeResult(404);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserAccess([Bind(Include = "SelectedBranches, SelectedRoles, UserId, Blocked, FlAccess, PdAccess, AllBranches")] UserAccessAssignment modelView)
        {
            if (modelView.UserId != null)
            {
                CcRepUser userModel = db.Users.Find(modelView.UserId);
                userModel.Locked = modelView.Blocked;

                foreach (UsersToFilials branch in userModel.Filials.ToList())
                {
                    db.UserrsToFilials.Remove(branch);
                }
                foreach (CcRepUserClaim claim in userModel.Claims.ToList())
                {
                    db.UserClaims.Remove(claim);
                }
                foreach (UsersToRoles roleUser in userModel.Roles.ToList())
                {
                    db.UserRoles.Remove(roleUser);
                }
                UpdateModel(userModel);


                if (modelView.AllBranches)
                {
                    var AllBranClaim = new CcRepUserClaim { ClaimType = "showAllBranches", ClaimValue = modelView.AllBranches.ToString() };
                    userModel.Claims.Add(AllBranClaim);
                }
                else if (modelView.SelectedBranches != null)
                {
                    foreach (var c in db.Filials.Where(co => modelView.SelectedBranches.Contains(co.NCFilial)))
                    {
                        userModel.Filials.Add(new UsersToFilials() { FilialId = c.NCFilial, UserId = userModel.Id });
                    }
                }

                if (modelView.SelectedRoles != null)
                {
                    foreach (var c in db.Roles.Where(co => modelView.SelectedRoles.Contains(co.Id)))
                    {
                        UsersToRoles model = new UsersToRoles() { RoleId = c.Id, UserId = userModel.Id };
                        userModel.Roles.Add(model);
                    }
                }

                var FlAccessClaim = new CcRepUserClaim { ClaimType = "FlAccess", ClaimValue = modelView.FlAccess };
                var PdAccessClaim = new CcRepUserClaim { ClaimType = "PdAccess", ClaimValue = modelView.PdAccess };

                userModel.Claims.Add(FlAccessClaim);
                userModel.Claims.Add(PdAccessClaim);

                // сохраняем изменения
                UserManager.UpdateAsync(userModel);

                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();

                return Redirect(Request.UrlReferrer.ToString());
            }

            return new HttpStatusCodeResult(404);
        }

        // GET: System/CcRepUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: System/CcRepUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CcRepUser ccRepUser = db.Users.Find(id);
            if (ccRepUser == null)
            {
                return HttpNotFound();
            }
            return View(ccRepUser);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

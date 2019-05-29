using CcRep.Models._dc;
using CcRep.Models.dic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CcRep.Areas.Directories.Controllers
{
    public class AccountClientsController : Controller
    {
        private CcRepContext db = new CcRepContext();

        // GET: Directories/AccountClients
        public ActionResult Index()
        {
            return View(db.AccountClients.ToList());
        }

        // GET: Directories/AccountClients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountClient accountClient = db.AccountClients.Find(id);
            if (accountClient == null)
            {
                return HttpNotFound();
            }
            return View(accountClient);
        }

        // GET: Directories/AccountClients/Create
        public ActionResult Create()
        {
            var model = new AccountClient();
            return View(model);
        }

        // POST: Directories/AccountClients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CNum,CBAccount,Description,BegDate,EndDate")] AccountClient accountClient)
        {
            if (ModelState.IsValid)
            {
                db.AccountClients.Add(accountClient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountClient);
        }

        // GET: Directories/AccountClients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountClient accountClient = db.AccountClients.Find(id);
            if (accountClient == null)
            {
                return HttpNotFound();
            }
            return View(accountClient);
        }

        // POST: Directories/AccountClients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CNum,CBAccount,Description,BegDate,EndDate")] AccountClient accountClient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountClient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountClient);
        }

        // GET: Directories/AccountClients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountClient accountClient = db.AccountClients.Find(id);
            if (accountClient == null)
            {
                return HttpNotFound();
            }
            return View(accountClient);
        }

        // POST: Directories/AccountClients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountClient accountClient = db.AccountClients.Find(id);
            db.AccountClients.Remove(accountClient);
            db.SaveChanges();
            return RedirectToAction("Index");
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

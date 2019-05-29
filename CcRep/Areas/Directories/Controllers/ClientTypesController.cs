using CcRep.Models._dc;
using CcRep.Models.dic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CcRep.Areas.Directories.Controllers
{
    public class ClientTypesController : Controller
    {
        private CcRepContext db = new CcRepContext();

        // GET: Directories/ClientTypes
        public ActionResult Index()
        {
            return View(db.ClientTypes.ToList());
        }

        // GET: Directories/ClientTypes/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientType clientType = db.ClientTypes.Find(id);
            if (clientType == null)
            {
                return HttpNotFound();
            }
            return View(clientType);
        }

        // GET: Directories/ClientTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Directories/ClientTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameShort,Description")] ClientType clientType)
        {
            if (ModelState.IsValid)
            {
                db.ClientTypes.Add(clientType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clientType);
        }

        // GET: Directories/ClientTypes/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientType clientType = db.ClientTypes.Find(id);
            if (clientType == null)
            {
                return HttpNotFound();
            }
            return View(clientType);
        }

        // POST: Directories/ClientTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameShort,Description")] ClientType clientType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientType);
        }

        // GET: Directories/ClientTypes/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientType clientType = db.ClientTypes.Find(id);
            if (clientType == null)
            {
                return HttpNotFound();
            }
            return View(clientType);
        }

        // POST: Directories/ClientTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            ClientType clientType = db.ClientTypes.Find(id);
            db.ClientTypes.Remove(clientType);
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

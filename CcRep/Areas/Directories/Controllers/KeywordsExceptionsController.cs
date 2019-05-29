using CcRep.Models._dc;
using CcRep.Models.dic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CcRep.Areas.Directories.Controllers
{
    public class KeywordsExceptionsController : Controller
    {
        private CcRepContext db = new CcRepContext();

        // GET: Directories/KeywordsExceptions
        public ActionResult Index()
        {
            return View(db.KeywordsExceptions.ToList());
        }

        // GET: Directories/KeywordsExceptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeywordsException keywordsException = db.KeywordsExceptions.Find(id);
            if (keywordsException == null)
            {
                return HttpNotFound();
            }
            return View(keywordsException);
        }

        // GET: Directories/KeywordsExceptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Directories/KeywordsExceptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Keyword")] KeywordsException keywordsException)
        {
            if (ModelState.IsValid)
            {
                db.KeywordsExceptions.Add(keywordsException);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(keywordsException);
        }

        // GET: Directories/KeywordsExceptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeywordsException keywordsException = db.KeywordsExceptions.Find(id);
            if (keywordsException == null)
            {
                return HttpNotFound();
            }
            return View(keywordsException);
        }

        // POST: Directories/KeywordsExceptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Keyword")] KeywordsException keywordsException)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keywordsException).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keywordsException);
        }

        // GET: Directories/KeywordsExceptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeywordsException keywordsException = db.KeywordsExceptions.Find(id);
            if (keywordsException == null)
            {
                return HttpNotFound();
            }
            return View(keywordsException);
        }

        // POST: Directories/KeywordsExceptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KeywordsException keywordsException = db.KeywordsExceptions.Find(id);
            db.KeywordsExceptions.Remove(keywordsException);
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

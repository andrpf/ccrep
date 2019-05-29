using CcRep.Models._dc;
using CcRep.Models.dic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CcRep.Areas.Directories.Controllers
{
    public class KeywordsController : Controller
    {
        private CcRepContext db = new CcRepContext();

        // GET: Directories/Keywords
        public ActionResult Index()
        {
            return View(db.Keywords.ToList());
        }

        // GET: Directories/Keywords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = db.Keywords.Find(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // GET: Directories/Keywords/Create
        public ActionResult Create()
        {
            var model = new Keyword();
            return View(model);
        }

        // POST: Directories/Keywords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Key_word,Description,BegDate,EndDate")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                db.Keywords.Add(keyword);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(keyword);
        }

        // GET: Directories/Keywords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = db.Keywords.Find(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: Directories/Keywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Key_word,Description,BegDate,EndDate")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyword).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keyword);
        }

        // GET: Directories/Keywords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = db.Keywords.Find(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: Directories/Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Keyword keyword = db.Keywords.Find(id);
            db.Keywords.Remove(keyword);
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

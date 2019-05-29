using CcRep.Models._dc;
using CcRep.Models.dic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CcRep.Areas.Directories.Controllers
{
    public class CodeToolingsController : Controller
    {
        private CcRepContext db = new CcRepContext();

        // GET: Directories/CodeToolings
        public ActionResult Index()
        {
            return View(db.CodeToolings.ToList());
        }

        // GET: Directories/CodeToolings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeTooling codeTooling = db.CodeToolings.Find(id);
            if (codeTooling == null)
            {
                return HttpNotFound();
            }
            return View(codeTooling);
        }

        // GET: Directories/CodeToolings/Create
        public ActionResult Create()
        {
            var model = new CodeTooling();
            return View(model);
        }

        // POST: Directories/CodeToolings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeTooling,DescTooling,BegDate,EndDate")] CodeTooling codeTooling)
        {
            if (ModelState.IsValid)
            {
                db.CodeToolings.Add(codeTooling);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(codeTooling);
        }

        // GET: Directories/CodeToolings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeTooling codeTooling = db.CodeToolings.Find(id);
            if (codeTooling == null)
            {
                return HttpNotFound();
            }
            return View(codeTooling);
        }

        // POST: Directories/CodeToolings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeTooling,DescTooling,BegDate,EndDate")] CodeTooling codeTooling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codeTooling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codeTooling);
        }

        // GET: Directories/CodeToolings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeTooling codeTooling = db.CodeToolings.Find(id);
            if (codeTooling == null)
            {
                return HttpNotFound();
            }
            return View(codeTooling);
        }

        // POST: Directories/CodeToolings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodeTooling codeTooling = db.CodeToolings.Find(id);
            db.CodeToolings.Remove(codeTooling);
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

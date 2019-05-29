using CcRep.Models._dc;
using CcRep.Models.dic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CcRep.Areas.Directories.Controllers
{
    public class PDTypesController : Controller
    {
        private CcRepContext db = new CcRepContext();

        // GET: Directories/PDTypes
        public ActionResult Index()
        {
            return View(db.PDTypes.ToList());
        }

        // GET: Directories/PDTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PDType pDType = db.PDTypes.Find(id);
            if (pDType == null)
            {
                return HttpNotFound();
            }
            return View(pDType);
        }

        // GET: Directories/PDTypes/Create
        public ActionResult Create()
        {
            var model = new PDType();

            return View(model);
        }

        // POST: Directories/PDTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodePD,DescCodePD,BegDate,EndDate")] PDType pDType)
        {
            if (ModelState.IsValid)
            {
                db.PDTypes.Add(pDType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pDType);
        }

        // GET: Directories/PDTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PDType pDType = db.PDTypes.Find(id);
            if (pDType == null)
            {
                return HttpNotFound();
            }
            return View(pDType);
        }

        // POST: Directories/PDTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodePD,DescCodePD,BegDate,EndDate")] PDType pDType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pDType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pDType);
        }

        // GET: Directories/PDTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PDType pDType = db.PDTypes.Find(id);
            if (pDType == null)
            {
                return HttpNotFound();
            }
            return View(pDType);
        }

        // POST: Directories/PDTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PDType pDType = db.PDTypes.Find(id);
            db.PDTypes.Remove(pDType);
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

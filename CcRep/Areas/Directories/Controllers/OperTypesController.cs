using CcRep.Models._dc;
using CcRep.Models.dic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CcRep.Areas.Directories.Controllers
{
    public class OperTypesController : Controller
    {
        private CcRepContext db = new CcRepContext();

        // GET: Directories/OperTypes
        public ActionResult Index()
        {
            return View(db.OperTypes.ToList());
        }

        // GET: Directories/OperTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperType operType = db.OperTypes.Find(id);
            if (operType == null)
            {
                return HttpNotFound();
            }
            return View(operType);
        }

        // GET: Directories/OperTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Directories/OperTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Oper_Type,OperTypeDesc")] OperType operType)
        {
            if (ModelState.IsValid)
            {
                db.OperTypes.Add(operType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(operType);
        }

        // GET: Directories/OperTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperType operType = db.OperTypes.Find(id);
            if (operType == null)
            {
                return HttpNotFound();
            }
            return View(operType);
        }

        // POST: Directories/OperTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Oper_Type,OperTypeDesc")] OperType operType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(operType);
        }

        // GET: Directories/OperTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperType operType = db.OperTypes.Find(id);
            if (operType == null)
            {
                return HttpNotFound();
            }
            return View(operType);
        }

        // POST: Directories/OperTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OperType operType = db.OperTypes.Find(id);
            db.OperTypes.Remove(operType);
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

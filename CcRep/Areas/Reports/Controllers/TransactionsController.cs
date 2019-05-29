using CcRep.Areas.Reports.Models;
using CcRep.Managers.Transactions;
using CcRep.Models._dc;
using CcRep.Models.vk;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CcRep.Areas.Reports.Controllers
{
    public class TransactionsController : Controller
    {
        private CcRepContext db = new CcRepContext();

        [Authorize]
        public ActionResult Index(int? id)
        {
            HeaderRep rep = db.HeaderReps.Find(id);
            if (rep == null)
            {
                throw new HttpException(404, "Отчет не был найден.");
            }

            var addInfReps = db.AddInfReps.Include(a => a.HeaderRep);

            TranIndexViewModel viewModel = new TranIndexViewModel() { Report = rep, AddInfReps = addInfReps.ToList() };

            return View(viewModel);
        }

        public ActionResult IndexCurrent(int idRep)
        {
            HeaderRep rep = db.HeaderReps.Find(idRep);
            if (rep == null)
            {
                throw new HttpException(404, "Отчет не был найден.");
            }

            var addInfReps = db.AddInfReps.Include(a => a.HeaderRep);
            TranIndexViewModel viewModel = new TranIndexViewModel() { Report = rep, AddInfReps = addInfReps.ToList() };

            return View(viewModel);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddInfRep addInfRep = db.AddInfReps.Include(h => h.HeaderRep).Where(d => d.IdOper == id).FirstOrDefault();

            if (addInfRep == null)
            {
                return HttpNotFound();
            }

            return View(addInfRep);
        }

        // Id - ReportId
        public ActionResult Create(int Id)
        {
            if (db.HeaderReps.Any(o => o.Id == Id))
            {
                var model = new TranCreateViewModel(db.HeaderReps.Find(Id));
                model.setViewLists(db);

                return View(model);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TranCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                TransactionsManager manager = new TransactionsManager(db);

                var result = manager.AddTranFromUserInput(model);

                return RedirectToAction("Index");
            }

            return Content("false");
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddInfRep addInfRep = db.AddInfReps.Find(id);
            if (addInfRep == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRep = new SelectList(db.HeaderReps, "Id", "UserLastEditedId", addInfRep.IdRep);

            return View(addInfRep);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Section,Status,Author,DateCreate,Source,IdSource,DateLock,UserLock,DateRemove,NumJD,NumDVK,DateRequest,DateGetDoc,SourceName,AccountClientId,PassportNum,IrbPaymentRK,PaymentRK,History,IdRep")] AddInfRep addInfRep)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addInfRep).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRep = new SelectList(db.HeaderReps, "Id", "UserLastEditedId", addInfRep.IdRep);
            return View(addInfRep);
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

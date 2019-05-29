using CcRep.Models._dc;
using CcRep.Models.vk;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CcRep.Models.adm;
using CcRep.Models.etl;
using CcRep.Managers;

namespace CcRep.Areas.Reports.Controllers
{
    public class HeaderRepsController : Controller
    {
        private CcRepContext db = new CcRepContext();

        // GET: Reports/HeaderReps
        public ActionResult Index()
        {
            var headerReps = db.HeaderReps.Include(h => h.UserArch).Include(h => h.UserCreated).Include(h => h.UserLastEdited);
            return View(headerReps.ToList());
        }

        public ActionResult ToArchiveList()
        {
            var ActiveReps = from p in db.HeaderReps.Include(h => h.UserArch).Include(h => h.UserCreated).Include(h => h.UserLastEdited)
                             where p.ArchiveDate == null
                             select p;
            return View(ActiveReps.ToList());
        }

        public ActionResult LoadFromDwhList()
        {
            var yr = DateTime.Today.Year;
            var mth = DateTime.Today.Month;
            var firstDayPrivMonth = new DateTime(yr, mth, 1).AddMonths(-1);

            var ActiveReps = from p in db.HeaderReps.Include(h => h.UserArch).Include(h => h.UserCreated).Include(h => h.UserLastEdited)
                             where p.ArchiveDate == null && p.BeginDate == firstDayPrivMonth
                             select p;
            return View(ActiveReps.ToList());
        }

        [HttpPost]
        public ActionResult DwhLoad(int id)
        {
            HeaderRep headerRep = db.HeaderReps.Find(id);

            ReportsManager repManager = new ReportsManager(db);

            string message = repManager.LoadDwhOperations(headerRep);

            TempData["Flash"] = message;

            return RedirectToAction("LoadFromDwhList");
        }

        [HttpPost]
        public ActionResult ToArchive(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeaderRep headerRep = db.HeaderReps.Find(id);

            headerRep.StatusUserArh = User.Identity.GetUserId();
            headerRep.ArchiveDate = DateTime.Now;
            db.Entry(headerRep).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ToArchiveList");
        }

        // GET: Reports/HeaderReps/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeaderRep headerRep = db.HeaderReps.Find(id);
            if (headerRep == null)
            {
                return HttpNotFound();
            }

            return View(headerRep);
        }

        // GET: Reports/HeaderReps/Create
        public ActionResult Create()
        {
            var model = new HeaderRep();
            
            return View(model);
        }

        // POST: Reports/HeaderReps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BeginDate,EndDate,Comment")] HeaderRep headerRep)
        {
            if (ModelState.IsValid)
            {
                db.HeaderReps.Add(headerRep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(headerRep);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConfirmDwhLoadin() {
            var ActiveReps = from p in db.DwhLoads
                             where p.DateLoad == null
                             select p;

            return false;
        }
    }
}

using CcRep.Models._dc;
using CcRep.Models.dic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CcRep.Areas.Directories.Controllers
{
    public class AcctReportsController : Controller
    {
        private CcRepContext db = new CcRepContext();

        // GET: Directories/AcctReportsTest
        public ActionResult Index()
        {

            return View(db.AcctReports.ToList());
        }

        // GET: Directories/AcctReportsTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcctReport acctReport = db.AcctReports.Find(id);
            if (acctReport == null)
            {
                return HttpNotFound();
            }
            return View(acctReport);
        }

        // GET: Directories/AcctReportsTest/Create
        public ActionResult Create()
        {
            ViewBag.ClientTypes = db.ClientTypes.ToList();

            return View();
        }

        // POST: Directories/AcctReportsTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Acc2,Resident,CntrPartner")] AcctReport acctReport, int[] selectedClientTypes)
        {
            if (ModelState.IsValid)
            {
                if (selectedClientTypes != null)
                {
                    foreach (var c in db.ClientTypes.Where(co => selectedClientTypes.Contains(co.Id)))
                    {
                        acctReport.ClientTypes.Add(c);
                    }
                }
                db.AcctReports.Add(acctReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(acctReport);
        }

        // GET: Directories/AcctReportsTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcctReport acctReport = db.AcctReports.Find(id);
            if (acctReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientTypes = db.ClientTypes.ToList();

            return View(acctReport);
        }

        // POST: Directories/AcctReportsTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Acc2,Resident,CntrPartner, selectedClientTypes")] AcctReport acctReport, int[] selectedClientTypes)
        {
            if (ModelState.IsValid)
            {
                AcctReport acctReportUpdated = db.AcctReports.Find(acctReport.Id);
                UpdateModel(acctReportUpdated);

                acctReportUpdated.ClientTypes.Clear();

                if (selectedClientTypes != null)
                {
                    foreach (var c in db.ClientTypes.Where(co => selectedClientTypes.Contains(co.Id)))
                    {
                        acctReportUpdated.ClientTypes.Add(c);
                    }
                }
                db.Entry(acctReportUpdated).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(acctReport);
        }

        // GET: Directories/AcctReportsTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcctReport acctReport = db.AcctReports.Find(id);
            if (acctReport == null)
            {
                return HttpNotFound();
            }
            return View(acctReport);
        }

        // POST: Directories/AcctReportsTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AcctReport acctReport = db.AcctReports.Find(id);
            db.AcctReports.Remove(acctReport);
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

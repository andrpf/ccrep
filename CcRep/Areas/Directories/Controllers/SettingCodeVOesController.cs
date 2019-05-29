using CcRep.Models._dc;
using CcRep.Models.dic;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CcRep.Areas.Directories.Controllers
{
    public class SettingCodeVOesController : Controller
    {
        private CcRepContext db = new CcRepContext();

        // GET: Directories/SettingCodeVOes
        public ActionResult Index()
        {
            var settingCodeVOs = db.SettingCodeVOs.Include(s => s.OperationCode);
            return View(settingCodeVOs.ToList());
        }

        // GET: Directories/SettingCodeVOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingCodeVO settingCodeVO = db.SettingCodeVOs.Find(id);
            if (settingCodeVO == null)
            {
                return HttpNotFound();
            }
            return View(settingCodeVO);
        }

        // GET: Directories/SettingCodeVOes/Create
        public ActionResult Create()
        {
            MultiSelectList СtList = CodeToolingsList();
            ViewBag.CtList = СtList;

            ViewBag.OperationCodeRef = new SelectList(db.DirectionPays, "Direction_Pay", "Direction_Pay");
            ViewBag.SectionRef = new SelectList(db.SettingCodeVOSections, "SectionNo", "SectionNo");

            return View();
        }

        // POST: Directories/SettingCodeVOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,CodeVO,Replace405,CodeVODesc,Code405Desc,OperationCodeRef,DirectionPay,IssuerNerez,IssuerRez,PropertyFlg,Include405,Include406,SectionRef")] SettingCodeVO settingCodeVO,
            int[] selectedCode405
            )
        {
            if (ModelState.IsValid)
            {
                if (selectedCode405 != null)
                {
                    foreach (var c in db.CodeToolings.Where(co => selectedCode405.Contains(co.Id)))
                    {
                        settingCodeVO.CodeToolings.Add(c);
                    }
                }
                db.SettingCodeVOs.Add(settingCodeVO);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.OperationCodeRef = new SelectList(db.DirectionPays, "Direction_Pay", "DescDirect", settingCodeVO.OperationCodeRef);

            return View(settingCodeVO);
        }

        // GET: Directories/SettingCodeVOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingCodeVO settingCodeVO = db.SettingCodeVOs.Find(id);

            if (settingCodeVO == null)
            {
                return HttpNotFound();
            }
            ViewBag.OperationCodeRef = new SelectList(db.DirectionPays, "Direction_Pay", "Direction_Pay", settingCodeVO.OperationCodeRef);
            var selectedCode405 = settingCodeVO.CodeToolings.Select(item => item.Id.ToString()).ToArray();

            MultiSelectList list = CodeToolingsList(selectedCode405);
            ViewBag.SectionRef = new SelectList(db.SettingCodeVOSections, "SectionNo", "SectionNo");
            ViewBag.CtList = list;
            ViewBag.Title = "Изменить код (405/406)";
            ViewBag.isEdit = true;

            return View(settingCodeVO);
        }

        // POST: Directories/SettingCodeVOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,CodeVO,Replace405,CodeVODesc,Code405Desc,OperationCodeRef,DirectionPay,IssuerNerez,IssuerRez,PropertyFlg,Include405,Include406, selectedCode405")] SettingCodeVO settingCodeVO,
            int[] selectedCode405
            )
        {
            if (ModelState.IsValid)
            {
                SettingCodeVO settingCodeVOUpdated = db.SettingCodeVOs.Find(settingCodeVO.Id);
                UpdateModel(settingCodeVOUpdated);

                settingCodeVOUpdated.CodeToolings.Clear();
                if (selectedCode405 != null)
                {
                    foreach (var c in db.CodeToolings.Where(co => selectedCode405.Contains(co.Id)))
                    {
                        settingCodeVOUpdated.CodeToolings.Add(c);
                    }
                }

                db.Entry(settingCodeVOUpdated).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OperationCodeRef = new SelectList(db.DirectionPays, "Direction_Pay", "DescDirect", settingCodeVO.OperationCodeRef);

            return View(settingCodeVO);
        }

        // GET: Directories/SettingCodeVOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingCodeVO settingCodeVO = db.SettingCodeVOs.Find(id);
            if (settingCodeVO == null)
            {
                return HttpNotFound();
            }

            return View(settingCodeVO);
        }

        // POST: Directories/SettingCodeVOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SettingCodeVO settingCodeVO = db.SettingCodeVOs.Find(id);
            db.SettingCodeVOs.Remove(settingCodeVO);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        private MultiSelectList CodeToolingsList(string[] defaultSelected = null)
        {
            List<SelectListItem> allItems = new List<SelectListItem>();

            List<CodeTooling> CtList = db.CodeToolings.ToList();
            CtList.ForEach(Ct =>
            {
                allItems.Add(new SelectListItem()
                {
                    Text = Ct.TypeTooling,
                    Value = Ct.Id.ToString(),
                });
            });

            return new MultiSelectList(allItems, "Value", "Text", defaultSelected);
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

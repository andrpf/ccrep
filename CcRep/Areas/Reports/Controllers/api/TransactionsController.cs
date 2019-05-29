using CcRep.Areas.Reports.Components;
using CcRep.Areas.Reports.Models;
using CcRep.Components.Filtering;
using CcRep.Managers.Transactions;
using CcRep.Models._dc;
using CcRep.Models.vk;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CcRep.Areas.Reports.Controllers.api
{
    //[Authorize]
    public class TransactionsController : ApiController
    {
        private CcRepContext db = new CcRepContext();

        [HttpGet]
        public string GridFiltersAndRules()
        {
            
            return QueryBuilderProvider.FiltersJson;
        }

        [HttpPost]
        public IHttpActionResult GetData([FromBody] FindTranDataModel req)
        {
            HeaderRep headerRep = db.HeaderReps.Find(req.ReportId);
            if (headerRep == null)
                return Json("Отчет с таким номером отсутствует.");

            GridDataHandler handler= new GridDataHandler(db);
            var res = handler.GetData(req, headerRep);
            if (!String.IsNullOrEmpty(handler.ErrorString))
                return Json(handler.ErrorString);
            return Json(res);
        }

        [HttpPost]
        public IHttpActionResult AddComment(AddCommentModel comment)
        {
            if (ModelState.IsValid)
            {
                CommentsManager comMngr = new CommentsManager(db);

                var userId = RequestContext.Principal.Identity.GetUserId();
                var result = comMngr.AddUserComment(comment, userId);

                var comments = comMngr.FindComments(comment.TranId);

                return Json(comments);
            }

            return Json(false);
        }

        [HttpGet]
        public IHttpActionResult GetComments(long Id)
        {
            CommentsManager comMngr = new CommentsManager(db);

            var comments = comMngr.FindComments(Id);

            return Json(comments);
        }

        [HttpGet]
        public IHttpActionResult GetStatusesBySupdocFl(string supdocFl)
        {
            List<dynamic> resultList = new List<dynamic>();

            switch (supdocFl)
            {
                case "Y":
                    resultList.AddRange(db.StatusOpers.Select(n => new { Value = n.Status, Text = n.NameStatus }).ToList());
                    break;
                case "N":
                    resultList.AddRange(db.StatusOpers.Where(n => n.SUPDOC_FL == null).Select(n => new { Value = n.Status, Text = n.NameStatus }).ToList());
                    break;
            }

            return Json(resultList);
        }
    }
}

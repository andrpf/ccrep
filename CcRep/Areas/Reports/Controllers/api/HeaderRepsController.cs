using CcRep.Models._dc;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace CcRep.Areas.Reports.Controllers.api
{
    public class HeaderRepsController : ApiController
    {
        private CcRepContext db = new CcRepContext();

        [HttpGet]
        public JsonResult<string> CheckEndBeginDate(string BeginDate, string EndDate)
        {
            var ActiveReps = from p in db.HeaderReps
                             where p.ArchiveDate == null
                             select p;

            if (ActiveReps.ToList().Count >= 2)
            {
                return Json("В базе уже есть 2 активных отчета");
            }

            try
            {
                var BegDate = DateTime.Parse(BeginDate);
                var EnDate = DateTime.Parse(EndDate);

                var year = BegDate.Year;
                var month = BegDate.Month;

                var startDate = new DateTime(year, month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                if (BegDate.Month != EnDate.Month)
                {
                    return Json("У дат разные месяцы");
                }
                else if (BegDate != startDate || EnDate != endDate)
                {
                    return Json("Начало должно содержать 1ое число месяца, а Конец - последнее");
                }
                else if (db.HeaderReps.Any(o => o.BeginDate == BegDate))
                {
                    return Json("Отчет за этот период уже существует");
                }

                return Json("true");
            }
            catch
            {
                return Json("Обе даты должны быть заполнены"); ;
            }
        }
    }
}

using CcRep.Models._dc;
using System.Linq;
using System.Web.Http;

namespace CcRep.Areas.Directories.Controllers.api
{
    public class AcctReportsController : ApiController
    {
        private CcRepContext db = new CcRepContext();

        [HttpGet]
        public bool IsAcc2NotExists(string Acc2, int? Id)
        {
            Acc2 = Acc2?.Trim();

            return !db.AcctReports.Any(o => o.Acc2 == Acc2 && o.Id != Id);
        }
    }
}

using CcRep.Models._dc;
using System.Linq;
using System.Web.Http;

namespace CcRep.Areas.Directories.Controllers.api
{
    public class AccountClientsController : ApiController
    {
        private CcRepContext db = new CcRepContext();

        [HttpGet]
        public bool IsAccClExists(string CNum, string CBAccount, int? Id)
        {
            CNum = CNum?.Trim();
            CBAccount = CBAccount?.Trim();

            return !db.AccountClients.Any(o => o.CNum == CNum && o.CBAccount == CBAccount && o.Id != Id);
        }
    }
}

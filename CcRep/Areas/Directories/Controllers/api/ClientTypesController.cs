using CcRep.Models._dc;
using System.Linq;
using System.Web.Http;

namespace CcRep.Areas.Directories.Controllers.api
{
    public class ClientTypesController : ApiController
    {
        private CcRepContext db = new CcRepContext();

        [HttpGet]
        public bool IsTypeExists(string NameShort, int? Id)
        {
            NameShort = NameShort?.Trim();
            
            return ! db.ClientTypes.Any(o => o.NameShort == NameShort && o.Id != Id);
        }
    }
}

using CcRep.Models._dc;
using System.Linq;
using System.Web.Http;

namespace CcRep.Areas.Directories.Controllers.api
{
    public class OperTypesController : ApiController
    {
        private CcRepContext db = new CcRepContext();

        [HttpGet]
        public bool IsTypeNotExists(byte Oper_Type, int? Id)
        {
            return !db.OperTypes.Any(o => o.Oper_Type == Oper_Type && o.Id != Id);
        }
    }
}

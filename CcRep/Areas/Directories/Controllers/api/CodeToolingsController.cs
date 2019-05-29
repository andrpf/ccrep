using CcRep.Models._dc;
using System.Linq;
using System.Web.Http;

namespace CcRep.Areas.Directories.Controllers.api
{
    public class CodeToolingsController : ApiController
    {
        private CcRepContext db = new CcRepContext();

        [HttpGet]
        public bool IsTypeNotExists(string TypeTooling, int? Id)
        {
            TypeTooling = TypeTooling?.Trim();

            return !db.CodeToolings.Any(o => o.TypeTooling == TypeTooling && o.Id != Id);
        }
    }
}

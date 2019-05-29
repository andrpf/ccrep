using CcRep.Models._dc;
using System.Linq;
using System.Web.Http;

namespace CcRep.Areas.Directories.Controllers.api
{
    public class PDTypesController : ApiController
    {
        private CcRepContext db = new CcRepContext();

        [HttpGet]
        public bool IsTypeNotExists(string CodePD, int? Id)
        {
            CodePD = CodePD?.Trim();

            return !db.PDTypes.Any(o => o.CodePD == CodePD && o.Id != Id);
        }
    }
}

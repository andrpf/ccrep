using CcRep.Models._dc;
using System.Linq;
using System.Web.Http;

namespace CcRep.Areas.Directories.Controllers.api
{
    public class KeywordsExceptionsController : ApiController
    {
        private CcRepContext db = new CcRepContext();

        [HttpGet]
        public bool IsTypeNotExists(string Keyword, int? Id)
        {
            Keyword = Keyword?.Trim();

            return !db.KeywordsExceptions.Any(o => o.Keyword == Keyword && o.Id != Id);
        }
    }
}

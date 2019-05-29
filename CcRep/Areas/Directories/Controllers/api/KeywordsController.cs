using CcRep.Models._dc;
using System.Linq;
using System.Web.Http;

namespace CcRep.Areas.Directories.Controllers.api
{
    public class KeywordsController : ApiController
    {
        private CcRepContext db = new CcRepContext();

        [HttpGet]
        public bool IsKeywordNotExist(string Key_word, int? Id)
        {
            Key_word = Key_word?.Trim();

            return !db.Keywords.Any(o => o.Key_word == Key_word && o.Id != Id);
        }
    }
}

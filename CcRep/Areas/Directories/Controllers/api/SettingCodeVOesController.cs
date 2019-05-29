using CcRep.Models._dc;
using System.Linq;
using System.Web.Http;

namespace CcRep.Areas.Directories.Controllers.api
{
    public class SettingCodeVOesController : ApiController
    {
        private CcRepContext db = new CcRepContext();

        [HttpGet]
        public bool IsCodeVoNotExists(string CodeVO, int? Id)
        {
            CodeVO = CodeVO?.Trim();

            return !db.SettingCodeVOs.Any(o => o.CodeVO == CodeVO && o.Id != Id);
        }
    }
}

using CcRep.Managers.Export;
using CcRep.Models._dc;
using CcRep.Models.vk;
using System;
using System.IO;
using System.Web.Http;
using System.Web.Http.Results;

namespace CcRep.Areas.Reports.Controllers.api
{
    public class ExportController : ApiController
    {
        CcRepContext db = new CcRepContext();

        [HttpGet]
        public JsonResult<string> ExportToKliko(int? id, string _filials, string type_clients, string export_path)
        {
            if (id == null)
                return Json("Номер отчета неопределен.");

            HeaderRep headerRep = db.HeaderReps.Find(id);
            if (headerRep == null)
                return Json("Отчет с таким номером отсутствует.");

            if (String.IsNullOrEmpty(export_path.Trim(new char[] { ' ','\\', '/' })))
                return Json("Некорректно задан путь к папке для формирования файлов для выгрузки в КЛИКО.");
            
            if (!Directory.Exists(export_path))
                Directory.CreateDirectory(export_path);

            if (!Directory.Exists(export_path))
                return Json("Ошибка при создании директории: " + export_path);

            ExportToKliko export_to_kliko = new ExportToKliko(db);
            string result = export_to_kliko.CreateKlikoFiles(headerRep, export_path, _filials, type_clients);
            return Json(result);
        }
    }
}
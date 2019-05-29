using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvStatusRezMapping : CsvMapping<StatusRez>
    {
        public CsvStatusRezMapping() : base()
        {
            MapProperty(0, x => x.Status);
            MapProperty(1, x => x.NameStatus);
        }
    }
}
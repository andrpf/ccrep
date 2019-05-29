using CcRep.Models.dic;
using TinyCsvParser.Mapping;

using TinyCsvParser.TypeConverter;

namespace CcRep.Models._db.csv
{
    public class CsvStatusOperMapping : CsvMapping<StatusOper>
    {
        public CsvStatusOperMapping() : base()
        {
            MapProperty(0, x => x.Status);
            MapProperty(1, x => x.NameStatus);
            MapProperty(2, x => x.SUPDOC_FL);
        }
    }
}
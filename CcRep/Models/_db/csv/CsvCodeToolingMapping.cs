using CcRep.Models.dic;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace CcRep.Models._db.csv
{
    public class CsvCodeToolingMapping : CsvMapping<CodeTooling>
    {
        public CsvCodeToolingMapping() : base()
        {
            MapProperty(0, x => x.TypeTooling);
            MapProperty(1, x => x.DescTooling);
            MapProperty(2, x => x.BegDate, new DateTimeConverter("dd.MM.yyyy"));
        }
    }
}
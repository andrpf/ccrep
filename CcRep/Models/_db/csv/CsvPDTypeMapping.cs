using CcRep.Models.dic;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace CcRep.Models._db.csv
{
    public class CsvPDTypeMapping : CsvMapping<PDType>
    {
        public CsvPDTypeMapping() : base()
        {
            MapProperty(0, x => x.CodePD);
            MapProperty(1, x => x.DescCodePD);
            MapProperty(2, x => x.BegDate, new DateTimeConverter("dd.MM.yyyy"));
        }
    }
}
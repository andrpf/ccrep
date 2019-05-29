using CcRep.Models.dic;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace CcRep.Models._db.csv
{
    public class CsvOperKindMapping : CsvMapping<OperKind>
    {
        public CsvOperKindMapping() : base()
        {
            MapProperty(0, x => x.Oper_Kind, new ByteConverter());
            MapProperty(1, x => x.DescOperKind);
        }
    }
}
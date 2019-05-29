using CcRep.Models.dic;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace CcRep.Models._db.csv
{
    public class CsvAddValue10Mapping : CsvMapping<AddValue10>
    {
        public CsvAddValue10Mapping() : base()
        {
            MapProperty(0, x => x.AddValue, new ByteConverter());
            MapProperty(1, x => x.DescAdd);
        }
    }
}
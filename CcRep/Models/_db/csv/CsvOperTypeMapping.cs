using CcRep.Models.dic;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace CcRep.Models._db.csv
{
    public class CsvOperTypeMapping : CsvMapping<OperType>
    {
        public CsvOperTypeMapping() : base()
        {
            MapProperty(0, x => x.Oper_Type, new ByteConverter());
            MapProperty(1, x => x.OperTypeDesc);
        }
    }
}
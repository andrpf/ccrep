using CcRep.Models.dic;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace CcRep.Models._db.csv
{
    public class CsvIssuerCodeMapping : CsvMapping<IssuerCode>
    {
        public CsvIssuerCodeMapping() : base()
        {
            MapProperty(0, x => x.Issuer_Code, new ByteConverter());
            MapProperty(1, x => x.DescIssuerCode);
        }
    }
}
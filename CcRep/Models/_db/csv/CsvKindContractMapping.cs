using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvKindContractMapping : CsvMapping<KindContract>
    {
        public CsvKindContractMapping() : base()
        {
            MapProperty(0, x => x.Kind_Contract);
            MapProperty(1, x => x.DescKindContract);
        }
    }
}
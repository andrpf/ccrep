using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvTypeContractMapping : CsvMapping<TypeContract>
    {
        public CsvTypeContractMapping() : base()
        {
            MapProperty(0, x => x.Type_Contract);
            MapProperty(1, x => x.DescTypeContract);
        }
    }
}
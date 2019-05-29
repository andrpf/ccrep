using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvDirectionPayMapping : CsvMapping<DirectionPay>
    {
        public CsvDirectionPayMapping() : base()
        {
            MapProperty(0, x => x.Direction_Pay);
            MapProperty(1, x => x.DescDirect);
        }
    }
}
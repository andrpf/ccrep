using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvClientTypeAcctReportMapping : CsvMapping<ClientType>
    {
        public CsvClientTypeAcctReportMapping() : base()
        {
            MapProperty(1, x => x.NameShort);
        }
    }
}
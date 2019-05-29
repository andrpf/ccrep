using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvClientTypeMapping : CsvMapping<ClientType>
    {
        public CsvClientTypeMapping() : base()
        {
            MapProperty(0, x => x.NameShort);
            MapProperty(1, x => x.Description);
        }
    }
}
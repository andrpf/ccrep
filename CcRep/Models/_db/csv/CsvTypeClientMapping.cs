using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvTypeClientMapping : CsvMapping<TypeClient>
    {
        public CsvTypeClientMapping() : base()
        {
            MapProperty(0, x => x.Type_Client);
            MapProperty(1, x => x.Description);
        }
    }
}
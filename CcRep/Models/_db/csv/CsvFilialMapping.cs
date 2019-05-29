using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvFilialMapping : CsvMapping<Filial>
    {
        public CsvFilialMapping() : base()
        {
            MapProperty(0, x => x.LCFilial);
            MapProperty(1, x => x.NCFilial);
            MapProperty(2, x => x.License);
            MapProperty(3, x => x.NameFilial);
        }
    }
}
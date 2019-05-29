using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvKindNerezMapping : CsvMapping<KindNeRez>
    {
        public CsvKindNerezMapping() : base()
        {
            MapProperty(0, x => x.Kind_Nerez);
            MapProperty(1, x => x.DescKind);
        }
    }
}
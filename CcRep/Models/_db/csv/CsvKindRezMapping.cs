using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvKindRezMapping : CsvMapping<KindRez>
    {
        public CsvKindRezMapping() : base()
        {
            MapProperty(0, x => x.Kind_Rez);
            MapProperty(1, x => x.DescKind);
        }
    }
}
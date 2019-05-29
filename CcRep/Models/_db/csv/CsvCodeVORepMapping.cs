using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvCodeVORepMapping : CsvMapping<CodeVORep>
    {
        public CsvCodeVORepMapping() : base()
        {
            MapProperty(0, x => x.Report);
            MapProperty(1, x => x.Section);
            MapProperty(2, x => x.CodeVO);
            MapProperty(3, x => x.DescCodeVO);
        }
    }
}
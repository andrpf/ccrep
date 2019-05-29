using CcRep.Models.dic;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CsvCodeToolingSettingCodeVOsMapping : CsvMapping<CodeTooling>
    {
        public CsvCodeToolingSettingCodeVOsMapping() : base()
        {
            MapProperty(1, x => x.TypeTooling);
        }
    }
}
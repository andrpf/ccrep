using CcRep.Models.dic;
using System;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace CcRep.Models._db.csv
{
    public class CsvCcRepRoleMapping : CsvMapping<CcRepRole>
    {
        public CsvCcRepRoleMapping() : base()
        {
            MapProperty(0, x => x.Name);
            MapProperty(1, x => x.Desc);
        }
    }
}
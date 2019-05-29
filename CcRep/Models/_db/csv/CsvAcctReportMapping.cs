using CcRep.Models.dic;
using System;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace CcRep.Models._db.csv
{
    public class CsvAcctReportMapping : CsvMapping<AcctReport>
    {
        public CsvAcctReportMapping() : base()
        {
            MapProperty(0, x => x.Acc2);
            MapProperty(2, x => x.Resident, new BoolConverter("TRUE", "FALSE", StringComparison.InvariantCulture));
            MapProperty(3, x => x.CntrPartner, new BoolConverter("TRUE", "FALSE", StringComparison.InvariantCulture));
        }
    }
}
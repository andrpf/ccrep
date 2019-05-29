using CcRep.Models.dic;
using System;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace CcRep.Models._db.csv
{
    public class CsvSettingCodeVOsMapping : CsvMapping<SettingCodeVO>
    {
        public CsvSettingCodeVOsMapping() : base()
        {
            MapProperty(0, x => x.CodeVO);
            MapProperty(2, x => x.Replace405);
            MapProperty(3, x => x.CodeVODesc);
            MapProperty(4, x => x.Code405Desc);
            MapProperty(5, x => x.SectionRef, new NullableInt16Converter());
            MapProperty(6, x => x.OperationCodeRef);
            MapProperty(7, x => x.DirectionPay, new NullableByteConverter());
            MapProperty(8, x => x.IssuerNerez, new NullableBoolConverter("TRUE", "FALSE", StringComparison.InvariantCulture));
            MapProperty(9, x => x.IssuerRez, new NullableBoolConverter("TRUE", "FALSE", StringComparison.InvariantCulture));
            MapProperty(10, x => x.PropertyFlg, new NullableBoolConverter("TRUE", "FALSE", StringComparison.InvariantCulture));
            MapProperty(11, x => x.Include405, new NullableBoolConverter("TRUE", "FALSE", StringComparison.InvariantCulture));
            MapProperty(12, x => x.Include406, new NullableBoolConverter("TRUE", "FALSE", StringComparison.InvariantCulture));
        }
    }
}
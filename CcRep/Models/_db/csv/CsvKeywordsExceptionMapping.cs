using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TinyCsvParser.Mapping;

using CcRep.Models.dic;

namespace CcRep.Models._db.csv
{
    public class CsvKeywordsExceptionMapping : CsvMapping<KeywordsException>
    {
        public CsvKeywordsExceptionMapping() : base()
        {
            MapProperty(0, x => x.Keyword);
        }
    }
}
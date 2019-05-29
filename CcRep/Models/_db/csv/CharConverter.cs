using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TinyCsvParser.TypeConverter;

namespace CcRep.Models._db.csv
{
    public class CharConverter : NonNullableConverter<char>
    {
        protected override bool InternalConvert(string value, out char result)
        {
            if (string.IsNullOrEmpty(value) || value.Length != 1)
            {
                result = ' ';
                return false;
            }

            result = value[0];
            return true;
        }
    }

    public class NullableCharConverter : NullableInnerConverter<char>
    {
        public NullableCharConverter()
            : base(new CharConverter())
        {
        }

        protected override bool InternalConvert(string value, out char? result)
        {
            if (string.IsNullOrEmpty(value))
                result = null;
            else
                result = value[0];
            return true;
        }
    }
}
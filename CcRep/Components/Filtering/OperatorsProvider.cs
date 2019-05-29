using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CcRep.Components.Filtering
{
    public static class OperatorsProvider
    {
        public static List<string> OperatorsString { get; set; } = new List<string> {
            "equal",
            "not_equal",
            "begins_with",
            "contains",
            "is_null",
            "is_not_null",
            "is_empty",
            "is_not_empty"
        };

        public static List<string> OperatorsInt { get; set; } = new List<string>
        {
            "equal",
            "not_equal",
            "is_null",
            "is_not_null",
            "less",
            "less_or_equal",
            "greater",
            "greater_or_equal"
        };

        public static List<string> OperatorsDouble { get; set; } = new List<string>
        {
            "equal",
            "not_equal",
            "is_null",
            "is_not_null",
            "less",
            "less_or_equal",
            "greater",
            "greater_or_equal"
        };

        public static List<string> OperatorsDate { get; set; } = new List<string>
        {
            "equal",
            "not_equal",
            "is_null",
            "is_not_null",
            "less",
            "less_or_equal",
            "greater",
            "greater_or_equal"
        };

        public static List<string> OperatorsBool { get; set; } = new List<string>
        {
            "equal",
            "not_equal"
        };
    }
}
using System;
using System.Collections.Generic;
using CcRep.Areas.Reports.Models;
using System.Globalization;
using System.Linq.Expressions;
using CcRep.Components.Handlers;

namespace CcRep.Managers.Transactions
{
    public class HandleFilterParams<T>
    {
        const string empty_false_condition = "0 != 1";
        const string empty_true_condition = "0 == 0";
        List<QueryBuildRule> rules = new List<QueryBuildRule>();
        CultureInfo provider = CultureInfo.InvariantCulture;

        public class OperatorLinq
        {
            public string oper;
            public string expr;
            public bool use_value;
            public string[] types;
        }

        static string[] all_types = new[] { "integer", "decimal", "string", "double", "datetime", "boolean" };
        static string[] five_types = new[] { "integer", "decimal", "string", "double", "boolean" };
        static string[] string_type = new[] { "string" };
        static string[] datetime_type = new[] { "datetime" };
        static string[] num_types = new[] { "integer", "double", "decimal" };

        const string date_time_format = "DateTime({1},{2},{3},{4},{5},{6})";

        readonly List<OperatorLinq> Operators = new List<OperatorLinq>
        {
            new OperatorLinq { oper = "equal", expr = "{0} = \"{1}\"", use_value = true, types = string_type },
            new OperatorLinq { oper = "equal", expr = "{0} = {1}", use_value = true, types = num_types },
            new OperatorLinq { oper = "equal", expr = "{0} = " + date_time_format, use_value = true, types = datetime_type },
            new OperatorLinq { oper = "not_equal", expr = "{0} != {1}", use_value = true, types = num_types },
            new OperatorLinq { oper = "not_equal", expr = "{0} != \"{1}\"", use_value = true, types = string_type },
            new OperatorLinq { oper = "not_equal", expr = "{0} != " + date_time_format, use_value = true, types = datetime_type },
            new OperatorLinq { oper = "is_null", expr = "{0} = null", use_value = false, types = all_types },
            new OperatorLinq { oper = "is_not_null", expr = "{0} != null", use_value = false, types = all_types },
            new OperatorLinq { oper = "begins_with", expr = "{0}.StartsWith(\"{1}\")", use_value = true, types = string_type },
            new OperatorLinq { oper = "contains", expr = "{0}.Contains(\"{1}\")", use_value = true, types = string_type },
            new OperatorLinq { oper = "is_empty", expr = "{0} == \"\"", use_value = false, types = string_type },
            new OperatorLinq { oper = "is_not_empty", expr = "{0} != \"\"", use_value = false, types = string_type },
            new OperatorLinq { oper = "less", expr = "{0} < \"{1}\"", use_value = true, types = string_type },
            new OperatorLinq { oper = "less", expr = "{0} < {1}", use_value = true, types = num_types },
            new OperatorLinq { oper = "less", expr = "{0} < " + date_time_format, use_value = true, types = datetime_type },
            new OperatorLinq { oper = "less_or_equal", expr = "{0} <= {1}", use_value = true, types = num_types },
            new OperatorLinq { oper = "less_or_equal", expr = "{0} <= \"{1}\"", use_value = true, types = string_type },
            new OperatorLinq { oper = "less_or_equal", expr = "{0} <= " + date_time_format, use_value = true, types = datetime_type },
            new OperatorLinq { oper = "greater", expr = "{0} > {1}", use_value = true, types = num_types },
            new OperatorLinq { oper = "greater", expr = "{0} > \"{1}\"", use_value = true, types = string_type },
            new OperatorLinq { oper = "greater", expr = "{0} > " + date_time_format, use_value = true, types = datetime_type },
            new OperatorLinq { oper = "greater_or_equal", expr = "{0} >= \"{1}\"", use_value = true, types = string_type },
            new OperatorLinq { oper = "greater_or_equal", expr = "{0} >= {1}", use_value = true, types = num_types },
            new OperatorLinq { oper = "greater_or_equal", expr = "{0} >= " + date_time_format, use_value = true, types = datetime_type }
        };

        OperatorLinq GetOperationLinq(string _operator, string type)
        {
            OperatorLinq op = Operators.Find(x => x.oper == _operator && Array.Find<string>(x.types, a => a == type) != null);
            if (op == null)
                throw new ApplicationException("Некорректный оператор фильтра: " + _operator);
            return op;
        }

        string GetPropertyName(string full_name)
        {
            string[] names = full_name.Split('_');
            return names[1];
        }

        public void AddField(QueryBuildRule qbr)
        {
            rules.Add(qbr);
        }

        public Expression<Func<T, bool>> GetExpression()
        {
            string expr = string.Empty;
            foreach (QueryBuildRule rule in rules)
            {
                if (expr != string.Empty)
                    expr += " and ";

                string prop_name = GetPropertyName(rule.id);
                OperatorLinq op = GetOperationLinq(rule._operator, rule.type);

                if (op.use_value)
                {
                    if (rule.type == "datetime")
                    {
                        DateTime dt = DateTime.ParseExact(rule.value, HandlerOutFormat.FORMAT_DATES[0] , provider);
                        expr += String.Format(op.expr, prop_name, dt.Year, dt.Month, dt.Day,
                                              dt.Hour, dt.Minute, dt.Second);
                    }                    
                    else
                    {   expr += String.Format(provider, op.expr, prop_name, rule.value);   }
                }
                else
                {       expr += String.Format(provider, op.expr, prop_name);    }
            };

            if (expr == string.Empty)
                expr = empty_true_condition;

            return System.Linq.Dynamic.DynamicExpression.ParseLambda<T, bool>(expr);
        }

        public Expression<Func<T, bool>> GetExpression(string[] arr, string param, bool all = false)
        {
            if (all)
                return System.Linq.Dynamic.DynamicExpression.ParseLambda<T, bool>(empty_true_condition);

            if (arr == null || arr.Length == 0)
                return System.Linq.Dynamic.DynamicExpression.ParseLambda<T, bool>(empty_false_condition);

            string expr = String.Format("@0.Contains({0})", param);
            return System.Linq.Dynamic.DynamicExpression.ParseLambda<T, bool>(expr, arr);
        }
    }
}
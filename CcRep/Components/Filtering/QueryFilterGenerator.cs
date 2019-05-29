using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace CcRep.Components.Filtering
{
    public class QueryFilterGenerator
    {
        public Object Obj { get; set; }

        public List<FilterObj> Filters { get; set; } = new List<FilterObj>();

        public QueryFilterGenerator()
        {

        }
        public QueryFilterGenerator(Object Obj) : base()
        {
            PropertyInfo[] props = Obj.GetType().GetProperties();

            SetFilters(props);
        }

        public QueryFilterGenerator(string ObjClass) : base()
        {
            PropertyInfo[] props = Obj.GetType().GetProperties();

            SetFilters(props);
        }

        public QueryFilterGenerator ParsForFilters<T>()
        {
            PropertyInfo[] props = typeof(T).GetProperties();

            SetFilters(props, typeof(T).Name);

            return this;
        }

        private void SetFilters(PropertyInfo[] props, string ClassName = null)
        {
            foreach (PropertyInfo p in props)
            {
                FilterObj filtObj = getFilterObject(p, ClassName);

                if (filtObj != null)
                {
                    Filters.Add(filtObj);
                }
            }
        }

        private FilterObj getFilterObject(PropertyInfo p, string idPrefix = null)
        {
            FilterObj result = null;

            string propName = p.Name;
            if(idPrefix != null)
            {
                propName = $"{idPrefix}_{propName}";
            }

            string displayName;

            IEnumerable<DisplayNameAttribute> sequence = p.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>();

            if (sequence != null && sequence.Any())
            {
                displayName = sequence.Single().DisplayName;
            }
            else
            {
                displayName = propName;
            }

            if (p.PropertyType == typeof(string))
            {
                result = new FilterObj { id = propName, operators = OperatorsProvider.OperatorsString, label = displayName, type = "string" };
            }
            else if (p.PropertyType == typeof(int) || p.PropertyType == typeof(byte) || p.PropertyType == typeof(Nullable<byte>) || p.PropertyType == typeof(Nullable<int>))
            {
                result = new FilterObj { id = propName, operators = OperatorsProvider.OperatorsInt, label = displayName, type = "integer" };
            }
            else if (p.PropertyType == typeof(decimal) || p.PropertyType == typeof(Nullable<decimal>))
            {
                result = new FilterObj { id = propName, operators = OperatorsProvider.OperatorsInt, label = displayName, type = "double" };
            }
            else if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(Nullable<DateTime>))
            {
                result = new FilterObj  { id = propName, operators = OperatorsProvider.OperatorsDate, label = displayName, type = "datetime" };
            }
            else if (p.PropertyType == typeof(bool) || p.PropertyType == typeof(Nullable<bool>))
            {
                result = new FilterObj { id = propName, operators = OperatorsProvider.OperatorsBool, label = displayName, type = "boolean" };
            }

            return result;
        }

        public string GetFiltersJson()
        {
            var jsonSerialiser = new JavaScriptSerializer();
            string json = jsonSerialiser.Serialize(Filters);

            return json;
        }

        public List<FilterObj> GetFilters()
        {
            return Filters;
        }
    }

    public class FilterObj
    {
        public string id { get; set; }
        public List<string> operators { get; set; }
        public string label { get; set; }
        public string type { get; set; }
    }
}
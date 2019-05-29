using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace CcRep.Components.Correction
{
    public static class CorrectProperties
    {
        public static void CorrectStrings(object obj)
        {
            Type t = obj.GetType();
            PropertyInfo[] infos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < infos.Length; i++)
            {
                PropertyInfo pi = t.GetProperty(infos[i].Name);
                if (pi.PropertyType == typeof(string))
                {
                    string value = (String)pi.GetValue(obj);
                    if (value != null && value.Length == 0)
                        continue;

                    string correctValue = CorrectString(value);
                    if (!correctValue.Equals(value))
                        pi.SetValue(obj, correctValue);
                }
            }
        }

        static string CorrectString(string value)
        {
            if (value == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder(value);
            return sb.Replace("\r\n", " ").Replace("\n\r", " ").Replace('\r', ' ')
                   .Replace('\n', ' ').Replace('\"', '\'').ToString().Trim(' ');
        }

        public static void Correction<T>(IEnumerable<T> list)
        {
            foreach (T info in list)
                CorrectStrings(info);
        }
        
    }
}
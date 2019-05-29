using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;

namespace CcRep.Models._dc
{
    public struct Table
    {
        public string Schema { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Таблица: {Name}; Схема: {Schema}";
        }
    }
    public static class GetTableTameExt
    {

        public static Table GetTableName<T>(this DbContext context) where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext.GetTableName<T>();
        }

        public static Table GetTableName<T>(this ObjectContext context) where T : class
        {
            string sql = context.CreateObjectSet<T>().ToTraceString();
            Regex regex = new Regex("FROM (?<table>.*) AS");
            Match match = regex.Match(sql);

            string table = match.Groups["table"].Value;

            string replacedTable = Regex.Replace(table, @"\[|\]", "");

            string[] words = replacedTable.Split(new char[] { '.' });

            Table tableInfo = new Table() { Name = words[words.Length - 1] };

            Array.Resize(ref words, words.Length - 1);

            tableInfo.Schema = string.Join(".", words);

            return tableInfo;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace CcRep.Models._db.csv
{
    public class CcRepCsvParser
    {
        string Path { get; set; }       

        public CcRepCsvParser(string _path)
        {
            Path = _path;
        }


        public IList<T> CsvParserStart<T>(CsvMapping<T> csvMapper, string name_file) where T:class, new()
        {
            string file = Path + name_file;
            if (!File.Exists(file))
                throw new ApplicationException(String.Format("The file '{0}' is not exist.", file));

            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');
           
            CsvParser<T> csvParser = new CsvParser<T>(csvParserOptions, csvMapper);
          
            var result = csvParser
               .ReadFromFile(file, Encoding.Default)
               .ToList().ConvertAll(x => x.Result);

            if (result.Count == 0)
                throw new ApplicationException("Cannot load data from csv-file: " + file);

            return (IList<T>)result;

        }
    }
}
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Service.Internals
{
    public class CsvBuilder : ICsvBuilder
    {
        const string Delimeter = ";";

        public T[] CsvParseFromStream<T>(Stream inputStream)
        {
            using (var reader = new StreamReader(inputStream))
            {
                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                csvReader.Configuration.HeaderValidated = default;
                csvReader.Configuration.MissingFieldFound = default;
                csvReader.Configuration.Delimiter = Delimeter;

                return csvReader.GetRecords<T>().ToArray();
            }
        }

        public async Task<string> DataToCsvStringAsync<T>(IEnumerable<T> inputData)
        {
            var result = string.Empty;
            using (var writer = new StringWriter())
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.Delimiter = Delimeter;
                await csvWriter.WriteRecordsAsync(inputData);

                result = writer.ToString();
            }

            return result;
        }
    }
}

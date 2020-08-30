using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ToDo.Service.Internals
{
    public interface ICsvBuilder
    {
        T[] CsvParseFromStream<T>(Stream inputStream);
        Task<string> DataToCsvStringAsync<T>(IEnumerable<T> inputData);
    }
}
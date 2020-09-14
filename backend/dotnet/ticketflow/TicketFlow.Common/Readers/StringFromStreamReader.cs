using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TicketFlow.Common.Readers
{
    internal class StringFromStreamReader : IStringFromStreamReader
    {
        public async Task<string> ReadAsync(Stream stream, Encoding encoding)
        {
            using var reader = new StreamReader(stream, encoding);
            return await reader.ReadToEndAsync();
        }
    }
}
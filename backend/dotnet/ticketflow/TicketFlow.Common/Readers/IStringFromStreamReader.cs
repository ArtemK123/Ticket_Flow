using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TicketFlow.Common.Readers
{
    public interface IStringFromStreamReader
    {
        Task<string> ReadAsync(Stream stream, Encoding encoding);
    }
}
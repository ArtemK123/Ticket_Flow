using System.Net.Http;
using System.Threading.Tasks;

namespace TicketFlow.Common.Validators
{
    public interface IServiceResponseValidator
    {
        Task ValidateAsync(HttpResponseMessage httpResponse);
    }
}
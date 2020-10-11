using TicketFlow.Common.ServiceUrl.Enums;
using TicketFlow.Common.ServiceUrl.Providers;

namespace TicketFlow.Common.ServiceUrl.Resolvers
{
    public interface IServiceUrlProviderResolver
    {
        IServiceUrlProvider Resolve(ServiceUrlProvidingType providingType);
    }
}
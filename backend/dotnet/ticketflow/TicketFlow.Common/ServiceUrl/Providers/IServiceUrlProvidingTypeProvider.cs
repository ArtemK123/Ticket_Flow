using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Providers
{
    public interface IServiceUrlProvidingTypeProvider
    {
        ServiceUrlProvidingType GetServiceUrlResolvingType();
    }
}
using TicketFlow.Common.ServiceUrl.Enums;

namespace TicketFlow.Common.ServiceUrl.Providers
{
    internal interface IServiceUrlProvidingTypeProvider
    {
        ServiceUrlProvidingType GetServiceUrlResolvingType();
    }
}
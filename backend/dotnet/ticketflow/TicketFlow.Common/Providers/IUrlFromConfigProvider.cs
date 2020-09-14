using Microsoft.Extensions.Configuration;

namespace TicketFlow.Common.Providers
{
    public interface IUrlFromConfigProvider
    {
        string GetUrl(IConfigurationRoot configuration);
    }
}
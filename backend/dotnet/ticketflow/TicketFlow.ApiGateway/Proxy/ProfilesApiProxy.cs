using System.Net.Http;
using TicketFlow.ApiGateway.Models.ProfileService;

namespace TicketFlow.ApiGateway.Proxy
{
    internal class ProfilesApiProxy : IProfilesApiProxy
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProfilesApiProxy(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public Profile GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Profile GetByUserAsync(string userEmail)
        {
            throw new System.NotImplementedException();
        }

        public Profile Add(Profile profile)
        {
            throw new System.NotImplementedException();
        }
    }
}
using TicketFlow.IdentityService.Service.Entities;

namespace TicketFlow.IdentityService.Service
{
    internal interface IJwtGenerator
    {
        string Generate(User user);

        string Generate(User user, int expireDays);
    }
}
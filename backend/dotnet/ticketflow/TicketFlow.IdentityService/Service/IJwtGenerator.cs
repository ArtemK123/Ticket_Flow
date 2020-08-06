using TicketFlow.IdentityService.Entities;

namespace TicketFlow.IdentityService.Service
{
    internal interface IJwtGenerator
    {
        string Generate(User user);

        string Generate(User user, int expireDays);
    }
}
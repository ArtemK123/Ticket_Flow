using TicketFlow.IdentityService.Domain.Entities;

namespace TicketFlow.IdentityService.Service
{
    internal interface IJwtGenerator
    {
        string Generate(IUser user);

        string Generate(IUser user, int expireDays);
    }
}
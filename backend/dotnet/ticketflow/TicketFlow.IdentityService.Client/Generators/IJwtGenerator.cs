using TicketFlow.IdentityService.Client.Extensibility.Entities;

namespace TicketFlow.IdentityService.Client.Generators
{
    internal interface IJwtGenerator
    {
        string Generate(IUser user);

        string Generate(IUser user, int expireDays);
    }
}
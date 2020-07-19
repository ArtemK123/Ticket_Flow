namespace TicketFlow.ProfileService.Domain.Providers
{
    internal interface IRandomValueProvider
    {
        int GetRandomInt(int from, int to);
    }
}
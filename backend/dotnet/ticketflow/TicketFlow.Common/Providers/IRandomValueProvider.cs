namespace TicketFlow.Common.Providers
{
    public interface IRandomValueProvider
    {
        int GetRandomInt(int from, int to);
    }
}
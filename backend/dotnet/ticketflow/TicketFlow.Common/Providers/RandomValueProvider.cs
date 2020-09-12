using System;

namespace TicketFlow.Common.Providers
{
    // public because can be used in Program.cs before the startup, so should be created directly
    public class RandomValueProvider : IRandomValueProvider
    {
        private readonly Random random;

        public RandomValueProvider()
        {
            random = new Random();
        }

        public int GetRandomInt(int from, int to)
        {
            // from inclusive, to exclusive
            return random.Next(from, to);
        }
    }
}
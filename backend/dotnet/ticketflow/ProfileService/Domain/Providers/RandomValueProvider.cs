using System;

namespace ProfileService.Domain.Providers
{
    internal class RandomValueProvider : IRandomValueProvider
    {
        private readonly Random random;

        public RandomValueProvider()
        {
            random = new Random();
        }

        public int GetRandomInt(int from, int to)
        {
            return random.Next(from, to);
        }
    }
}
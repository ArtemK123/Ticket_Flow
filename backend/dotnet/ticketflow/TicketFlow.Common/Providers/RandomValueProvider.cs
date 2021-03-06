﻿using System;

namespace TicketFlow.Common.Providers
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
            // from inclusive, to exclusive
            return random.Next(from, to);
        }
    }
}
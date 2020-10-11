using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketFlow.Common.Extensions
{
    public static class EnumExtensions
    {
        public static IReadOnlyCollection<TEnum> GetAllValues<TEnum>()
            where TEnum : struct
        {
            return Enum.GetNames(typeof(TEnum)).Select(Enum.Parse<TEnum>).ToArray();
        }
    }
}
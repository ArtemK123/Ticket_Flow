using System;

namespace ProfileService.Models.Exceptions
{
    internal class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
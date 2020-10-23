using System;

namespace TicketFlow.Common.Exceptions
{
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(string serviceName)
            : base($"Service with name={serviceName} is not found")
        {
        }
    }
}
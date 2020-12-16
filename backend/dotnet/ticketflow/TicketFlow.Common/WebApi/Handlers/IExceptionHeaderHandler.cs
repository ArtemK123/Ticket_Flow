using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace TicketFlow.Common.WebApi.Handlers
{
    public interface IExceptionHeaderHandler
    {
        public void WriteExceptionHeader<TException>(IHeaderDictionary headers, TException exception)
            where TException : Exception;

        public bool IsExceptionInHeader<TException>(IHeaderDictionary headers)
            where TException : Exception;

        public bool IsExceptionInHeader<TException>(HttpResponseHeaders headers)
            where TException : Exception;
    }
}
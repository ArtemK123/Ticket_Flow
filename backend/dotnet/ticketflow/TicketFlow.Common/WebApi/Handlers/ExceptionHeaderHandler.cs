using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace TicketFlow.Common.WebApi.Handlers
{
    internal class ExceptionHeaderHandler : IExceptionHeaderHandler
    {
        private const string ExceptionHeaderName = "Caused-Exception";

        public void WriteExceptionHeader<TException>(IHeaderDictionary headers, TException exception)
            where TException : Exception
            => headers.Add(ExceptionHeaderName, typeof(TException).Name);

        public bool IsExceptionInHeader<TException>(IHeaderDictionary headers)
            where TException : Exception
            => headers.Contains(new KeyValuePair<string, StringValues>(ExceptionHeaderName, typeof(TException).Name));

        public bool IsExceptionInHeader<TException>(HttpResponseHeaders headers)
            where TException : Exception
            => headers.TryGetValues(ExceptionHeaderName, out IEnumerable<string> headerValues) && headerValues.Contains(typeof(TException).Name);
    }
}
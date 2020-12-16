using System.Net.Http;
using Microsoft.AspNetCore.Http;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.WebApi.Handlers;
using Xunit;

namespace TicketFlow.Common.Test.WebApi.Handlers
{
    public class ExceptionHeaderHandlerTest
    {
        private const string ExceptionHeaderName = "Caused-Exception";

        private readonly ExceptionHeaderHandler exceptionHeaderHandler;

        public ExceptionHeaderHandlerTest()
        {
            exceptionHeaderHandler = new ExceptionHeaderHandler();
        }

        [Fact]
        public void WriteExceptionHeader_ShouldAddExceptionTypeToHeaders()
        {
            NotFoundException exception = new NotFoundException("Test");
            IHeaderDictionary headers = new HeaderDictionary();

            exceptionHeaderHandler.WriteExceptionHeader(headers, exception);

            string headerValue = headers[ExceptionHeaderName];
            Assert.Equal("NotFoundException", headerValue);
        }

        [Fact]
        public void IsExceptionInHeader_HeaderDictionary_Found_ShouldReturnTrueWhenHeadersContainExceptionType()
        {
            IHeaderDictionary headers = new HeaderDictionary();
            headers[ExceptionHeaderName] = "NotFoundException";

            var actual = exceptionHeaderHandler.IsExceptionInHeader<NotFoundException>(headers);

            Assert.True(actual);
        }

        [Fact]
        public void IsExceptionInHeader_HeaderDictionary_NotFound_ShouldReturnFalseWhenHeadersDoesNotContainExceptionType()
        {
            IHeaderDictionary headers = new HeaderDictionary();

            var actual = exceptionHeaderHandler.IsExceptionInHeader<NotFoundException>(headers);

            Assert.False(actual);
        }

        [Fact]
        public void IsExceptionInHeader_HttpResponseHeaders_Found_ShouldReturnTrueWhenHeadersContainExceptionType()
        {
            HttpResponseMessage testResponse = new HttpResponseMessage();
            testResponse.Headers.Add(ExceptionHeaderName, "NotFoundException");

            var actual = exceptionHeaderHandler.IsExceptionInHeader<NotFoundException>(testResponse.Headers);

            Assert.True(actual);
        }

        [Fact]
        public void NotExceptionInHeader_HttpResponseHeaders_NotFound_ShouldReturnFalseWhenHeadersDoesNotContainExceptionType()
        {
            IHeaderDictionary headers = new HeaderDictionary();

            var actual = exceptionHeaderHandler.IsExceptionInHeader<NotFoundException>(headers);

            Assert.False(actual);
        }
    }
}
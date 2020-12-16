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
        public void IsExceptionInHeader_Found_ShouldReturnTrueWhenHeadersContainExceptionType()
        {
            IHeaderDictionary headers = new HeaderDictionary();
            headers[ExceptionHeaderName] = "NotFoundException";

            var actual = exceptionHeaderHandler.IsExceptionInHeader<NotFoundException>(headers);

            Assert.True(actual);
        }

        [Fact]
        public void NotExceptionInHeader_NotFound_ShouldReturnFalseWhenHeadersDoesNotContainExceptionType()
        {
            IHeaderDictionary headers = new HeaderDictionary();

            var actual = exceptionHeaderHandler.IsExceptionInHeader<NotFoundException>(headers);

            Assert.False(actual);
        }
    }
}
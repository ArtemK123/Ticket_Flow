using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TicketFlow.Common.Exceptions;
using TicketFlow.ProfileService.Client.Validators;
using Xunit;

namespace TicketFlow.ProfileService.Client.Test.Validators
{
    public class ProfileServiceResponseValidatorTest
    {
        private const string ExpectedExceptionMessage = "test message";
        private const string ExpectedInternalServiceExceptionMessage = "Internal error in Profile Service";

        private readonly ProfileServiceResponseValidator profileServiceResponseValidator;

        public ProfileServiceResponseValidatorTest()
        {
            profileServiceResponseValidator = new ProfileServiceResponseValidator();
        }

        [Fact]
        internal async Task ValidateAsync_NotFound_ShouldThrowNotFoundException_Async()
        {
            await RunValidateAsyncExceptionTestAsync<NotFoundException>(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent(ExpectedExceptionMessage)
                },
                ExpectedExceptionMessage);
        }

        [Fact]
        internal async Task ValidateAsync_BadRequest_ShouldThrowNotUniqueEntityException_Async()
        {
            await RunValidateAsyncExceptionTestAsync<NotUniqueEntityException>(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(ExpectedExceptionMessage)
                },
                ExpectedExceptionMessage);
        }

        [Fact]
        internal async Task ValidateAsync_InternalServerError_ShouldThrowInternalServiceException_Async()
        {
            await RunValidateAsyncExceptionTestAsync<InternalServiceException>(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                },
                ExpectedInternalServiceExceptionMessage);
        }

        [Fact]
        internal async Task ValidateAsync_ByDefault_ShouldPass_Async()
        {
            await profileServiceResponseValidator.ValidateAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Forbidden });
        }

        private async Task RunValidateAsyncExceptionTestAsync<TException>(HttpResponseMessage httpResponseMessage, string exceptionMessage)
            where TException : Exception
        {
            TException exception = await Assert.ThrowsAsync<TException>(() => profileServiceResponseValidator.ValidateAsync(httpResponseMessage));

            Assert.Equal(exceptionMessage, exception.Message);
        }
    }
}
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using TicketFlow.Common.Exceptions;
using TicketFlow.Common.WebApi.Handlers;
using TicketFlow.ProfileService.Client.Extensibility.Exceptions;
using TicketFlow.ProfileService.Client.Validators;
using Xunit;

namespace TicketFlow.ProfileService.Client.Test.Validators
{
    public class ProfileServiceResponseValidatorTest
    {
        private const string ExpectedExceptionMessage = "test message";
        private const string ExpectedInternalServiceExceptionMessage = "Internal error in Profile Service";

        private readonly IExceptionHeaderHandler exceptionHeaderHandler;
        private readonly ProfileServiceResponseValidator profileServiceResponseValidator;

        public ProfileServiceResponseValidatorTest()
        {
            exceptionHeaderHandler = Substitute.For<IExceptionHeaderHandler>();
            profileServiceResponseValidator = new ProfileServiceResponseValidator(exceptionHeaderHandler);
        }

        [Fact]
        internal async Task ValidateAsync_BadRequest_NotUniqueUserProfileException_ShouldThrowNotUniqueUserProfileException()
        {
            HttpResponseMessage response = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(ExpectedExceptionMessage) };
            exceptionHeaderHandler.IsExceptionInHeader<NotUniqueUserProfileException>(response.Headers).Returns(true);

            await RunValidateAsyncExceptionTestAsync<NotUniqueUserProfileException>(response, ExpectedExceptionMessage);
        }

        [Fact]
        internal async Task ValidateAsync_BadRequest_InvalidRequestModelException_ShouldThrowInvalidRequestModelException()
        {
            HttpResponseMessage response = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(ExpectedExceptionMessage) };
            exceptionHeaderHandler.IsExceptionInHeader<NotUniqueUserProfileException>(response.Headers).Returns(false);

            await RunValidateAsyncExceptionTestAsync<InvalidRequestModelException>(response, ExpectedExceptionMessage);
        }

        [Fact]
        internal async Task ValidateAsync_NotFound_ProfileNotFoundByIdException_ShouldThrowProfileNotFoundByIdException()
        {
            HttpResponseMessage response = new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound, Content = new StringContent(ExpectedExceptionMessage) };
            exceptionHeaderHandler.IsExceptionInHeader<ProfileNotFoundByIdException>(response.Headers).Returns(true);
            exceptionHeaderHandler.IsExceptionInHeader<ProfileNotFoundByUserEmailException>(response.Headers).Returns(false);

            await RunValidateAsyncExceptionTestAsync<ProfileNotFoundByIdException>(response, ExpectedExceptionMessage);
        }

        [Fact]
        internal async Task ValidateAsync_NotFound_ProfileNotFoundByUserEmailException_ShouldThrowProfileNotFoundByUserEmailException()
        {
            HttpResponseMessage response = new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound, Content = new StringContent(ExpectedExceptionMessage) };
            exceptionHeaderHandler.IsExceptionInHeader<ProfileNotFoundByIdException>(response.Headers).Returns(false);
            exceptionHeaderHandler.IsExceptionInHeader<ProfileNotFoundByUserEmailException>(response.Headers).Returns(true);

            await RunValidateAsyncExceptionTestAsync<ProfileNotFoundByUserEmailException>(response, ExpectedExceptionMessage);
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
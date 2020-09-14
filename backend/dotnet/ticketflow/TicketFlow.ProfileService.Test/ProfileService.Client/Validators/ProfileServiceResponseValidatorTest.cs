using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using TicketFlow.Common.Exceptions;
using TicketFlow.ProfileService.Client.Validators;

namespace TicketFlow.ProfileService.Test.ProfileService.Client.Validators
{
    [TestFixture]
    internal class ProfileServiceResponseValidatorTest
    {
        private const string ValidateAsyncMethodName = nameof(ProfileServiceResponseValidator.ValidateAsync) + ". ";
        private const string ExpectedExceptionMessage = "test message";
        private const string ExpectedInternalServiceExceptionMessage = "Internal error in Profile Service";

        private ProfileServiceResponseValidator profileServiceResponseValidator;

        [SetUp]
        public void TestInitialize()
        {
            profileServiceResponseValidator = new ProfileServiceResponseValidator();
        }

        [TestCase(TestName = ValidateAsyncMethodName + "Should throw NotFoundException for HttpStatusCode.NotFound")]
        public void NotFoundExceptionTest()
        {
            RunValidateAsyncExceptionTest<NotFoundException>(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent(ExpectedExceptionMessage)
                },
                ExpectedExceptionMessage);
        }

        [TestCase(TestName = ValidateAsyncMethodName + "Should throw NotUniqueEntityException for HttpStatusCode.BadRequest")]
        public void NotUniqueEntityExceptionTest()
        {
            RunValidateAsyncExceptionTest<NotUniqueEntityException>(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(ExpectedExceptionMessage)
                },
                ExpectedExceptionMessage);
        }

        [TestCase(TestName = ValidateAsyncMethodName + "Should throw InternalServiceException for HttpStatusCode.InternalServerError")]
        public void InternalServiceExceptionTest()
        {
            RunValidateAsyncExceptionTest<InternalServiceException>(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                },
                ExpectedInternalServiceExceptionMessage);
        }

        [TestCase(TestName = ValidateAsyncMethodName + "Should just pass by default")]
        public async Task DefaultTest()
        {
            await profileServiceResponseValidator.ValidateAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Forbidden });
        }

        private void RunValidateAsyncExceptionTest<TException>(HttpResponseMessage httpResponseMessage, string exceptionMessage)
            where TException : Exception
        {
            TException exception = Assert.ThrowsAsync<TException>(() => profileServiceResponseValidator.ValidateAsync(httpResponseMessage));
            Assert.AreEqual(exceptionMessage, exception.Message);
        }
    }
}
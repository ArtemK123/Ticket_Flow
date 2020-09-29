using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TicketFlow.Common.Providers;
using Xunit;

namespace TicketFlow.Common.Test.Providers.UrlFromConfigProviderTests
{
    public abstract class UrlFromConfigProviderTestBase
    {
        protected const string ServiceBaseUrl = "http://localhost";
        protected const int Port = 12345;

        private readonly UrlFromConfigProvider urlFromConfigProvider;

        protected UrlFromConfigProviderTestBase()
        {
            urlFromConfigProvider = new UrlFromConfigProvider();
        }

        protected static int GetPortFromUrl(string url) => new Uri(url).Port;

        protected void RunGetUrlTest(IReadOnlyDictionary<string, string> settings, Action<string> assertAction)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            string actual = urlFromConfigProvider.GetUrl(configuration);

            assertAction(actual);
        }

        protected void RunGetUrlThrowsExceptionTest<TException>(IReadOnlyDictionary<string, string> settings, Action<TException> assertAction)
            where TException : Exception
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            TException exception = Assert.Throws<TException>(() => urlFromConfigProvider.GetUrl(configuration));

            assertAction(exception);
        }
    }
}
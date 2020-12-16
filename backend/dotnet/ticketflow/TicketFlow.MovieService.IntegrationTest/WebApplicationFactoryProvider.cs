using System;
using System.Net.Http;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TicketFlow.MovieService.Client.Extensibility.DependencyInjection;
using TicketFlow.MovieService.IntegrationTest.Fakes;
using TicketFlow.MovieService.Persistence;

namespace TicketFlow.MovieService.IntegrationTest
{
    internal class WebApplicationFactoryProvider
    {
        public WebApplicationFactory<Startup> GetWebApplicationFactory()
        {
            HttpClient client = default;

            WebApplicationFactory<Startup> localWebApplicationFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton(serviceProvider => Substitute.For<IMigrationRunner>());
                        services.AddSingleton<ICinemaHallRepository, FakeCinemaHallRepository>();
                        services.AddSingleton<IFilmRepository, FakeFilmRepository>();
                        services.AddSingleton<IMovieRepository, FakeMovieRepository>();
                        services.AddSingleton(serviceProvider => new Lazy<IHttpClientFactory>(() =>
                        {
                            var mock = Substitute.For<IHttpClientFactory>();

                            // ReSharper disable once AccessToModifiedClosure - It is expected closure, which is used for testing service proxy
                            mock.CreateClient(default).ReturnsForAnyArgs(client);
                            return mock;
                        }));
                        services.AddMovieService();
                    });
                });

            client = localWebApplicationFactory.CreateClient();

            return localWebApplicationFactory;
        }
    }
}
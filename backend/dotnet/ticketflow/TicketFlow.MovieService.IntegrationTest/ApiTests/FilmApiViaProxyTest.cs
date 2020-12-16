using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;
using Xunit;

namespace TicketFlow.MovieService.IntegrationTest.ApiTests
{
    public class FilmApiViaProxyTest : IDisposable
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly IFilmApiProxy proxy;

        public FilmApiViaProxyTest()
        {
            webApplicationFactory = new WebApplicationFactoryProvider().GetWebApplicationFactory();
            proxy = webApplicationFactory.Services.GetService<IFilmApiProxy>();
        }

        public void Dispose()
        {
            webApplicationFactory?.Dispose();
        }

        [Fact]
        public async Task Add_ThenGetAll_ShouldReturnAllFilms()
        {
            IReadOnlyCollection<FilmCreationModel> creationModels = new[]
            {
                new FilmCreationModel("Title1", "Description1", new DateTime(1, 1, 1), "Creator1", 1, 1),
                new FilmCreationModel("Title2", "Description2", new DateTime(2, 2, 2), "Creator2", 2, 2),
                new FilmCreationModel("Title3", "Description3", new DateTime(3, 3, 3), "Creator3", 3, 3),
            };

            List<int> addedFilmIds = new List<int>();

            foreach (FilmCreationModel creationModel in creationModels)
            {
                int id = await proxy.AddAsync(creationModel);
                addedFilmIds.Add(id);
            }

            IReadOnlyCollection<IFilm> fetchedCinemaHalls = await proxy.GetAllAsync();

            Assert.True(fetchedCinemaHalls.All(cinemaHall => creationModels.Any(creationModel => SameFilm(cinemaHall, creationModel))));
            Assert.True(fetchedCinemaHalls.All(cinemaHall => addedFilmIds.Contains(cinemaHall.Id)));
        }

        private static bool SameFilm(IFilm film, FilmCreationModel creationModel)
            => film.Creator == creationModel.Creator
               && film.Description == creationModel.Description
               && film.Duration == creationModel.Duration
               && film.Title == creationModel.Title
               && film.AgeLimit == creationModel.AgeLimit
               && film.PremiereDate == creationModel.PremiereDate;
    }
}
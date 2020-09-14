using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;
using TicketFlow.MovieService.Client.Extensibility.Proxies;

namespace TicketFlow.ApiGateway.StartupServices.Seeders
{
    internal class FilmsSeeder : IFilmsSeeder
    {
        private readonly IFilmApiProxy filmApiProxy;

        private readonly IReadOnlyCollection<FilmCreationModel> filmsToSeed = new[]
        {
            new FilmCreationModel("Terminator", "I will be back", new DateTime(1984, 11, 26), "James Cameron", 107, 13),
            new FilmCreationModel("Star wars - A New Hope", "In the galaxy far far away...", new DateTime(1977, 5, 25), "George Lucas", 121, 8),
            new FilmCreationModel("Deadpool", "X gonna give it to ya", new DateTime(2016, 2, 8), "20th Century Fox", 108, 18),
            new FilmCreationModel(
                "The Godfather",
                "Now you come to me and you say 'Don Corleone, give me justice', but you don't even ask with respect. You don't offer friendship. You don't even think to call me Godfather",
                new DateTime(1972, 3, 14),
                "Paramount Pictures",
                177,
                13),
            new FilmCreationModel("The Good, the Bad and the Ugly", "The best trio", new DateTime(1966, 12, 23), "Sergio Leone", 177, 13),
            new FilmCreationModel("Fight Club", "Where is my mind? In the water, see it swimming", new DateTime(1999, 9, 10), "David Fincher", 139, 16),
            new FilmCreationModel("The matrix", "The spoon is a lie!", new DateTime(1999, 3, 31), "The Wachowskis", 136, 13),
            new FilmCreationModel("Parasite", "This film will impress you", new DateTime(2019, 5, 29), "CJ Entertainment", 132, 13),
            new FilmCreationModel("Forrest Gump", "Run, Forrest, Run", new DateTime(1994, 6, 23), "Robert Zemeckis", 142, 0),
            new FilmCreationModel("Back in future", "you built a time machine..... out of a delorian ?", new DateTime(1985, 7, 3), "Robert Zemeckis", 116, 0)
        };

        public FilmsSeeder(IFilmApiProxy filmApiProxy)
        {
            this.filmApiProxy = filmApiProxy;
        }

        public async Task SeedAsync()
        {
            IReadOnlyCollection<IFilm> storedFilms = await filmApiProxy.GetAllAsync();
            if (storedFilms.Count == 0)
            {
                await Task.WhenAll(filmsToSeed.Select(cinemaHall => filmApiProxy.AddAsync(cinemaHall)));
            }
        }
    }
}
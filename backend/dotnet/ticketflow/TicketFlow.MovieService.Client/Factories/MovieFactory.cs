using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Client.Entities;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Factories;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;

namespace TicketFlow.MovieService.Client.Factories
{
    internal class MovieFactory : IMovieFactory
    {
        private readonly IRandomValueProvider randomValueProvider;

        public MovieFactory(IRandomValueProvider randomValueProvider)
        {
            this.randomValueProvider = randomValueProvider;
        }

        public IMovie Create(StoredMovieCreationModel creationModel)
            => new Movie(creationModel.Id, creationModel.StartTime, creationModel.Film, creationModel.CinemaHall);

        public IMovie Create(MovieCreationModel creationModel)
        {
            int id = randomValueProvider.GetRandomInt(0, int.MaxValue);
            return new Movie(id, creationModel.StartTime, creationModel.Film, creationModel.CinemaHall);
        }
    }
}
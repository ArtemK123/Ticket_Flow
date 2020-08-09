using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;

namespace TicketFlow.MovieService.Service.Factories
{
    internal class MovieFactory : IMovieFactory
    {
        private readonly IRandomValueProvider randomValueProvider;

        public MovieFactory(IRandomValueProvider randomValueProvider)
        {
            this.randomValueProvider = randomValueProvider;
        }

        public IMovie Create(MovieCreationModel creationModel)
        {
            int id = randomValueProvider.GetRandomInt(0, int.MaxValue);
            return new Movie(id, creationModel.StartTime, creationModel.Film, creationModel.CinemaHall);
        }
    }
}
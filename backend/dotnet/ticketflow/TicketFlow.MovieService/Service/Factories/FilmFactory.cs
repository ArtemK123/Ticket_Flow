using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;

namespace TicketFlow.MovieService.Service.Factories
{
    internal class FilmFactory : IFilmFactory
    {
        private readonly IRandomValueProvider randomValueProvider;

        public FilmFactory(IRandomValueProvider randomValueProvider)
        {
            this.randomValueProvider = randomValueProvider;
        }

        public IFilm Create(FilmCreationModel creationModel)
        {
            int id = randomValueProvider.GetRandomInt(0, int.MaxValue);
            return new Film(id, creationModel.Title, creationModel.Description, creationModel.PremiereDate, creationModel.Creator, creationModel.Duration, creationModel.AgeLimit);
        }
    }
}
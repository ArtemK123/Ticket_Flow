using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Client.Entities;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Factories;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;

namespace TicketFlow.MovieService.Client.Factories
{
    internal class FilmFactory : IFilmFactory
    {
        private readonly IRandomValueProvider randomValueProvider;

        public FilmFactory(IRandomValueProvider randomValueProvider)
        {
            this.randomValueProvider = randomValueProvider;
        }

        public IFilm Create(StoredFilmCreationModel creationModel)
            => new Film(creationModel.Id, creationModel.Title, creationModel.Description, creationModel.PremiereDate, creationModel.Creator, creationModel.Duration, creationModel.AgeLimit);

        public IFilm Create(FilmCreationModel creationModel)
        {
            int id = randomValueProvider.GetRandomInt(0, int.MaxValue);
            return new Film(id, creationModel.Title, creationModel.Description, creationModel.PremiereDate, creationModel.Creator, creationModel.Duration, creationModel.AgeLimit);
        }
    }
}
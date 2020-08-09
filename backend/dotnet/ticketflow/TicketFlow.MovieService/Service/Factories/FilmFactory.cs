using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.FilmModels;

namespace TicketFlow.MovieService.Service.Factories
{
    internal class FilmFactory : EntityFactoryBase<IFilm, FilmCreationModel>, IEntityFactory<IFilm, StoredFilmCreationModel>
    {
        public FilmFactory(IRandomValueProvider randomValueProvider)
            : base(randomValueProvider)
        {
        }

        public IFilm Create(StoredFilmCreationModel creationModel)
            => new Film(creationModel.Id, creationModel.Title, creationModel.Description, creationModel.PremiereDate, creationModel.Creator, creationModel.Duration, creationModel.AgeLimit);

        protected override IFilm CreateEntityFromModel(int id, FilmCreationModel creationModel)
            => new Film(id, creationModel.Title, creationModel.Description, creationModel.PremiereDate, creationModel.Creator, creationModel.Duration, creationModel.AgeLimit);
    }
}
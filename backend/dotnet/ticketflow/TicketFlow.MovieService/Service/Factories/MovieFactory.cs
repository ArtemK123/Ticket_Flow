using TicketFlow.Common.Providers;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models.MovieModels;

namespace TicketFlow.MovieService.Service.Factories
{
    internal class MovieFactory : EntityFactoryBase<IMovie, MovieCreationModel>, IEntityFactory<IMovie, StoredMovieCreationModel>
    {
        public MovieFactory(IRandomValueProvider randomValueProvider)
            : base(randomValueProvider)
        {
        }

        public IMovie Create(StoredMovieCreationModel creationModel)
            => new Movie(creationModel.Id, creationModel.StartTime, creationModel.Film, creationModel.CinemaHall);

        protected override IMovie CreateEntityFromModel(int id, MovieCreationModel creationModel) => new Movie(id, creationModel.StartTime, creationModel.Film, creationModel.CinemaHall);
    }
}
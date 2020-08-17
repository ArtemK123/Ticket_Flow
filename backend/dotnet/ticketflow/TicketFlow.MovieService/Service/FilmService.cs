using System.Collections.Generic;
using TicketFlow.Common.Factories;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Exceptions;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;
using TicketFlow.MovieService.Persistence;

namespace TicketFlow.MovieService.Service
{
    internal class FilmService : IFilmService
    {
        private readonly IFilmRepository filmRepository;
        private readonly IEntityFactory<IFilm, FilmCreationModel> entityFactory;

        public FilmService(IFilmRepository filmRepository, IEntityFactory<IFilm, FilmCreationModel> entityFactory)
        {
            this.filmRepository = filmRepository;
            this.entityFactory = entityFactory;
        }

        public IReadOnlyCollection<IFilm> GetAll()
            => filmRepository.GetAll();

        public IFilm GetById(int id)
            => filmRepository.TryGet(id, out IFilm film)
                ? film
                : throw new NotFoundException($"Film with id=${id} is not found");

        public IFilm Add(FilmCreationModel creationModel)
        {
            var addedEntity = entityFactory.Create(creationModel);
            filmRepository.Add(addedEntity);
            return addedEntity;
        }
    }
}
using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Exceptions;
using TicketFlow.MovieService.Domain.Models;
using TicketFlow.MovieService.Persistence;
using TicketFlow.MovieService.Service.Factories;

namespace TicketFlow.MovieService.Service
{
    internal class FilmService : IFilmService
    {
        private readonly IFilmRepository filmRepository;
        private readonly IFilmFactory filmFactory;

        public FilmService(IFilmRepository filmRepository, IFilmFactory filmFactory)
        {
            this.filmRepository = filmRepository;
            this.filmFactory = filmFactory;
        }

        public IReadOnlyCollection<IFilm> GetAll()
            => filmRepository.GetAll();

        public IFilm GetById(int id)
            => filmRepository.TryGetById(id, out IFilm film)
                ? film
                : throw new NotFoundException($"Film with id=${id} is not found");

        public IFilm Add(FilmCreationModel creationModel)
        {
            var addedEntity = filmFactory.Create(creationModel);
            filmRepository.Add(addedEntity);
            return addedEntity;
        }
    }
}
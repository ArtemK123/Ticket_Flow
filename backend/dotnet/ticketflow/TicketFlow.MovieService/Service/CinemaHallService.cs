using System.Collections.Generic;
using TicketFlow.Common.Exceptions;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Factories;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Persistence;

namespace TicketFlow.MovieService.Service
{
    internal class CinemaHallService : ICinemaHallService
    {
        private readonly ICinemaHallRepository cinemaHallRepository;
        private readonly ICinemaHallFactory entityFactory;

        public CinemaHallService(ICinemaHallRepository cinemaHallRepository, ICinemaHallFactory entityFactory)
        {
            this.cinemaHallRepository = cinemaHallRepository;
            this.entityFactory = entityFactory;
        }

        public IReadOnlyCollection<ICinemaHall> GetAll()
            => cinemaHallRepository.GetAll();

        public ICinemaHall GetById(int id)
            => cinemaHallRepository.TryGet(id, out ICinemaHall cinemaHall)
                ? cinemaHall
                : throw new NotFoundException($"Cinema hall with id=${id} is not found");

        public ICinemaHall Add(CinemaHallCreationModel cinemaHallCreationModel)
        {
            ICinemaHall addedEntity = entityFactory.Create(cinemaHallCreationModel);
            cinemaHallRepository.Add(addedEntity);
            return addedEntity;
        }
    }
}
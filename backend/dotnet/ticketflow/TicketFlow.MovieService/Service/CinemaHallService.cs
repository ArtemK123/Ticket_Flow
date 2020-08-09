using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Exceptions;
using TicketFlow.MovieService.Domain.Models;
using TicketFlow.MovieService.Domain.Models.CinemaHallModels;
using TicketFlow.MovieService.Persistence;
using TicketFlow.MovieService.Service.Factories;

namespace TicketFlow.MovieService.Service
{
    internal class CinemaHallService : ICinemaHallService
    {
        private readonly ICinemaHallRepository cinemaHallRepository;
        private readonly ICinemaHallFactory cinemaHallFactory;

        public CinemaHallService(ICinemaHallRepository cinemaHallRepository, ICinemaHallFactory cinemaHallFactory)
        {
            this.cinemaHallRepository = cinemaHallRepository;
            this.cinemaHallFactory = cinemaHallFactory;
        }

        public IReadOnlyCollection<ICinemaHall> GetAll()
            => cinemaHallRepository.GetAll();

        public ICinemaHall GetById(int id)
            => cinemaHallRepository.TryGetById(id, out ICinemaHall cinemaHall)
                ? cinemaHall
                : throw new NotFoundException($"Cinema hall with id=${id} is not found");

        public ICinemaHall Add(CinemaHallCreationModel cinemaHallCreationModel)
        {
            ICinemaHall addedEntity = cinemaHallFactory.Create(cinemaHallCreationModel);
            cinemaHallRepository.Add(addedEntity);
            return addedEntity;
        }
    }
}
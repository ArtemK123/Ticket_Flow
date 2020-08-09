using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;

namespace TicketFlow.MovieService.Service
{
    public interface IMovieService
    {
        IReadOnlyCollection<IMovie> GetAll();

        IMovie GetById(int id);

        IMovie Add(MovieCreationModel creationModel);
    }
}
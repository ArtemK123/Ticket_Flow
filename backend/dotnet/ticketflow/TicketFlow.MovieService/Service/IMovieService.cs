using System.Collections.Generic;
using TicketFlow.MovieService.Domain.Entities;
using TicketFlow.MovieService.Domain.Models;
using TicketFlow.MovieService.Domain.Models.MovieModels;

namespace TicketFlow.MovieService.Service
{
    public interface IMovieService
    {
        IReadOnlyCollection<IMovie> GetAll();

        IMovie GetById(int id);

        IMovie Add(MovieCreationIdReferencedModel creationIdReferencedModel);
    }
}
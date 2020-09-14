using System.Collections.Generic;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;

namespace TicketFlow.MovieService.Service
{
    public interface IMovieService
    {
        IReadOnlyCollection<IMovie> GetAll();

        IMovie GetById(int id);

        IMovie Add(MovieCreationIdReferencedModel creationIdReferencedModel);
    }
}
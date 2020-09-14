using System;
using TicketFlow.MovieService.Client.Extensibility.Entities;

namespace TicketFlow.ApiGateway.WebApi.MoviesApi.Models
{
    public class ShortMovieModel
    {
        public ShortMovieModel()
        {
        }

        public ShortMovieModel(IMovie movie)
        {
            Id = movie.Id;
            Title = movie.Film.Title;
            StartTime = movie.StartTime;
            CinemaHallName = movie.CinemaHall.Name;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public string CinemaHallName { get; set; }
    }
}
using System;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;

namespace TicketFlow.MovieService.Client.Extensibility.Models.MovieModels
{
    public class MovieSerializationModel
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public FilmSerializationModel Film { get; set; }

        public CinemaHallSerializationModel CinemaHall { get; set; }
    }
}
namespace TicketFlow.MovieService.WebApi.CinemaHallsApi.ClientModels
{
    public class CinemaHallCreationApiModel
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public int SeatRows { get; set; }

        public int SeatsInRow { get; set; }
    }
}
namespace TicketFlow.MovieService.WebApi.ClientModels.Requests
{
    public class AddCinemaHallRequest
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public int SeatRows { get; set; }

        public int SeatsInRow { get; set; }
    }
}
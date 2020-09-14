namespace TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels
{
    public class CinemaHallSerializationModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int SeatRows { get; set; }

        public int SeatsInRow { get; set; }
    }
}
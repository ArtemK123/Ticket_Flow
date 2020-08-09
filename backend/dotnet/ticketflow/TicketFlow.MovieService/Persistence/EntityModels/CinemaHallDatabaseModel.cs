namespace TicketFlow.MovieService.Persistence.EntityModels
{
    public class CinemaHallDatabaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int SeatRows { get; set; }

        public int SeatsInRow { get; set; }
    }
}
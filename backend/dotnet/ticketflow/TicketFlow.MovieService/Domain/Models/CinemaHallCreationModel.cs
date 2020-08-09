namespace TicketFlow.MovieService.Domain.Models
{
    public class CinemaHallCreationModel
    {
        public CinemaHallCreationModel(string name, string location, int seatRows, int seatsInRow)
        {
            Name = name;
            Location = location;
            SeatRows = seatRows;
            SeatsInRow = seatsInRow;
        }

        public string Name { get; }

        public string Location { get; }

        public int SeatRows { get; }

        public int SeatsInRow { get; }
    }
}
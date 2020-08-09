namespace TicketFlow.MovieService.Domain.Entities
{
    internal class CinemaHall : ICinemaHall
    {
        public CinemaHall(int id, string name, string location, int seatRows, int seatsInRow)
        {
            Id = id;
            Name = name;
            Location = location;
            SeatRows = seatRows;
            SeatsInRow = seatsInRow;
        }

        public int Id { get; }

        public string Name { get; }

        public string Location { get; }

        public int SeatRows { get; }

        public int SeatsInRow { get; }
    }
}
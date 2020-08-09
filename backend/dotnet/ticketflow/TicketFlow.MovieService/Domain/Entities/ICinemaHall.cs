namespace TicketFlow.MovieService.Domain.Entities
{
    public interface ICinemaHall
    {
        public int Id { get; }

        public string Name { get; }

        public string Location { get; }

        public int SeatRows { get; }

        public int SeatsInRow { get; }
    }
}
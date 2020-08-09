namespace TicketFlow.MovieService.Domain.Models.CinemaHallModels
{
    public class StoredCinemaHallCreationModel : CinemaHallCreationModel
    {
        public StoredCinemaHallCreationModel(int id, string name, string location, int seatRows, int seatsInRow)
            : base(name, location, seatRows, seatsInRow)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
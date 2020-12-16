using System.Collections.Generic;
using System.Linq;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.CinemaHallModels;
using TicketFlow.MovieService.Client.Extensibility.Serializers;
using TicketFlow.MovieService.Persistence;

namespace TicketFlow.MovieService.IntegrationTest.Fakes
{
    internal class FakeCinemaHallRepository : ICinemaHallRepository
    {
        private readonly ICinemaHallSerializer cinemaHallSerializer;
        private readonly List<CinemaHallSerializationModel> storedCinemaHallModels;

        public FakeCinemaHallRepository(ICinemaHallSerializer cinemaHallSerializer)
        {
            this.cinemaHallSerializer = cinemaHallSerializer;
            storedCinemaHallModels = new List<CinemaHallSerializationModel>();
        }

        public bool TryGet(int identifier, out ICinemaHall entity)
        {
            CinemaHallSerializationModel cinemaHallModel = storedCinemaHallModels.FirstOrDefault(model => model.Id == identifier);
            if (cinemaHallModel != null)
            {
                entity = cinemaHallSerializer.Deserialize(cinemaHallModel);
                return true;
            }

            entity = default;
            return false;
        }

        public IReadOnlyCollection<ICinemaHall> GetAll()
            => storedCinemaHallModels.Select(cinemaHall => cinemaHallSerializer.Deserialize(cinemaHall)).ToArray();

        public void Add(ICinemaHall entity)
        {
            CinemaHallSerializationModel model = cinemaHallSerializer.Serialize(entity);
            storedCinemaHallModels.Add(model);
        }

        public void Update(ICinemaHall entity)
        {
            if (TryGet(entity.Id, out ICinemaHall _))
            {
                Delete(entity.Id);
                Add(entity);
            }
        }

        public void Delete(int identifier)
        {
            CinemaHallSerializationModel cinemaHallModel = storedCinemaHallModels.FirstOrDefault(model => model.Id == identifier);
            if (cinemaHallModel != null)
            {
                storedCinemaHallModels.Remove(cinemaHallModel);
            }
        }
    }
}
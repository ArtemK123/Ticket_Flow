using System.Collections.Generic;
using System.Linq;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.MovieModels;
using TicketFlow.MovieService.Client.Extensibility.Serializers;
using TicketFlow.MovieService.Persistence;

namespace TicketFlow.MovieService.IntegrationTest.Fakes
{
    internal class FakeMovieRepository : IMovieRepository
    {
        private readonly IMovieSerializer movieSerializer;
        private readonly List<MovieSerializationModel> storedMovieModels;

        public FakeMovieRepository(IMovieSerializer movieSerializer)
        {
            this.movieSerializer = movieSerializer;
            storedMovieModels = new List<MovieSerializationModel>();
        }

        public bool TryGet(int identifier, out IMovie entity)
        {
            MovieSerializationModel movieModel = storedMovieModels.FirstOrDefault(model => model.Id == identifier);
            if (movieModel != null)
            {
                entity = movieSerializer.Deserialize(movieModel);
                return true;
            }

            entity = default;
            return false;
        }

        public IReadOnlyCollection<IMovie> GetAll()
            => storedMovieModels.Select(movie => movieSerializer.Deserialize(movie)).ToArray();

        public void Add(IMovie entity)
        {
            MovieSerializationModel model = movieSerializer.Serialize(entity);
            storedMovieModels.Add(model);
        }

        public void Update(IMovie entity)
        {
            if (TryGet(entity.Id, out IMovie _))
            {
                Delete(entity.Id);
                Add(entity);
            }
        }

        public void Delete(int identifier)
        {
            MovieSerializationModel movieModel = storedMovieModels.FirstOrDefault(model => model.Id == identifier);
            if (movieModel != null)
            {
                storedMovieModels.Remove(movieModel);
            }
        }
    }
}
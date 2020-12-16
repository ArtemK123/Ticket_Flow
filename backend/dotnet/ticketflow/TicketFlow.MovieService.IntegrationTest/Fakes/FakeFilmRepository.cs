using System.Collections.Generic;
using System.Linq;
using TicketFlow.MovieService.Client.Extensibility.Entities;
using TicketFlow.MovieService.Client.Extensibility.Models.FilmModels;
using TicketFlow.MovieService.Client.Extensibility.Serializers;
using TicketFlow.MovieService.Persistence;

namespace TicketFlow.MovieService.IntegrationTest.Fakes
{
    internal class FakeFilmRepository : IFilmRepository
    {
        private readonly IFilmSerializer filmSerializer;
        private readonly List<FilmSerializationModel> storedFilmModels;

        public FakeFilmRepository(IFilmSerializer filmSerializer)
        {
            this.filmSerializer = filmSerializer;
            storedFilmModels = new List<FilmSerializationModel>();
        }

        public bool TryGet(int identifier, out IFilm entity)
        {
            FilmSerializationModel filmModel = storedFilmModels.FirstOrDefault(model => model.Id == identifier);
            if (filmModel != null)
            {
                entity = filmSerializer.Deserialize(filmModel);
                return true;
            }

            entity = default;
            return false;
        }

        public IReadOnlyCollection<IFilm> GetAll()
            => storedFilmModels.Select(film => filmSerializer.Deserialize(film)).ToArray();

        public void Add(IFilm entity)
        {
            FilmSerializationModel model = filmSerializer.Serialize(entity);
            storedFilmModels.Add(model);
        }

        public void Update(IFilm entity)
        {
            if (TryGet(entity.Id, out IFilm _))
            {
                Delete(entity.Id);
                Add(entity);
            }
        }

        public void Delete(int identifier)
        {
            FilmSerializationModel filmModel = storedFilmModels.FirstOrDefault(model => model.Id == identifier);
            if (filmModel != null)
            {
                storedFilmModels.Remove(filmModel);
            }
        }
    }
}
using System.Collections.Generic;

namespace TicketFlow.MovieService.Persistence
{
    public interface ICrudRepository<in TIdentifier, TEntity>
    {
        bool TryGet(TIdentifier identifier, out TEntity entity);

        IReadOnlyCollection<TEntity> GetAll();

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TIdentifier identifier);
    }
}
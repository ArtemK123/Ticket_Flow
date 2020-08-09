using System.Collections.Generic;

namespace TicketFlow.MovieService.Persistence
{
    public interface ICrudRepository<TEntity, in TIdentifier>
    {
        bool TryGet(TIdentifier id, out TEntity entity);

        IReadOnlyCollection<TEntity> GetAll();

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TIdentifier id);
    }
}
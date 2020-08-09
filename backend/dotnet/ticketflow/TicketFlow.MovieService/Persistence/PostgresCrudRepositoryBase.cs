using System.Collections.Generic;
using TicketFlow.Common.Providers;

namespace TicketFlow.MovieService.Persistence
{
    public abstract class PostgresCrudRepositoryBase<TEntity, TIdentifier> : ICrudRepository<TEntity, TIdentifier>
    {
        private readonly IPostgresDbConnectionProvider dbConnectionProvider;

        protected PostgresCrudRepositoryBase(IPostgresDbConnectionProvider dbConnectionProvider)
        {
            this.dbConnectionProvider = dbConnectionProvider;
        }

        public bool TryGet(TIdentifier id, out TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TEntity> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Add(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(TIdentifier id)
        {
            throw new System.NotImplementedException();
        }
    }
}
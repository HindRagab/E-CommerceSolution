using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.DbContexts;

namespace E_Commerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = [];
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var EntityType = typeof(TEntity);

            if (_repositories.TryGetValue(EntityType , out object? repository))
                return (IGenaricRepository<TEntity, TKey>)repository;

            var newRepo = new GenaricRepository<TEntity, TKey>(_dbContext);
            _repositories[EntityType] = newRepo;
            return newRepo;
        }
        public Task<int> SavaChangesAsync() => _dbContext.SaveChangesAsync();
    }
}

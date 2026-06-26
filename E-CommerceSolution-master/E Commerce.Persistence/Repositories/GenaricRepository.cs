using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce.Persistence.Repositories
{
    public class GenaricRepository<TEntity, TKey> : IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;

        public GenaricRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();
        public async Task<TEntity?> GetByIdAsunc(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);
        public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationsEvaluator.CreateQuery
                (_dbContext.Set<TEntity>(), specifications).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsunc(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationsEvaluator.CreateQuery
                (_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }

        public Task<int> CounAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return SpecificationsEvaluator.CreateQuery
                (_dbContext.Set<TEntity>(), specifications).CountAsync();
        }
    }
}

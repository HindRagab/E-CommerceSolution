using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenaricRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task AddAsync(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity?> GetByIdAsunc(TKey id);
        Task<TEntity?> GetByIdAsunc(ISpecifications<TEntity , TKey> specifications);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity , TKey> specifications);
        Task<int> CounAsync(ISpecifications<TEntity, TKey> specifications);
    }
}

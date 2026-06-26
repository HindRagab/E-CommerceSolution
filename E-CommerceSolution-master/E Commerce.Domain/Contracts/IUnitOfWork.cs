using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SavaChangesAsync();

        IGenaricRepository<TEntity , TKey> GetRepository<TEntity , TKey>() where TEntity : BaseEntity<TKey>;
    }
}

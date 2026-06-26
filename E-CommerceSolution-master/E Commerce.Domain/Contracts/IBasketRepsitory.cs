using E_Commerce.Domain.Entities.BasketModule;

namespace E_Commerce.Domain.Contracts
{
    public interface IBasketRepsitory
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeTolive = default);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}

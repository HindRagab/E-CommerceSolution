namespace E_Commerce.Domain.Entities.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } = default!; // GUID : Created From Client [Frontend]
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}

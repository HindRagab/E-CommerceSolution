namespace E_Commerce.Domain.Entities
{
    public abstract class BaseEntity<TKet>
    {
        public TKet Id { get; set; } = default!;
    }
}

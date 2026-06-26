namespace E_Commerce.Domain.Entities.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; } // FK
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal Subtotal { get; set; } // Total Price Of Items
        //public decimal Total { get; set; } // Subtotal + DeliveryMethodCost

        public decimal GetTotal() => Subtotal + DeliveryMethod.Price;
    }
}

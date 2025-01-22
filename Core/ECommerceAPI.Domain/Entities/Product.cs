using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}

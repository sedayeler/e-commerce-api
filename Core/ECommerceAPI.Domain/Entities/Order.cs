using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}

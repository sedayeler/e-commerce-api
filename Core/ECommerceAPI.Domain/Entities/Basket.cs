using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}

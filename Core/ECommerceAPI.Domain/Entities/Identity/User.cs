using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Domain.Entities.Identity
{
    public class User : IdentityUser<string>
    {
        public int BasketId { get; set; }
        public string FullName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public Basket Basket { get; set; }
    }
}

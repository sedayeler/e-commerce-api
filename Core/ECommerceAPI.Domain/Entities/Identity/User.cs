using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Domain.Entities.Identity
{
    public class User : IdentityUser<string>
    {
        public string FullName { get; set; }
    }
}

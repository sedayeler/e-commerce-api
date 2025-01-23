using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Domain.Entities
{
    public class Endpoint : BaseEntity
    {
        public string HttpType { get; set; }
        public string ActionType { get; set; }
        public string Definition { get; set; }
        public string Code { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}

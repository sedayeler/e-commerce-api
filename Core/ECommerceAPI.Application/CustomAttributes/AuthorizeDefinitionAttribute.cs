using ECommerceAPI.Domain.Enums;

namespace ECommerceAPI.Application.CustomAttributes
{
    public class AuthorizeDefinitionAttribute : Attribute
    {
        public string Menu { get; set; }
        public ActionType ActionType { get; set; }
        public string Definition { get; set; }
    }
}

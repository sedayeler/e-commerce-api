using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs.Basket
{
    public class CreateBasketItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

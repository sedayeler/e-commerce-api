using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class UpdateBasketItemRequest
    {
        public int BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}

using ECommerceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs.Order
{
    public class UpdateOrderStatusRequest
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}

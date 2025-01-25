using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class ListOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}

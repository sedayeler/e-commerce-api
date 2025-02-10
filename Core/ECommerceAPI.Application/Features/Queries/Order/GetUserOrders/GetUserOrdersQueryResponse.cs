using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.DTOs;

namespace ECommerceAPI.Application.Features.Queries.Order.GetUserOrders
{
    public class GetUserOrdersQueryResponse
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
        public ICollection<ListOrderItem> OrderItems { get; set; }
    }
}

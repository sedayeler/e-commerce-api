using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Order.GetUserOrders
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQueryRequest, List<GetUserOrdersQueryResponse>>
    {
        private readonly IOrderService _orderService;

        public GetUserOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<GetUserOrdersQueryResponse>> Handle(GetUserOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            List<ListOrder> orders = await _orderService.GetUserOrdersAsync();

            return orders.Select(o => new GetUserOrdersQueryResponse()
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                Address = o.Address,
                Status = o.Status.ToString(),
                TotalPrice = o.TotalPrice,
                UserId = o.UserId,
                OrderItems = o.OrderItems.Select(oi => new ListOrderItem()
                {
                    ProductId = oi.Product.Id,
                    ProductName = oi.Product.Name,
                    Price = oi.Price,
                    Quantity = oi.Quantity,
                    OrderId = oi.OrderId
                }).ToList()
            }).ToList();
        }
    }
}

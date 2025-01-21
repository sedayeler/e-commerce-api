using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities;
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
            List<Domain.Entities.Order> orders = await _orderService.GetUserOrdersAsync();

            return orders.Select(order => new GetUserOrdersQueryResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Address = order.Address,
                Status = order.Status.ToString(),
                TotalPrice = order.TotalPrice,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Select(oi => new ListOrderItem
                {
                    ProductName = oi.Product.Name,
                    ProductId = oi.Product.Id,
                    Price = oi.Price,
                    Quantity = oi.Quantity,
                    OrderId = oi.OrderId
                }).ToList()
            }).ToList();
        }
    }
}

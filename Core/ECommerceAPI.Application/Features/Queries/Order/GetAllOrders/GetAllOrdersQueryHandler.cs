using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, List<GetAllOrdersQueryResponse>>
    {
        private readonly IOrderService _orderService;

        public GetAllOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<GetAllOrdersQueryResponse>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Order> orders = await _orderService.GetAllOrdersAsync();

            return orders.Select(o => new GetAllOrdersQueryResponse
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                Address = o.Address,
                Status = o.Status,
                TotalPrice = o.TotalPrice,
                UserId = o.UserId,
                OrderItems = o.OrderItems.Select(oi => new ListOrderItem
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

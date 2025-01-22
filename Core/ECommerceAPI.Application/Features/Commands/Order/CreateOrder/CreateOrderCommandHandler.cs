using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IOrderHubService _orderHubService;

        public CreateOrderCommandHandler(IOrderService orderService, IOrderHubService orderHubService)
        {
            _orderService = orderService;
            _orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.CreateOrderAsync(new()
            {
                Address = request.Address
            });

            await _orderHubService.OrderCreatedMessageAsync("A new order has been created.");
            return new();
        }
    }
}

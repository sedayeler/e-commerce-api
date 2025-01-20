using ECommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Order.GetUserOrders
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQueryRequest, GetUserOrdersQueryResponse>
    {
        private readonly IOrderService _orderService;

        public GetUserOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetUserOrdersQueryResponse> Handle(GetUserOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            await _orderService.GetUserOrdersAsync();

            return new();
        }
    }
}

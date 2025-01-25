using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Commands.Order.CreateOrder;
using ECommerceAPI.Application.Features.Commands.Order.UpdateOrderStatus;
using ECommerceAPI.Application.Features.Queries.Order.GetAllOrders;
using ECommerceAPI.Application.Features.Queries.Order.GetUserOrders;
using ECommerceAPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.orders, ActionType = ActionType.Reading, Definition = "Get User Orders")]
        public async Task<IActionResult> GetUserOrders([FromQuery] GetUserOrdersQueryRequest request)
        {
            List<GetUserOrdersQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("get-all")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.orders, ActionType = ActionType.Reading, Definition = "Get All Orders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest request)
        {
            List<GetAllOrdersQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.orders, ActionType = ActionType.Writing, Definition = "Create Order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            CreateOrderCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.orders, ActionType = ActionType.Updating, Definition = "Update Order Status")]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusCommandRequest request)
        {
            UpdateOrderStatusCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

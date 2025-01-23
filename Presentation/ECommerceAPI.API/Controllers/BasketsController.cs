using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Commands.Basket.AddItemToBasket;
using ECommerceAPI.Application.Features.Commands.Basket.RemoveBasketItem;
using ECommerceAPI.Application.Features.Commands.Basket.UpdateQuantity;
using ECommerceAPI.Application.Features.Queries.Basket.GetBasketItems;
using ECommerceAPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/baskets")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.baskets, ActionType = ActionType.Reading, Definition = "Get basket items")]
        public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest request)
        {
            List<GetBasketItemsQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.baskets, ActionType = ActionType.Writing, Definition = "Add item to basket")]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest request)
        {
            AddItemToBasketCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.baskets, ActionType = ActionType.Updating, Definition = "Update quantity")]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest request)
        {
            UpdateQuantityCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.baskets, ActionType = ActionType.Deleting, Definition = "Remove basket item")]
        public async Task<IActionResult> RemoveBasketItem(RemoveBasketItemCommandRequest request)
        {
            RemoveBasketItemCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Product.DeleteProduct;
using ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            List<GetAllProductQueryResponse> response = await _mediator.Send(new GetAllProductQueryRequest());
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdProduct([FromRoute] GetByIdQueryRequest request)
        {
            GetByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductCommandRequest request)
        {
            DeleteProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

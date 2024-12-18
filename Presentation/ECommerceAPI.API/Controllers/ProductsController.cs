using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Product.DeleteProduct;
using ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllProductQueryResponse response = await _mediator.Send(new GetAllProductQueryRequest());
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdQueryRequest request)
        {
            GetByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}

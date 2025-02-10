using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductsQueryRequest : IRequest<List<GetAllProductsQueryResponse>>
    {
    }
}

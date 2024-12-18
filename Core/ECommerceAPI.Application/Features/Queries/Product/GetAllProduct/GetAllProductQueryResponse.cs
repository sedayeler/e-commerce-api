using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryResponse
    {
        public List<ECommerceAPI.Domain.Entities.Product> Products { get; set; }
    }
}

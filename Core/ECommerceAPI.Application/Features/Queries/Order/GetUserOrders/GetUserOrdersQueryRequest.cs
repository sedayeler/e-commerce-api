﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Order.GetUserOrders
{
    public class GetUserOrdersQueryRequest : IRequest<List<GetUserOrdersQueryResponse>>
    {
    }
}

﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryRequest : IRequest<GetAllUsersQueryResponse>
    {
    }
}
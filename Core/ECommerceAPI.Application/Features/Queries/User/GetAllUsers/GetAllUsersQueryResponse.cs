﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public object User { get; set; }
        public int TotalUsersCount { get; set; }
    }
}

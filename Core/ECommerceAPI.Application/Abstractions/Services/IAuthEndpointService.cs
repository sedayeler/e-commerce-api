﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IAuthEndpointService
    {
        Task AssignRoleToEndpointAsync(string[] roles, string menu, string code, Type type);
        Task<List<string>> GetRolesToEndpointAsync(string menu, string code);
    }
}

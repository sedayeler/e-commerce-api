﻿using ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.User.RefreshToken
{
    public class RefreshTokenResponse
    {
        public Token Token { get; set; }
    }
}

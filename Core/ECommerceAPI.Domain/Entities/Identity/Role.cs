﻿using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Domain.Entities.Identity
{
    public class Role : IdentityRole<string>
    {
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}

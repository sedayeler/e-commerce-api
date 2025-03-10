﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories
{
    public class OrderItemWriteRepository : WriteRepository<OrderItem>, IOrderItemWriteRepository
    {
        public OrderItemWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}

﻿using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories;
using ECommerceAPI.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql(Configuration.ConfigurationString));
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<ECommerceAPIDbContext>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}

﻿using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IAppService, AppService>();
        }
    }
}

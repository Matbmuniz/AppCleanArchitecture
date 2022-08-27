using AppCleanArchitecture.Application.Interfaces;
using AppCleanArchitecture.Application.Mappings;
using AppCleanArchitecture.Application.Services;
using AppCleanArchitecture.Domain.Interface;
using AppCleanArchitecture.Infra.Data.Context;
using AppCleanArchitecture.Infra.Data.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCleanArchitecture.Infra.IoC
{
    public static class DependyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
               IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductService, ProductService>();  
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            return services;
        }
    }
}

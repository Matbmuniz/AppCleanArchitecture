using AppCleanArchitecture.Application.Interfaces;
using AppCleanArchitecture.Application.Mappings;
using AppCleanArchitecture.Application.Services;
using AppCleanArchitecture.Domain.Accounts;
using AppCleanArchitecture.Domain.Interface;
using AppCleanArchitecture.Infra.Data.Context;
using AppCleanArchitecture.Infra.Data.Identity;
using AppCleanArchitecture.Infra.Data.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
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

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => 
                        options.AccessDeniedPath = "/Account/Login");

            services.AddScoped<IAuthenticate, AuthenticateServices>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductService, ProductService>();  
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            var myhandlers = AppDomain.CurrentDomain.Load("AppCleanArchitecture.Application");
            services.AddMediatR(myhandlers);

            return services;
        }
    }
}

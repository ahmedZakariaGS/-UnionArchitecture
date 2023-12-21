using Application.Interfaces.Repositories.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.Base;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
using Application.Shared.Models;
using System.Configuration;
using Application.Interfaces.HelperService;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
             services.AddRepositories();
            services.AddServices();

            return services;
        }
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            var applicationSetting = configuration.GetSection("ApplicationSetting");
            services.Configure<ApplicationSetup>(applicationSetting);



        }
        private static void AddRepositories(this IServiceCollection services)
        {
   

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<IHelperService, HelperService>();


            services
          .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
          .AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }
        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IMediator, Mediator>();
     

        }


    }
}

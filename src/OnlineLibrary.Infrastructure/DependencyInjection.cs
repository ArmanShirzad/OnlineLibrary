using OnlineLibrary.Application.Validators.Book;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using OnlineLibrary.Application.Validators.User;
using OnlineLibrary.Application.Validators.Loan;
using OnlineLibrary.Domain.Interfaces;
using OnlineLibrary.Infrastructure.ExternalServices;
using OnlineLibrary.Infrastructure.Repositories;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace OnlineLibrary.Infrastructure
{
    public static class DependencyInjection
    {

        public static void AddInfrastructureLayerServices( this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

            services.AddSingleton(Log.Logger);

            //redis
            var redisConnectionString = configuration["Redis:ConnectionString"];
            var redisOptions = ConfigurationOptions.Parse(redisConnectionString);
            redisOptions.AbortOnConnectFail = false;

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisOptions));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "SampleInstance";
            });

            services.AddScoped<NotificationService>();
            services.AddScoped<RedisDataService>();
            services.AddScoped<RedisStreamService>();
            services.AddScoped<GeoService>();
            services.AddScoped<RedisModuleService>();


        }

        //serilog
        public static void ConfigureSerilog(HostBuilderContext context, IServiceProvider services, LoggerConfiguration configuration)
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console();
        }
    }
}

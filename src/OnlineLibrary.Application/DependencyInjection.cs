using OnlineLibrary.Application.Interfaces;
using OnlineLibrary.Application.Mappings;
using OnlineLibrary.Application.Services;
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

namespace OnlineLibrary.Application
{
    public static class DependencyInjection 
    {
        public static void AddApplicationLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILoanService, LoanService>();
            //mapster
            var config = TypeAdapterConfig.GlobalSettings;
            new MappingProfile().Register(config);
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>(); 
            services.AddValidatorsFromAssemblyContaining<CreateBookValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
            services.AddValidatorsFromAssemblyContaining<DeleteLoanValidator>();
        }

    }
}

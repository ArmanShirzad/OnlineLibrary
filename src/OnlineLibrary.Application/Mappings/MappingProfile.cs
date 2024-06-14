using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

using OnlineLibrary.Domain.DTOs;
using OnlineLibrary.Domain.Entities;
namespace OnlineLibrary.Application.Mappings
{
    public class MappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //from entitty as source to dto as destination
            config.NewConfig<Book, BookDto>()
                 .Map(dest => dest.Id, src => src.Id)
                 .Map(dest => dest.Title, src => src.Title)
                 .Map(dest => dest.Author, src => src.Author)
                 .Map(dest => dest.ISBN, src => src.ISBN)
                 .Map(dest => dest.PublishedDate, src => src.PublishedDate)
                 .Map(dest => dest.IsAvailable, src => src.IsAvailable);

            config.NewConfig<User, UserDto>()
                        .Map(dest => dest.Id, src => src.Id)
                        .Map(dest => dest.Username, src => src.Username)
                        .Map(dest => dest.Email, src => src.Email);

            TypeAdapterConfig<Loan, LoanDto>.NewConfig()
                .Map(dest => dest.Book, src => src.Book)
                .Map(dest => dest.User, src => src.User)
                .Map(dest => dest.BorrowedDate, src => src.BorrowedDate)
                .Map(dest => dest.ReturnedDate, src => src.ReturnedDate);

            config.NewConfig<Book, CreateBookDto>();
                config.NewConfig<CreateBookDto, Book>();

            TypeAdapterConfig<CreateLoanDto, Loan>.NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.UserId, src => src.UserId);


            config.NewConfig<User, CreateUserDto>();
                config.NewConfig<CreateUserDto, User>();

        }
    }

}

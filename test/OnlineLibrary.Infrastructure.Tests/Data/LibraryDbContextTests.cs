using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace OnlineLibrary.Infrastructure.Tests.Data
{
    public class LibraryDbContextTests : LibraryDbContext
    {
        public LibraryDbContextTests(DbContextOptions<LibraryDbContext> options) : base(options) { }


    }
}

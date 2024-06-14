using Microsoft.EntityFrameworkCore;

using OnlineLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;


namespace OnlineLibrary.Infrastructure.Data
{
    public class LibraryDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "DefaultConnectionString",
           b => b.MigrationsAssembly(typeof(LibraryDbContext).Assembly.FullName));
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Loan>().HasKey(l => l.Id);

            modelBuilder.Entity<Loan>().HasOne(l => l.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Loan>().HasOne(l => l.Book)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.BookId);
        }

        
    }
}

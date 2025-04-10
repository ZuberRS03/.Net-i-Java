using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    internal class ExchangeDbContext : DbContext
    {
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<Country> Countries => Set<Country>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=E:\studia\sem 6\dotNet i Java\laby\.Net-i-Java\lab 3 i 4\ConsoleApp\exchange.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Przykładowe dane testowe (opcjonalnie)
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Poland", CurrencyId = 1 }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace ExchangeMauiApp.Core
{
    internal class ExchangeDbContext : DbContext
    {
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<Country> Countries => Set<Country>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "exchange.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            // relacja: jeden Currency → wiele Countries
            modelBuilder.Entity<Currency>()
                .HasMany(c => c.Countries)
                .WithOne(c => c.Currency)
                .HasForeignKey(c => c.CurrencyId);
        }
    }
}

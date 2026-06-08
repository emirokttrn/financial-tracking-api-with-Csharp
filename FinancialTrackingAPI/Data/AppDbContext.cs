using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace FinancialTrackingAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<   AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Portfolio> Portfolios => Set<Portfolio>();
        public DbSet<Asset> Assets => Set<Asset>();
                public DbSet<Transaction> Transactions => Set<Transaction>();
    }
}
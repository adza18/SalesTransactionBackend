using Microsoft.EntityFrameworkCore;
using SalesTransaction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Infrastructure.Persistence
{
    public class ApplicationDbContext  : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
                
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<SaleTransaction> SaleTransaction { get; set; }







    }
}

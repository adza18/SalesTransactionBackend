using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesTransaction.Application.Interfaces;
using SalesTransaction.Application.Services;
using SalesTransaction.Infrastructure.Persistence;
using SalesTransaction.Infrastructure.Repositories;
using SalesTransaction.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Infrastructure.Dependencies
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly("Infrastructure")));

            services.AddScoped<IDataAccess, DataAccess>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISalesTransactionRepository, SalesTransactionRepository>();
            services.AddScoped<IInvoiceGenerator, InvoiceGenerator>();




            return services;

        }
    }
    }

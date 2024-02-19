using ETicaretApi.Persistance.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ETicaretApi.Application.Repositories.CustomerRepositories;
using ETicaretApi.Persistance.Repositories.CustomerRepositories;
using ETicaretApi.Application.Repositories.OrderRepositories;
using ETicaretApi.Persistance.Repositories.OrderRepositories;
using ETicaretApi.Application.Repositories.ProductRepositories;
using ETicaretApi.Persistance.Repositories.ProductRepositories;
using ETicaretApi.Persistance.Utils;
using ETicaretApi.Application.Repositories.FileRepositories;
using ETicaretApi.Persistance.Repositories.FileRepositories;
using ETicaretApi.Application.Repositories.ProductImageFileRepositories;
using ETicaretApi.Persistance.Repositories.ProductImageFileRepositories;
using ETicaretApi.Application.Repositories.InvoceFileRepositories;
using ETicaretApi.Persistance.Repositories.InvoceFileRepositories;

namespace ETicaretApi.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceService(this IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>(option => option.UseNpgsql(GetConnectionString.GetConnection()));
            services.AddScoped<DbContext, ApiDbContext>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddScoped<IInvoceFileReadRepository, InvoceFileReadRepository>();
            services.AddScoped<IInvoceFileWriteRepository, InvoceFileWriteRepository>();

        }
    }
}


using ETicaretApi.Application.Abstractions.Storage;
using ETicaretApi.Infrastructure.Services;
using ETicaretApi.Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection services)
            where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}

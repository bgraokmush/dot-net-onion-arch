using ETicaretApi.Application.Repositories.OrderRepositories;
using ETicaretApi.Application.Repositories.ProductRepositories;
using ETicaretApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Persistance.Repositories.ProductRepositories
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}

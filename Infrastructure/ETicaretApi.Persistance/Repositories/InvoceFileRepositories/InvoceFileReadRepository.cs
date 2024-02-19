using ETicaretApi.Application.Repositories.InvoceFileRepositories;
using ETicaretApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Persistance.Repositories.InvoceFileRepositories
{
    public class InvoceFileReadRepository : ReadRepository<InvoceFile>, IInvoceFileReadRepository
    {
        public InvoceFileReadRepository(DbContext dbContext, bool tracking = true) : base(dbContext, tracking)
        {
        }
    }
}

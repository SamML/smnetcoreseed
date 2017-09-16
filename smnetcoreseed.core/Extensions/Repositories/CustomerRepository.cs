using Microsoft.EntityFrameworkCore;
using smnetcoreseed.core.Data.Repositories;
using smnetcoreseed.core.Interfaces.Repositories;
using smnetcoreseed.core.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace smnetcoreseed.core.Extensions.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CoreRepositoriesDbContext context) : base(context)
        { }

        public IEnumerable<Customer> GetTopActiveCustomers(int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomersData()
        {
            return appContext.Customers
                .Include(c => c.Orders).ThenInclude(o => o.OrderDetails).ThenInclude(d => d.Product)
                .Include(c => c.Orders).ThenInclude(o => o.Cashier)
                .OrderBy(c => c.Name)
                .ToList();
        }

        private CoreRepositoriesDbContext appContext
        {
            get { return (CoreRepositoriesDbContext)_context; }
        }
    }
}
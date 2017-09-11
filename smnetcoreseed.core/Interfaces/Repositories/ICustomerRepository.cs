using smnetcoreseed.core.Models.Repositories;
using System.Collections.Generic;

namespace smnetcoreseed.core.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetTopActiveCustomers(int count);

        IEnumerable<Customer> GetAllCustomersData();
    }
}
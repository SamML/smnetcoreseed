using smnetcoreseed.core.Data.Repositories;
using smnetcoreseed.core.Interfaces.Repositories;

namespace smnetcoreseed.core.Extensions.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreRepositoriesDbContext _context;

        private ICustomerRepository _customers;
        private IProductRepository _products;
        private IOrdersRepository _orders;

        public UnitOfWork(CoreRepositoriesDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new CustomerRepository(_context);

                return _customers;
            }
        }

        public IProductRepository Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductRepository(_context);

                return _products;
            }
        }

        public IOrdersRepository Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new OrdersRepository(_context);

                return _orders;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using smnetcoreseed.core.Data.Repositories;
using smnetcoreseed.core.Models.Repositories;
using smnetcoreseed.core.Interfaces.Repositories;

namespace smnetcoreseed.core.Extensions.Repositories
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(DbContext context) : base(context)
        { }

        private CoreRepositoriesDbContext appContext
        {
            get { return (CoreRepositoriesDbContext)_context; }
        }
    }
}
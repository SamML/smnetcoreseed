using Microsoft.EntityFrameworkCore;
using smnetcoreseed.core.Data.Repositories;
using smnetcoreseed.core.Interfaces.Repositories;
using smnetcoreseed.core.Models.Repositories;

namespace smnetcoreseed.core.Extensions.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        { }

        private CoreRepositoriesDbContext appContext
        {
            get { return (CoreRepositoriesDbContext)_context; }
        }
    }
}
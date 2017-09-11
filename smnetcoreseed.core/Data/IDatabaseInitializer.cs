using System.Threading.Tasks;

namespace smnetcoreseed.core.Data
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}
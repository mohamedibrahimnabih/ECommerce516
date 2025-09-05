using ECommerce516.DataAccess;
using System.Threading.Tasks;

namespace ECommerce516.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context = new();

        public async Task CreateRangeAsync(List<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
        }
    }
}

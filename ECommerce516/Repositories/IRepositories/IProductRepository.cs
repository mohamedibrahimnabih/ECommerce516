using Microsoft.EntityFrameworkCore;

namespace ECommerce516.Repositories.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task CreateRangeAsync(List<Product> products);
    }
}

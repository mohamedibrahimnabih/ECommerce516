using ECommerce516.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce516.Repositories
{
    public class BrandRepository
    {

        private ApplicationDbContext _context = new();

        // CRUD

        public async Task CreateAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
        }

        public void Update(Brand brand)
        {
            _context.Brands.Update(brand);
        }

        public void Delete(Brand brand)
        {
            _context.Brands.Remove(brand);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Brand>> GetAsync(Expression<Func<Brand, bool>>? expression = null)
        {
            var brands = _context.Brands.AsQueryable();

            if(expression is not null)
            {
                brands = brands.Where(expression);
            }

            return await brands.ToListAsync();
        }

        public async Task<Brand?> GetOneAsync(Expression<Func<Brand, bool>>? expression = null)
        {
            return (await GetAsync(expression)).FirstOrDefault();
        }

    }
}

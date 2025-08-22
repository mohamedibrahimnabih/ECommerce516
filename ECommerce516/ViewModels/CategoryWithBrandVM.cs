namespace ECommerce516.ViewModels
{
    public class CategoryWithBrandVM
    {
        public List<Category> Categories { get; set; } = null!;
        public List<Brand> Brands { get; set; } = null!;
        public Product? Product { get; set; }
    }
}

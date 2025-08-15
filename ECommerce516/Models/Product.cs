namespace ECommerce516.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string MainImg { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int Traffic { get; set; }

        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public List<ProductSubImage> ProductSubImage { get; set; }
    }
}

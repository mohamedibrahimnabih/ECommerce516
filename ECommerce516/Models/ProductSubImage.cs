namespace ECommerce516.Models
{
    public class ProductSubImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string Img { get; set; }
        public Product Product { get; set; }
    }
}

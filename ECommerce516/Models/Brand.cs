using System.ComponentModel.DataAnnotations;

namespace ECommerce516.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public List<Product> Products { get; set; }
    }
}

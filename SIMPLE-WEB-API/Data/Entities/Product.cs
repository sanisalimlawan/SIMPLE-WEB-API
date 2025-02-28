using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMPLE_WEB_API.Data.Entities
{
    public class Product 
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Sale {  get; set; }
        public int Stock { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
       

    }
}

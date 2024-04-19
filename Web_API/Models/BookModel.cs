using System.ComponentModel.DataAnnotations;

namespace Web_API.Models
{
    public class BookModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}

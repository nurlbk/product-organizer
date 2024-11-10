using System.ComponentModel.DataAnnotations;

namespace proj1.DB.Models {
    public class GroupedProduct {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? GroupName { get; set; }
        [Required]
        public int? ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Product? Product { get; set; }

    }
}

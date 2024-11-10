using System.ComponentModel.DataAnnotations;


namespace proj1.DB.Models {
    public class Product {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Measurment { get; set; }
        [Required]
        public double? UnitPrice { get; set; }
        [Required]
        public int? Quantity { get; set; }

    }
}

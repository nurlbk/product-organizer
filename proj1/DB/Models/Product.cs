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
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string? Status { get; set; }

    }

    public static class ProductStatus {
        public const string Active = "Active";
        public const string Inactive = "Inactive";

    }
}

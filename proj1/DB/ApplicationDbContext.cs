using Microsoft.EntityFrameworkCore;
using proj1.DB.Models;

namespace proj1.DB {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<GroupedProduct> GroupedProducts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<GroupedProduct>()
            //    .HasOne(gp => gp.Product)
            //    .WithMany()
            //    .HasForeignKey(gp => gp.ProductId)
            //    .OnDelete(DeleteBehavior.Cascade);



        }
    }
}

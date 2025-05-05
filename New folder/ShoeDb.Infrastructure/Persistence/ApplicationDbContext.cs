using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistence
{
   public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
      {
      }

      // DbSets for your entities
      public DbSet<ApplicationUser> Users { get; set; }
      public DbSet<Product> Products { get; set; }
      public DbSet<Order> Orders { get; set; }
      public DbSet<ProductImage> ProductImages { get; set; }
      public DbSet<Review> Reviews { get; set; }
      public DbSet<OrderItem> OrderItems { get; set; }  // Fixed typo here
      public DbSet<ProductCategory> ProductCategories { get; set; }  // Fixed naming convention
      public DbSet<Category> Categories { get; set; }  // Fixed naming convention

      // Overriding SaveChangesAsync to handle transactions
      public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
      {
         // Use a transaction to ensure atomicity
         using var transaction = await Database.BeginTransactionAsync(cancellationToken);
         try
         {
            // Call base method to save changes to the database
            var result = await base.SaveChangesAsync(cancellationToken);

            // Commit transaction after successful save
            await transaction.CommitAsync(cancellationToken);

            return result;
         }
         catch (Exception ex)
         {
            // Rollback transaction if there is an exception
            await transaction.RollbackAsync(cancellationToken);
            // Log the exception here if needed
            throw new InvalidOperationException("An error occurred while saving changes", ex);
         }
      }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         // Example: Configure composite key for ProductCategory
         modelBuilder.Entity<ProductCategory>()
             .HasKey(pc => new { pc.ProductId, pc.CategoryId });

         // Additional custom configurations if needed
      }
   }
}

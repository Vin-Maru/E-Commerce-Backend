using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Domain.Entities
{
   using System;
   using System.Collections.Generic;

   public class Product
   {
      public Guid Id { get; set; } // Unique identifier
      public string Name { get; set; } // Product name
      public string Description { get; set; } // Product description
      public decimal Price { get; set; } // Price of the product
      public decimal? DiscountPrice { get; set; } // Optional discounted price
      public string Category { get; set; } // Product category
      public string SKU { get; set; } // Stock Keeping Unit (unique identifier for inventory)
      public int StockQuantity { get; set; } // Number of items in stock
      public bool IsActive { get; set; } // Indicates if the product is active and available for sale
      public DateTime CreatedAt { get; set; } // Date when the product was created
      public DateTime UpdatedAt { get; set; } // Date when the product was last updated
      public string MainImageUrl { get; set; } // URL for the main product image
      public List<string> ImageUrls { get; set; } // URLs for additional product images
      public List<string> SizeOptions { get; set; } // Available size options
      public List<string> ColorOptions { get; set; } // Available color options

      public ICollection<ProductImage> ProductImages { get; set; }
      public ICollection<Review> Reviews { get; set; }
      public ICollection<OrderItem> OrderItems { get; set; }
      public ICollection<ProductCategory> ProductCategories { get; set; }

   }

}

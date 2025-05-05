using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Domain.Entities
{
   public class ProductImage
   {
      public Guid Id { get; set; }
      public Guid ProductId { get; set; }
      public string ImageUrl { get; set; }
      public bool IsMainImage { get; set; }

      // Navigation property (if using Entity Framework)
      public Product Product { get; set; }
   }

}

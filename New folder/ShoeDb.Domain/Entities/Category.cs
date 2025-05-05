using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Domain.Entities
{
   public class Category
   {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public Guid? ParentCategoryId { get; set; } // For hierarchical categories

      public ICollection<ProductCategory> ProductCategories { get; set; }  // Navigation property
   }

}

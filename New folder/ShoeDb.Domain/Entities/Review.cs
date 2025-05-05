using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ShoeStore.Domain.Entities
{
   public class Review
   {
      public Guid Id { get; set; }
      public Guid ProductId { get; set; }
      public Guid UserId { get; set; }
      public int Rating { get; set; }
      public string Comment { get; set; }
      public DateTime CreatedAt { get; set; }

      // Navigation properties (if using Entity Framework)
      public Product Product { get; set; }
      public ApplicationUser User { get; set; }
   }

}

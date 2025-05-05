using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ShoeStore.Domain.Entities
{
   public class Order
   {
      public Guid Id { get; set; }
      public Guid UserId { get; set; }
      public decimal TotalAmount { get; set; }
      public string Status { get; set; }
      public string ShippingAddress { get; set; }
      public DateTime CreatedAt { get; set; }
      public DateTime UpdatedAt { get; set; }

      // Navigation property (if using Entity Framework)
      public ApplicationUser User { get; set; }
      public ICollection<OrderItem> OrderItems { get; set; }
   }

}

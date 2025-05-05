using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Domain.Entities
{
   public class OrderItem
   {
      public Guid Id { get; set; }
      public Guid OrderId { get; set; }
      public Guid ProductId { get; set; }
      public int Quantity { get; set; }
      public decimal Price { get; set; }

      // Navigation properties (if using Entity Framework)
      public Order Order { get; set; }
      public Product Product { get; set; }
   }

}

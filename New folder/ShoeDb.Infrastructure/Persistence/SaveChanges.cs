using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeStore.Application.Implimentation.Interfaces;

namespace ShoeStore.Infrastructure.Persistence
{
   public class SaveChanges : ISaveChanges
   {
      private readonly ApplicationDbContext _context;
   public SaveChanges(ApplicationDbContext context) 
   {
      _context = context;
   }
      public async Task<bool> SaveAsync()
      {
      return await _context.SaveChangesAsync() > 0; 
      }
   }
}

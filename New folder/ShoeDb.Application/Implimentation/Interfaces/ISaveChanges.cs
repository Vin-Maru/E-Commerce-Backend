using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeStore.Application.Abstractions.Navision.IRepository;

namespace ShoeStore.Application.Implimentation.Interfaces
{
   public interface ISaveChanges
   {
      Task<bool> SaveAsync();
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Application.Abstractions.Interfaces;
using ShoeStore.Application.Abstractions.Navision.IRepository;
using ShoeStore.Application.Implimentation.Interfaces;
namespace ShoeDb.Application.Implimentation.Interfaces
{
   public class UnitOfWork : IUnitOfWork
   {
      private readonly ISaveChanges _save;
      public ICategoryRepository CategoryRepository { get; }

      public UnitOfWork( ICategoryRepository categoryRepository, ISaveChanges save)
      {
         CategoryRepository = categoryRepository;
         _save = save;

      }

      public Task<bool> CommitAsync() => _save.SaveAsync();
   }
}

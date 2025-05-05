using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Application.Abstractions.Navision.IRepository;
using ShoeStore.Application.Dtos;
using ShoeStore.Application.Dtos.Common;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Persistence;

namespace ShoeStore.Infrastructure.Implimentation.Repository
{
   public class CategoryRepository : ICategoryRepository
   {
      private readonly ApplicationDbContext _applicationDbContext; 
      public CategoryRepository(ApplicationDbContext applicationDbContext)
      {
         _applicationDbContext = applicationDbContext;
      }
      public async Task<bool> AddAsync(Category request)
      {
       await _applicationDbContext.Categories.AddAsync(request);
         return true;
      }

      public Task DeleteAsync(Domain.Entities.Category category)
      {
         throw new NotImplementedException();
      }

      public async Task<Category?> ExistsByNameAsync(string name)
      {
       return await _applicationDbContext.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
      }

      public async Task<List<Category>> GetAllAsync()
      {
       return await _applicationDbContext.Categories.ToListAsync();
      }

      public Task<Domain.Entities.Category?> GetByIdAsync(int id)
      {
         throw new NotImplementedException();
      }
   }
}

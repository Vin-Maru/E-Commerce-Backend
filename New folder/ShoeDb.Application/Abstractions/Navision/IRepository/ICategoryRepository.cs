using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeStore.Application.Dtos;
using ShoeStore.Application.Dtos.Common;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Application.Abstractions.Navision.IRepository
{
    public interface ICategoryRepository
    {
      Task <List<Category>> GetAllAsync();
      Task<bool> AddAsync(Category request);
      Task<Category?> GetByIdAsync(int id);
      Task DeleteAsync(Category category);
      Task<Category> ExistsByNameAsync(string name);
   }
}

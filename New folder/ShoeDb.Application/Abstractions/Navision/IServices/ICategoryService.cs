using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeStore.Application.Dtos;
using ShoeStore.Application.Dtos.Common;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Application.Abstractions.Navision.IServices
{
   public interface ICategoryService
   {
      Task<List<Category>> GetAllCategoriesAsync();
      Task<Category?> GetCategoryByIdAsync(int id);
      Task <bool> GetCategoryByNameAsync(string name);
      Task<AuthResult> AddCategoryAsync(CategoryDto request);
      Task<bool> UpdateCategoryAsync(int id, CategoryDto request);
      Task<bool> DeleteCategoryAsync(int id);
   }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeStore.Application.Abstractions.Interfaces;
using ShoeStore.Application.Abstractions.Navision.IServices;
using ShoeStore.Application.Dtos;
using ShoeStore.Application.Dtos.Common;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Application.Implimentation.Services
{
  public class CategoryService : ICategoryService
   {
      private readonly IUnitOfWork _unitOfWork;
    public CategoryService(IUnitOfWork unitOfWork) 
    {
         _unitOfWork = unitOfWork;
    }

      public async Task<AuthResult> AddCategoryAsync(CategoryDto request)
      {
         var exists = await _unitOfWork.CategoryRepository.ExistsByNameAsync(request.Name);
         if(exists != null)
         {
           return AuthResult.Fail("Category already exist");
         }
         var category = new Category
         {
            Name = request.Name,
            Description = request.Description,
         };
         await _unitOfWork.CategoryRepository.AddAsync(category);
         await _unitOfWork.CommitAsync();
         return AuthResult.Success(null, "Category added successfully");
      }


      public Task<bool> DeleteCategoryAsync(int id)
      {
         throw new NotImplementedException();
      }
      
      public async Task<List<Category>> GetAllCategoriesAsync()
      {
         var response = await _unitOfWork.CategoryRepository.GetAllAsync();
         return response;
      }

      public Task<Category?> GetCategoryByIdAsync(int id)
      {
         throw new NotImplementedException();
      }

      public Task<bool> GetCategoryByNameAsync(string name)
      {
         throw new NotImplementedException();
      }

      public Task<bool> UpdateCategoryAsync(int id, CategoryDto request)
      {
         throw new NotImplementedException();
      }

   }
}

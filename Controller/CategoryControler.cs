using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Abstractions.Interfaces;
using ShoeStore.Application.Dtos;
using ShoeStore.Application.Dtos.Common;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Controller
{
   [ApiController]
   [Route("api/v{version:apiVersion}/[controller]")]
   public class CategoryController : ControllerBase
   {
      private readonly IServiceManager _serviceManager;

      public CategoryController(IServiceManager serviceManager)
      {
         _serviceManager = serviceManager;
      }

      [HttpPost("addcategory")]
      public async Task<ActionResult<AuthResult>> AddCategory([FromBody] CategoryDto request)
      {
         if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");
         try
         {
            var result = await _serviceManager.CategoryService.AddCategoryAsync(request);
            if (result == null)
            {
               return Problem(
                      detail: result.Message,
                      statusCode: StatusCodes.Status500InternalServerError,
                      title: $"Process Failed. | {ControllerContext.ActionDescriptor.ActionName}",
                      instance: HttpContext.Request.Path
                  );
            }
            return Ok(result);
         }
         catch (Exception ex)
         {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An unexpected error occurred.",
                instance: HttpContext.Request.Path
            );
         }
      }
      [HttpGet("getcategories")]
      public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
      {

         var categories = await _serviceManager.CategoryService.GetAllCategoriesAsync();
         return Ok(categories);
      }
   }
}

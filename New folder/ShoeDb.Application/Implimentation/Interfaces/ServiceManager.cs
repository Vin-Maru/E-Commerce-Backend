using ShoeStore.Application.Abstractions.Interfaces;
using ShoeStore.Application.Abstractions.Navision.IServices;

namespace ShoeDb.Application.Implimentation.Interfaces
{
    public class ServiceManager : IServiceManager
    {
      public ServiceManager(
      IAccountService accountService,
      ICategoryService categoryService
      )
      {
         AccountService = accountService;
         CategoryService = categoryService;
      }


      public IAccountService? AccountService { get; }
      public ICategoryService CategoryService { get; }
    }
}

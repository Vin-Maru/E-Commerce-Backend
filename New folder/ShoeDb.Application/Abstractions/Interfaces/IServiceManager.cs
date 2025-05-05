using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeStore.Application.Abstractions.Navision.IServices;
namespace ShoeStore.Application.Abstractions.Interfaces
{
    public interface IServiceManager
    {
        IAccountService AccountService { get; }
        ICategoryService CategoryService { get; }
    }
}

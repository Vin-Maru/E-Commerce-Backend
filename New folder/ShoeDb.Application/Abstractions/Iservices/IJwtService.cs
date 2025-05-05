using ShoeStore.Application.Dtos.Common;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Application.Abstractions.Iservices;
public interface IJwtService
{
    Task<Response<string>> GenerateToken(ApplicationUser user);
}

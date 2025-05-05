using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Application.Abstractions.Navision.IServices;
using ShoeStore.Domain.Entities;
using ShoeStore.Application.Abstractions.Iservices;
using ShoeStore.Application.Dtos.Account;
using ShoeStore.Application.Dtos.Common;
namespace ShoeStore.Application.Implimentation.Services
{
  
    public class AccountService : IAccountService
    {
        private readonly ISmsSender _smsSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        public AccountService(IJwtService jwtService,UserManager<ApplicationUser> userManager, ISmsSender smsSender)
        {
            _smsSender = smsSender;
            _userManager = userManager;
            _jwtService = jwtService;
        }
        public Task<AuthResult> ForgotPasswordAsync(string PhoneNumber)
        {
            throw new NotImplementedException();
        }
        public async Task<AuthResult> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == loginRequest.PhoneNumber);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                return AuthResult.Fail("Invalid phone number or password");
            var token = await _jwtService.GenerateToken(user);

            return AuthResult.Success(token.Data,"Login Successful");
        }

      public async Task<AuthResult> RegisterAsync(RegisterRequest registerRequest)
      {
         var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == registerRequest.PhoneNumber);
         if(existingUser != null)
         {
            return AuthResult.Fail("User already exist");
         }
         try
         {
            var user = new ApplicationUser
            {
               FirstName = registerRequest.FirstName,
               SecondName = registerRequest.SecondName,
               UserName = registerRequest.PhoneNumber,
               PhoneNumber = registerRequest.PhoneNumber,
               Email = registerRequest.Email
            };
       
            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded)
            {
               return AuthResult.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return AuthResult.Success("","User Registered Successfully");
         }
         catch (Exception ex)
         {
            return AuthResult.Fail($"Unhandled Exception: {ex.Message}");
         }
      }

      public Task<AuthResult> ResetPasswordAsync(ResetPasswordRequest resetpassword)
        {
            throw new NotImplementedException();
        }
    }
}

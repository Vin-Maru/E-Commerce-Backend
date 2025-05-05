using System;
using System.Collections.Generic;
using ShoeStore.Application.Dtos.Account;
using ShoeStore.Application.Dtos.Common;


namespace ShoeStore.Application.Abstractions.Navision.IServices
{
   public interface IAccountService
    {
        Task <AuthResult> LoginAsync(LoginRequest loginRequest);
        Task <AuthResult> RegisterAsync(RegisterRequest registerRequest);
        Task<AuthResult> ForgotPasswordAsync( string PhoneNumber);
        Task<AuthResult> ResetPasswordAsync(ResetPasswordRequest resetpassword);

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Abstractions.Interfaces;
using ShoeStore.Application.Dtos.Account;


namespace ShoeDb.Controller
{
   [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class AccountController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public AccountController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
        try{
            var result = await _serviceManager.AccountService.LoginAsync(request);
            if(!result.Successful){
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
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
        try{ 
            var result = await _serviceManager.AccountService.RegisterAsync(request);
         if (!result.Successful)
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

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
        try{
            var result = await _serviceManager.AccountService.ForgotPasswordAsync(request.PhoneNumber);
            if (!result.Successful)
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
      [HttpPost("reset-password")]
      public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
      {
         try
         {
            var result = await _serviceManager.AccountService.ResetPasswordAsync(request);
            if (!result.Successful)
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
    }
}

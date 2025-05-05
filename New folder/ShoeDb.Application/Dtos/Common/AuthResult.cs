using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Dtos.Common
{
    public class AuthResult
    {
        public bool Successful { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }

        public static AuthResult Success(string token, string Message) => new() { Successful = true, Token = token,Message = Message };
        public static AuthResult Fail(string message) => new() { Successful = false, Message = message };
    }
}

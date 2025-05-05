using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Dtos.Account
{
    public class RegisterRequest
    {
        public string FirstName { get; set; } = string.Empty;
       public string SecondName { get; set; } = string.Empty;
       public string PhoneNumber { get; set; } = string.Empty;
       public string Password { get; set; } = string.Empty;
      public string Email { get; set; }

   }
}

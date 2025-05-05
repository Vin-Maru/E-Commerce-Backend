using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Dtos.Account
{
    public class ForgotPasswordRequest
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}

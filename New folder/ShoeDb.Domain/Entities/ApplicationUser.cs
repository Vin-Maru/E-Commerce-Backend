using Microsoft.AspNetCore.Identity;

namespace ShoeStore.Domain.Entities
{
   public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public   string SecondName { get; set; }
        
    }
}

using Microsoft.AspNetCore.Identity;

namespace Web_API.Data
{
    public class Application_User : IdentityUser
    {
        public String FirstName { get; set; } = null!;
        public String LastName { get; set; } = null!;
    }
}

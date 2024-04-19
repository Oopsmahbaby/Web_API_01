using Microsoft.AspNetCore.Identity;
using Web_API.Models;

namespace Web_API.Repositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(Sign_Up_Model model);
        public Task<String> SignInAsync(Sign_In_Model model);
    }
}

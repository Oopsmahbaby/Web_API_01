using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_API.Data;
using Web_API.Helpers;
using Web_API.Models;

namespace Web_API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<Application_User> userManager;
        private readonly SignInManager<Application_User> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountRepository(UserManager<Application_User> userManager, 
                                SignInManager<Application_User> signInManager,
                                IConfiguration configuration, RoleManager<IdentityRole> roleManager) 
        {
            this.userManager = userManager;
            this.signInManager= signInManager;
            this.configuration= configuration;
            this.roleManager = roleManager;
        }
        public async Task<string> SignInAsync(Sign_In_Model model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (user == null || !passwordValid)
            {
                return string.Empty;
            }
            //var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            //if (!result.Succeeded) { return string.Empty; }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Lay user Role -> string
            var userRole = await userManager.GetRolesAsync(user);
            foreach (var role in userRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken (
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(20),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }

        public async Task<IdentityResult> SignUpAsync(Sign_Up_Model model)
        {
            var user = new Application_User {
                FirstName=model.FirstName,
                LastName=model.LastName,
                Email=model.Email,
                UserName=model.Email
            
            };
            var result = await userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                if(!await roleManager.RoleExistsAsync(AppRole.Customer))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Customer));
                }
                await userManager.AddToRoleAsync(user, AppRole.Customer);
            }
            return result;
        }
    }
}

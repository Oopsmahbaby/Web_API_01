using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_API.Data;
using Web_API.Models;

namespace Web_API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<Application_User> userManager;
        private readonly SignInManager<Application_User> signInManager;
        private readonly IConfiguration configuration;

        public AccountRepository(UserManager<Application_User> userManager, 
                                SignInManager<Application_User> signInManager,
                                IConfiguration configuration) 
        {
            this.userManager = userManager;
            this.signInManager= signInManager;
            this.configuration= configuration;
        }
        public async Task<string> SignInAsync(Sign_In_Model model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded) { return string.Empty; }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
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
            return await userManager.CreateAsync(user,model.Password);

        }
    }
}

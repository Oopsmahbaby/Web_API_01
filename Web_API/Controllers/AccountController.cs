using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API.Models;
using Web_API.Repositories;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository acountRepo;

        public AccountController(IAccountRepository repo)
        {
            acountRepo = repo;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> signUp(Sign_Up_Model signUpModel)
        {
            var result= await acountRepo.SignUpAsync(signUpModel);
            if(result.Succeeded)
            {
                return Ok(result.Succeeded);
            }else
            return Unauthorized();
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> signIn(Sign_In_Model signInModel)
        {
            var result= await acountRepo.SignInAsync(signInModel);
            if(string.IsNullOrEmpty(result)) return Unauthorized();
            return Ok(result);
        }
    }
}

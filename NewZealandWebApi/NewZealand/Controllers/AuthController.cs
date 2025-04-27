using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewZealand.Interface;
using NewZealand.Models.DTO;

namespace NewZealand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository){
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
       //post
       [HttpPost]
       [Route("Register")]
       public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO){
        var identityUser = new IdentityUser{
            UserName = registerRequestDTO.Username,
            Email = registerRequestDTO.Username
        };
        var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);
        if(identityResult.Succeeded){
            //Add Roles to the user
            if(registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any()){
              identityResult =   await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                if(identityResult.Succeeded){
                    return Ok("User Register successfully!");
                }
            }
        }
        return BadRequest("Something went wrong");
       }

       //login
       [HttpPost]
       [Route("login")]
       public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO){
        var user = await userManager.FindByEmailAsync(loginRequestDTO.UserName);
        if(user != null){
           var checkpassword =  await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
           if(checkpassword){

            //get the roles for this user
            var roles = await userManager.GetRolesAsync(user);
            if(roles != null){
                //Create Token
                var jwtToken =  tokenRepository.CreateJwtToken(user, roles.ToList());
                //for direct given token to user we use loginresponcedto for security purpose
                var response = new LoginResponseDTO{
                    JwtToken = jwtToken
                };
                return Ok(response);
            }
           }
        }
        return BadRequest("UserName Password incorrect");
       }
    }
}
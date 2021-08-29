using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerAPI.Controllers
{
    [Route("api/[controller]/[action]"), ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfigurationSection _jwtSettings;

        public AccountsController(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtSettings = configuration.GetSection("JwtSettings");
        }

        [Authorize(Roles = "Standart"), HttpGet]
        public String Test2()
        {
            return "accounts controller";
        }

        public String Test()
        {
            return "hello";
        }

        [HttpPost, Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Register(UserRegistrationModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                return Ok(result.Errors);
            }
            await _userManager.AddToRoleAsync(user, "Standart");

            return StatusCode(201);
        }

        /// only dev
        //[HttpGet]
        //public async Task<IActionResult> Seed()
        //{
        //    var users = new List<User>();
        //    var userCount = 0;
        //    var adminUser = new User
        //    {
        //        FirstName = "Bob",
        //        LastName = "Bobson",
        //        Email = "Bob@bob.com",
        //        UserName = "Bob@bob.com"
        //    };
        //    var result = await _userManager.CreateAsync(adminUser, "egegG123@");
        //    if (result.Succeeded)
        //    {
        //        userCount++;
        //        users.Add(adminUser);
        //    } 
        //    else
        //    {
        //        return new JsonResult( new
        //        {
        //            User = adminUser,
        //            Err = result.Errors
        //        });
        //    }

        //    var standardUser = new User
        //    {
        //        FirstName = "Don",
        //        LastName = "DOnson",
        //        Email = "Don@gffg.com",
        //        UserName = "Don@gffg.com",
        //    };
        //    result = await _userManager.CreateAsync(standardUser, "eedS43!@");
        //    if (result.Succeeded)
        //    {
        //        userCount++;
        //        users.Add(standardUser);
        //    }
        //    else
        //    {
        //        return new JsonResult(new
        //        {
        //            User = standardUser,
        //            Err = result.Errors
        //        });
        //    }

        //    return new JsonResult(new
        //    {
        //        Count = userCount,
        //        Users = users
        //    });
        //}

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, userModel.Password))
            {
                var signingCredentials = GetSigningCredentials();
                var claims = GetClaims(user);
                var tokenOptions = GenerateTokenOptions(signingCredentials, await claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(token);
            }
            return Unauthorized("Invalid Authentication");
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);
            return tokenOptions;
        }
    }
}

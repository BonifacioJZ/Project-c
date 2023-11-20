using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Interfaces;
using App.Res;
using Domain.Dto.User;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace App.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string? secretKey;
        private readonly UserManager<User> _userManager;
        public AuthenticationService(IConfiguration configuration, UserManager<User> userManager){
            this.secretKey = configuration.GetSection("settings").GetSection("secretKey").ToString();
            _userManager = userManager;
        }
        public Task<Response<string>> Login(UserLogin userLogin)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> Register(UserInDto register)
        {
            var userExist = await _userManager.FindByEmailAsync(register.Email);
            if (userExist != null) return new Response<string>(){
                Success= false,
                message = "User already exist",
                Data = null
            };
            User user = new User(){
                UserName = register.Password,
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded) return new Response<string>(){
                Success= false,
                message = "Error to save user",
                Data = null
            };
            var token  = "hello";
            return new Response<string>(){
                Success= true,
                message = "User saved",
                Data = token
            }; 
            
        }
    }
}
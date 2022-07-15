using EmployeeManagerment.Entity;
using EmployeeManagerment.Models;
using EmployeeManagerment.Respository.AppUserRepository;
using Exercise2.EF;
using Exercise2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagerment.Services
{
    public class UserService : IUserService
    {
        public readonly UserManager<AppUser> UserManager;
        public readonly SignInManager<AppUser> SignInManager;
        public readonly IConfiguration Configuration;
        private readonly IAppUserRepository _appUserRepository;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
            , IConfiguration configuration, IAppUserRepository appUserRepository)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            Configuration = configuration;
            _appUserRepository = appUserRepository;
        }

        public async Task<APIResult<List<UserModel>>> GetAll()
        {
            var user = await _appUserRepository.GetModelAll();
            return new APIResult<List<UserModel>>() { Success = true, Message ="Successful", ResultObject = user};
        }

        public async Task<APIResult<UserModel>> Login(LoginRequest request)
        {
            var user = await _appUserRepository.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new APIResult<UserModel>() { Success = false, Message = "Can't not find Username!" };
            }
            var result = await _appUserRepository.SignInAsync(user, request.Password,
                request.RememberMe == null ? false : (bool)request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new APIResult<UserModel>() { Success = false, Message = "Login failed!" }; ;
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(Configuration["Token:Issuer"],
                Configuration["Token:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new APIResult<UserModel>()
            {
                Success = true,
                Message = "Login successful!",
                ResultObject = new UserModel()
                {
                    UserName = request.UserName,
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                }
            };

        }

        public async Task<APIResult<string>> Register(RegisterRequest request)
        {
            var checkUser = await _appUserRepository.FindByNameAsync(request.UserName);
            if(checkUser != null)
                return new APIResult<string>() { Success = false, Message = "Register failed!", ResultObject = $"Already has username '{request.UserName}'" };
            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email
            };
            var result = await _appUserRepository.CreateAppUser(user,request.Password);
            if (!result.Succeeded)
                return new APIResult<string>() { Success = false, Message = "Register failed!", ResultObject = result.ToString() };
            return new APIResult<string>() { Success = true, Message = "Register successful!", ResultObject = "Register successful!" };
        }
    }
}

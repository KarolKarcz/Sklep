using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Extensions;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AccountController(DataContext dataContext, ITokenService tokenService, IEmailService emailService)
        {
            _dataContext = dataContext;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName))
                return BadRequest("Username already exists");

            if (await EmailExists(registerDto.EmailAdress))
                return BadRequest("Email is already in use");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                EmailAdress = registerDto.EmailAdress.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                PersonalData = new AppUserPersonalData()
                {
                    Adress = new(),
                    FirstName = "",
                    LastName = "",
                    Newsletter = registerDto.Newsletter
                }
                
            };

            EmailConfirmation emailConfirmationEntity = new EmailConfirmation
            {
                Confirmed = false,
                ConfirmationString = RandomString(),
                User = user
            };

            _dataContext.Add(user);
            _dataContext.Add(emailConfirmationEntity);
            await _dataContext.SaveChangesAsync();

            await _emailService.SendPasswordConfirmationEmailAsync(user.EmailAdress, emailConfirmationEntity.ConfirmationString);

            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _dataContext.AppUsers.FirstOrDefaultAsync(user => user.UserName == loginDto.UserName.ToLower());

            string unauthorizedMessage = "Incorrect username or password";

            if (user == null)
                return Unauthorized(unauthorizedMessage);

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized(unauthorizedMessage);

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _dataContext.AppUsers.AnyAsync(user => user.UserName == username.ToLower());
        }

        private async Task<bool> EmailExists(string email)
        {
            return await _dataContext.AppUsers.AnyAsync(user => user.EmailAdress == email.ToLower());
        }

        private static string RandomString()
        {
            Random random = new Random();

            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, 96)
                .Select(x => pool[random.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }
    }
}

using API.Controllers.Helpers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountPasswordResetController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IEmailService _emailService;

        public AccountPasswordResetController(DataContext dataContext, IEmailService emailService)
        {
            _dataContext = dataContext;
            _emailService = emailService;
        }

        [HttpGet("ValidateToken/{resetToken}")]
        public async Task<ActionResult<bool>> ResetPasswordTokenCheck(string resetToken)
        {
            if (resetToken.Length != 96)
                return BadRequest("Link has invalid");

            PasswordReset passwordReset = await _dataContext.PasswordReset.FirstOrDefaultAsync(x => x.ResetToken == resetToken);

            if (passwordReset == null)
                return BadRequest("Link is invalid");

            if (passwordReset.RequestGenerationData.AddMinutes(30) <= DateTime.Now)
                return BadRequest("Link has expired");

            if(!passwordReset.Active)
                return BadRequest("Link has expired");

            return true;
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<bool>> ResetPassword(PasswordResetDto passwordResetDto)
        {
            if (passwordResetDto.ResetToken.Length != 96)
                return BadRequest("Link is invalid");

            PasswordReset passwordReset = await _dataContext.PasswordReset.FirstOrDefaultAsync(x => x.ResetToken == passwordResetDto.ResetToken);

            if (passwordReset == null)
                return BadRequest("Link is invalid");

            if (!passwordReset.Active)
                return BadRequest("Link has expired");

            AppUser appUser = await _dataContext.AppUsers.FirstOrDefaultAsync(x => x.EmailAdress == passwordReset.EmailAddres);

            if (appUser == null)
                return BadRequest("User does not exist");

            using var hmac = new HMACSHA512();

            appUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordResetDto.NewPassword));
            appUser.PasswordSalt = hmac.Key;

            passwordReset.Active = false;

            _dataContext.AppUsers.Update(appUser);
            _dataContext.PasswordReset.Update(passwordReset);

            await _dataContext.SaveChangesAsync();

            return true;
        }

        [HttpPost("RequestResetEmail")]
        public async Task<ActionResult> RequestEmailReset(string email)
        {
            if (email == null)
                return BadRequest("Email is invalid");

            if (!email.Contains('@'))
                return BadRequest("Email is invalid");

            AppUser appUser = await _dataContext.AppUsers.FirstOrDefaultAsync(x => x.EmailAdress == email);

            if (appUser == null)
                return BadRequest("Email does not exist");

            string resetToken = PasswordTokenHelpers.RandomString();

            PasswordReset passwordReset = new PasswordReset
            {
                Active = true,
                EmailAddres = appUser.EmailAdress,
                ResetToken = resetToken,
                RequestGenerationData = DateTime.Now
            };

            _dataContext.PasswordReset.Add(passwordReset);
            await _dataContext.SaveChangesAsync();

            if (await _emailService.SendPasswordResetEmailAsync(email, appUser.UserName, resetToken))
                return Ok();

            return Unauthorized("Something went wrong. Try again later");
        }
    }
}

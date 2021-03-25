using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountConfirmationController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public AccountConfirmationController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("{ConfirmationToken}")]
        public async Task<ActionResult<EmailConfirmation>> ConfirmAccount(string ConfirmationToken)
        {
            EmailConfirmation email = await _dataContext.EmailConfirmation.FirstOrDefaultAsync(x => x.ConfirmationString == ConfirmationToken);

            if (email == null)
                return BadRequest("Link is invalid");

            if (email.Confirmed)
                return BadRequest("Email is already active");

            email.Confirmed = true;

            _dataContext.EmailConfirmation.Update(email);

            await _dataContext.SaveChangesAsync();

            return email;
        }

    }
}

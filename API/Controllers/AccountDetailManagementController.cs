using API.Controllers.Helpers;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountDetailManagementController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public AccountDetailManagementController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [Authorize]
        [HttpPost("ModifyAddress")]
        public async Task<ActionResult> ModifyAddress(UserAddressDto addressDto)
        {
            AppUser user = await _dataContext.AppUsers.Include(x => x.PersonalData.Adress).FirstOrDefaultAsync(x => x.Id == getIdFromClaims().Value);

            if (user == null)
                return Unauthorized("User does not exist");

            ConvertDtosToEntities.ConvertUserAddress(ref user, addressDto);

            _dataContext.AppUsers.Update(user);

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpPost("ModifyPersonalInfo")]
        public async Task<ActionResult> ModifyPersonalInfo(NameAndSurnameDto nameAndSurnameDto)
        {
            AppUser user = await _dataContext.AppUsers.Include(x => x.PersonalData.Adress).FirstOrDefaultAsync(x => x.Id == getIdFromClaims().Value);

            if (user == null)
                return Unauthorized("User does not exist");

            ConvertDtosToEntities.ConvertUserNameAndSurname(ref user, nameAndSurnameDto);

            _dataContext.AppUsers.Update(user);

            await _dataContext.SaveChangesAsync();

            return Ok();

        }

        [Authorize]
        [HttpGet("GetAddress")]
        public async Task<ActionResult<AppUserAddress>> GetAddress()
        {
            AppUser user = await _dataContext.AppUsers.Include(x => x.PersonalData.Adress).FirstOrDefaultAsync(x => x.Id == getIdFromClaims().Value);

            if (user == null)
                return Unauthorized("User does not exist");

            return user.PersonalData.Adress;
        }

        [Authorize]
        [HttpGet("GetPersonalInfo")]
        public async Task<ActionResult<AppUserPersonalData>> GetPersonalInfo()
        {
            AppUser user = await _dataContext.AppUsers.Include(x => x.PersonalData).FirstOrDefaultAsync(x => x.Id == getIdFromClaims().Value);

            if (user == null)
                return Unauthorized("User does not exist");

            return user.PersonalData;
        }

        private ActionResult<int> getIdFromClaims()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                Unauthorized("Invalid token");

            return int.Parse((identity.Claims.ToArray()[0]).Value);
        }
    }
}

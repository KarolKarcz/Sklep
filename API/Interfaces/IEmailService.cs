using API.DTOs;
using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendPasswordConfirmationEmailAsync(string emailAdress, string confirmationLink);
        Task<bool> SendPasswordResetEmailAsync(string emailAdress, string username, string resetLink);
    }
}

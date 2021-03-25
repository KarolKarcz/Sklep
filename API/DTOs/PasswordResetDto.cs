using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class PasswordResetDto
    {
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class PasswordReset
    {
        public int Id { get; set; }
        public string EmailAddres { get; set; }
        public string ResetToken { get; set; }
        public DateTime RequestGenerationData { get; set; }
        public bool Active { get; set; }
    }
}

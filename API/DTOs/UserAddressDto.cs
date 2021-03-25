using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserAddressDto
    {
        [Required]
        [MinLength(9)]
        public string TelephoneNumber { get; set; }
        [Required]
        public string StreetAndHouseNumber { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }

        //-------------------------------------------------//

        public string CompanyName { get; set; }
        public string Nip { get; set; }
    }
}

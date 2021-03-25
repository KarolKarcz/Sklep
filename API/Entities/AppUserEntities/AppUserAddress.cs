using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUserAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TelephoneNumber { get; set; }
        public string StreetAndHouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        //-------------------------------------------------//

        public string CompanyName { get; set; }
        public string Nip { get; set; }


    }
}

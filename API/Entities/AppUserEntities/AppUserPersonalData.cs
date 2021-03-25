using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUserPersonalData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Newsletter { get; set; }
        public virtual AppUserAddress Adress { get; set; }
    }
}

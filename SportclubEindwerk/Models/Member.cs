using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportclubEindwerk.Model
{
   public class Member
    {
        [Key]
        
        public Guid MemberId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }


        public virtual ICollection<SportClub> SportClubs { get; set; }

        public Address Address { get; set; }

    }
}

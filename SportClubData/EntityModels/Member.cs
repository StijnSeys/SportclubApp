using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportClub.Data.EntityModels
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


        public virtual ICollection<Club> SportClubs { get; set; }

        public virtual Address Address { get; set; }

    }
}

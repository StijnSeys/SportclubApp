using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SportclubEindwerk.Model
{
   public class SportClub
    {
        [Key]
        public Guid SportClubId { get; set; }
        [Required]
        public string Name { get; set; }
        public string ClubLogo { get; set; }
        public string ClubColor { get; set; }
        [Required]
        public string Password { get; set; }


        public virtual ICollection<Member> Members { get; set; }

        public virtual ICollection<Sport> Sports { get; set; }

        public Address Address { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportClub.Data.EntityModels
{
  public class Address
    {
        [Key]
        [Required]
        public Guid AddressId { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int PostCode { get; set; }

        public virtual ICollection<Member> AddressMembers { get; set; }

        public virtual  ICollection<Club> AddressSportClubs { get; set; } 
    }
}

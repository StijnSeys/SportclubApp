using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportClub.Data.EntityModels
{
    public class Sport
    {
        [Key]
        [Required]
        public Guid SportId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual IList<Material> SportMaterials { get; set; }

        public virtual IList<Club> SportClubs { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}

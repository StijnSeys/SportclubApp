using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportClub.Data.EntityModels
{
   public class Sport
    {
        [Key]
        
        public Guid SportId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Material> SportMaterials { get; set; }

        public virtual ICollection<Club> SportClubs { get; set; }
    }
}

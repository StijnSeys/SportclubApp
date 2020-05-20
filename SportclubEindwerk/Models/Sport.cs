﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sportclub.UI.Models
{
   public class Sport
    {
        [Key]
        
        public Guid SportId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Material> SportMaterials { get; set; }

        public virtual ICollection<SportClub> SportClubs { get; set; }
    }
}

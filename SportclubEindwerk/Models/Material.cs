﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Sportclub.UI.Models
{
 public class Material
    {
        [Key]
        public Guid MaterialId { get; set; }
        [Required]
        public string MaterialName { get; set; }
        [Required]
        public decimal Price { get; set; }



        public Sport Sport { get; set; }

        
    }
}

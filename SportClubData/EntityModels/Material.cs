using System;
using System.ComponentModel.DataAnnotations;

namespace SportClub.Data.EntityModels
{
 public class Material
    {
        [Key]
        public Guid MaterialId { get; set; }
        [Required]
        public string MaterialName { get; set; }
        [Required]
        public decimal Price { get; set; }


        public virtual Sport Sport { get; set; }

        
    }
}

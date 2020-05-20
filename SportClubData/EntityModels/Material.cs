using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportclubEindwerk.Model
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

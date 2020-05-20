using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportclubEindwerk.Model
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

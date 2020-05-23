using System;
using System.ComponentModel.DataAnnotations;

namespace Sportclub.UI.Models
{
 public class Material
    {
        public Guid MaterialId { get; set; }
       
        public string MaterialName { get; set; }
        
        public decimal Price { get; set; }


        
    }
}

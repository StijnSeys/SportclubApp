using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sportclub.UI.Models
{
  public  class Address
    {
       
        public Guid AddressId { get; set; }
        
        public string Street { get; set; }
        
        public string Number { get; set; }
        
        public string City { get; set; }
       
        public int PostCode { get; set; }

    }
}

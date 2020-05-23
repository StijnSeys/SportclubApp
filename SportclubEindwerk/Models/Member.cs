using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sportclub.UI.Models
{
   public class Member
    {
        
        
        public Guid MemberId { get; set; }
        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
       
        public string Email { get; set; }


    }
}

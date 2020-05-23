using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sportclub.UI.Models
{
   public class SportClub
    {
        
        public Guid SportClubId { get; set; }
       
        public string Name { get; set; }
        public string ClubLogo { get; set; }
        public string ClubColor { get; set; }
        public string Password { get; set; }


    }
}

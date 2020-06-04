using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportClub.Data.EntityModels;

namespace SportClub.UI.Models
{
 public  class MemberListModel
    {

        public Member Member { get; set; }

        public string MemberDisplay => $"{Member.FirstName} {Member.LastName}";


    }
}

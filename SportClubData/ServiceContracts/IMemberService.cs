using SportclubEindwerk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubData.Data
{
    public interface IMemberService
    {

        void CreateMember(Member member);

        void DeleteMember(Member member);

        void UpdateMember(Member member);

        ICollection<Member> GetMembers();


    }
}

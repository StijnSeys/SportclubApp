using System.Collections.Generic;
using SportClub.Data.EntityModels;

namespace SportClub.Data.ServiceContracts
{
    public interface IMemberService
    {
        
        void CreateMember(Member member);

        void DeleteMember(Member member);

       void UpdateMember(Member member);

        IList<Member> GetMembers();


    }
}

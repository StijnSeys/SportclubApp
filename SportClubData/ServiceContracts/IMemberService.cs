using SportClub.Data.EntityModels;
using System;
using System.Collections.Generic;

namespace SportClub.Data.ServiceContracts
{
    public interface IMemberService
    {
        
        void CreateMember(Member member);

        void DeleteMember(Member member);

       void UpdateMember(Member member);

        IList<Member> GetSportClubMembers(Guid clubId);

        bool CheckMember(Guid memberId);

        Member GetMember(Guid memberId);

    }
}

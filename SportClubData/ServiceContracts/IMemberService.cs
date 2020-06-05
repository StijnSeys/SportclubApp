using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using SportClub.Data.EntityModels;

namespace SportClub.Data.ServiceContracts
{
    public interface IMemberService
    {
        
        void CreateMember(Member member);

        void DeleteMember(Member member);

       void UpdateMember(Member member);

        IList<Member> GetMembers();

        bool CheckMember(Guid memberId);

        Member GetMember(Guid memberId);

    }
}

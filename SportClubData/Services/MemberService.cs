using System;
using System.Collections.Generic;
using SportClub.Data.DataContext;
using SportClub.Data.EntityModels;
using SportClub.Data.ServiceContracts;

namespace SportClub.Data.Services
{
  public class MemberService : IMemberService
    {

        private readonly SportClubDBContext _context;

        public MemberService(SportClubDBContext context)
        {
            _context = context;
        }

        public void CreateMember(Member member)
        {
           
        }

        public void DeleteMember(Member member)
        {
            throw new NotImplementedException();
        }

        public void UpdateMember(Member member)
        {
            throw new NotImplementedException();
        }

        public IList<Member> GetMembers()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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
            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public void DeleteMember(Member member)
        {
            _context.Members.Remove(member);
            _context.SaveChanges();
        }

        public void UpdateMember(Member member)
        {
            var updateMember = GetMember(member.MemberId);

            updateMember.MemberId = member.MemberId;
            updateMember.Address = member.Address;
            updateMember.Email = member.Email;
            updateMember.FirstName = member.FirstName;
            updateMember.LastName = member.LastName;
            updateMember.SportClubs = member.SportClubs;
            updateMember.Sports = member.Sports;

            _context.SaveChanges();
        }
        public IList<Member> GetSportClubMembers(Guid clubId)
        {

            var members = _context.Members.Where(m => m.SportClubs.Any(c => c.SportClubId == clubId));
            return members.ToList();

        }


        public bool CheckMember(Guid memberId)
        {
            var check = _context.Members.Any(m => m.MemberId == memberId);
            return check;
        }

        public Member GetMember(Guid memberId)
        {
            var member = _context.Members.FirstOrDefault(m => m.MemberId == memberId);
            return member;
        }
    }
}

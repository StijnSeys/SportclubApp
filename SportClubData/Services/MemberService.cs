using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportclubEindwerk.Model;

namespace SportClubData.Data
{
  public class MemberService : IMemberService
    {

        private readonly DbContext _context;

        public MemberService(DbContext context)
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

        public ICollection<Member> GetMembers()
        {
            throw new NotImplementedException();
        }
    }
}

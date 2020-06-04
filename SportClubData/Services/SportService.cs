using System.Collections.Generic;
using System.Linq;
using SportClub.Data.DataContext;
using SportClub.Data.EntityModels;
using SportClub.Data.ServiceContracts;

namespace SportClub.Data.Services
{
  public class SportService : ISportService
  {

      private readonly SportClubDBContext _context;

        public SportService(SportClubDBContext context)
        {

            _context = context;
        }

      public IList<Sport> GetSports()
      {
          var sports = _context.Sports;
          return sports.ToList();
      }
  }
}

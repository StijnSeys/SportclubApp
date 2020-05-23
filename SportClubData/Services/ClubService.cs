using System.Linq;
using SportClub.Data.EntityModels;
using SportClub.Data.ServiceContracts;
using SportClub.Data.DataContext;


namespace SportClub.Data.Services
{
  public class ClubService : IClubService
  {
      private readonly SportClubDBContext _dbContext;


      public ClubService(SportClubDBContext dbContext)
      {
          _dbContext = dbContext;
      }


      public EntityModels.Club LoginSportClub(string password, string clubName)
      {
         var club = _dbContext.Clubs.FirstOrDefault(s => s.Name == clubName && s.Password == password);
          return club;
      }

      public bool CheckSportClub(string clubName)
      {
          throw new System.NotImplementedException();
      }
  }
}

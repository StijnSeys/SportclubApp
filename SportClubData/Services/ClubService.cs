using System.ComponentModel;
using System.Data.Entity.Migrations;
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


      public Club LoginSportClub(string password, string clubName)
      {
          var club = _dbContext.Clubs.FirstOrDefault(s => s.Name == clubName && s.Password == password);
          return club;
      }

      public bool CheckSportClub(string clubName)
      {
          var check = _dbContext.Clubs.Any(c => c.Name == clubName);
          return check;
      }

      public void CreateSportClub(Club club)
      {
          _dbContext.Clubs.Add(club);
          _dbContext.SaveChanges();
      }

      public void DeleteSportClub(Club club)
      {
          _dbContext.Clubs.Remove(club);
          _dbContext.SaveChanges();

      }

      public void UpdateSportClub(Club club)
      {
         
          _dbContext.Clubs.AddOrUpdate(club);
          _dbContext.SaveChanges();
      }
  }
}

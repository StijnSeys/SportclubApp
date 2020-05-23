using SportClub.Data.EntityModels;

namespace SportClub.Data.ServiceContracts
{
  public interface IClubService
  {

    Club LoginSportClub(string password, string clubName);

    bool CheckSportClub(string clubName);

  }
}

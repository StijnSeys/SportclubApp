using System.Collections.Generic;
using SportClub.Data.EntityModels;

namespace SportClub.Data.ServiceContracts
{
  public interface ISportService
  {

      IList<Sport> GetSports();

      Sport GetSportByName(string name);
  }
}

using System;
using System.Collections.Generic;
using SportClub.Data.EntityModels;

namespace SportClub.Data.ServiceContracts
{
  public interface IMaterialService
    {

        void CreateMaterial(Material material);

        ICollection<Material> GetMaterialForSport(Guid sportId);

    }
}

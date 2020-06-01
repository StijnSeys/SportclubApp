using SportClub.Data.DataContext;
using SportClub.Data.EntityModels;
using SportClub.Data.ServiceContracts;

namespace SportClub.Data.Services
{
  public  class MaterialService : IMaterialService
    {

        private readonly SportClubDBContext _context;

        public MaterialService(SportClubDBContext dbContext)
        {
            _context = dbContext;
        }
        public void CreateMaterial(Material material)
        {

            _context.Materials.Add(material);
            _context.SaveChanges();
        }

    }
}

using SportClub.Data.DataContext;
using SportClub.Data.EntityModels;
using SportClub.Data.ServiceContracts;

namespace SportClub.Data.Services
{
    public class AddressService : IAddressService
    {
        private readonly SportClubDBContext _dbContext;

        public AddressService(SportClubDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateAddress(Address address)
        {
            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();
        }
    }
}

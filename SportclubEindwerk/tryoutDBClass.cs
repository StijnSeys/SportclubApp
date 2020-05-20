using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportclubEindwerk.Data;
using SportclubEindwerk.Model;

namespace SportclubEindwerk
{
    class tryoutDBClass
    {

        public void CheckUpp()
        {
            using (var context = new SportClubDbContext())
            {
                //dummy member
                var member = new Member()
                {
                    MemberId = Guid.NewGuid(), Email = "Stijnseys@gmail.com", FirstName = "Stijn", LastName = "Seys"
                };


                //dummy sportclub
                var sportclub = new SportClub()
                {
                    Name = "de dulle sporters", Password = "123456", SportClubId = Guid.NewGuid()
                };


                //Dummy address
                var address = new Address()
                {
                    AddressId = Guid.NewGuid(), City = "Kortrijk", Street = "KortrijkStraat", Number = "10A",
                    PostCode = 8500
                };
                var address2 = new Address()
                {
                    AddressId = Guid.NewGuid(), City = "Kortrijk", Street = "Bissegemstraat", Number = "1",
                    PostCode = 8500
                };


                //dummy Sports
                var voetbal = new Sport()
                {
                    SportId = Guid.NewGuid(), Name = "Voetbal"
                };

                var tennis = new Sport()
                {
                    SportId = Guid.NewGuid(), Name = "Tennis"
                };


                //dummy material
                var  material1 = new Material()
                {
                    MaterialId = Guid.NewGuid(), MaterialName = "Voetbal", Price = 20.0m
                };

                var material2 = new Material()
                {
                    MaterialId = Guid.NewGuid(), MaterialName = "Tennisbal", Price = 10.0m
                };



                //configure relations 
                material1.Sport = voetbal;
                material2.Sport = tennis;


                member.Address =  address;
                member.SportClubs = new List<SportClub>{sportclub};

                address2.AddressSportClubs = new List<SportClub>{sportclub};
                address.AddressMembers = new List<Member>{member};

                voetbal.SportClubs = new List<SportClub>{sportclub};
                voetbal.SportMaterials = new List<Material>{material1};

                tennis.SportClubs = new List<SportClub>{sportclub};
                tennis.SportMaterials = new List<Material>{material2};

                sportclub.Members = new List<Member>{member};
                sportclub.Sports = new List<Sport>{voetbal,tennis};
                sportclub.Address = address2;

                context.Addresses.Add(address);
                context.Members.Add(member);
                context.SaveChanges();


            }

        }
    }
}

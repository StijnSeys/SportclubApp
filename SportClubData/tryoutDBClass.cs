using System;
using System.Collections.Generic;
using SportClub.Data.DataContext;
using SportClub.Data.EntityModels;

namespace SportClub.Data
{
 public class tryoutDBClass
    {

        public void DummyData()
        {
            using (var context = new SportClubDBContext())
            {
                //dummy member
                var member = new Member()
                {
                    MemberId= Guid.NewGuid(),
                    Email = "stijnseys@gmail.com",
                    FirstName = "stijn",
                    LastName = "seys"
                };


                //dummy sportclub
                var club = new Club()
                {
                    Name = "de dulle sporters",
                    Password = "123456",
                    SportClubId =  Guid.NewGuid()
                };


                //dummy address
                var address = new Address()
                {
                    AddressId = Guid.NewGuid(),
                    City = "kortrijk",
                    Street = "kortrijkstraat",
                    Number = "10a",
                    PostCode  = 8500
                };
                var address2 = new Address()
                {
                    AddressId = Guid.NewGuid(),
                    City = "kortrijk",
                    Street = "bissegemstraat",
                    Number = "1",
                    PostCode = 8500
                };


                //dummy sports
                var voetbal = new Sport()
                {
                    SportId = Guid.NewGuid(),
                    Name = "voetbal"
                };

                var tennis = new Sport()
                {
                    SportId = Guid.NewGuid(),
                    Name = "tennis"
                };


                //dummy material
                var material1 = new Material()
                {
                    MaterialId = Guid.NewGuid(),
                    MaterialName = "voetbal",
                    Price = 20.0m
                };

                var material2 = new Material()
                {
                    MaterialId  = Guid.NewGuid(),
                    MaterialName  = "tennisbal",
                    Price = 10.0m
                };



                //configure relations 
                material1.Sport = voetbal;
                material2.Sport = tennis;


                member.Address = address;
                member.SportClubs = new List<Club> { club };

                address2.AddressSportClubs = new List<Club> { club };
                address.AddressMembers = new List<Member> { member };

                voetbal.SportClubs = new List<Club> { club };
                voetbal.SportMaterials = new List<Material> { material1 };

                tennis.SportClubs = new List<Club> { club };
                tennis.SportMaterials = new List<Material> { material2 };

                club.Members = new List<Member> { member };
                club.Sports = new List<Sport> { voetbal, tennis };
                club.Address = address2;

                //adding to db
                context.Clubs.Add(club);

                context.Addresses.Add(address);
                context.Addresses.Add(address2);

                context.Members.Add(member);

                context.Materials.Add(material1);
                context.Materials.Add(material2);

                context.Sports.Add(voetbal);
                context.Sports.Add(tennis);

                context.SaveChanges();


            }

        }
    }
}

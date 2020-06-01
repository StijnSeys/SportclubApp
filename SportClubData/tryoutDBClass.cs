﻿using System;
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
                var member1 = new Member()
                {
                    MemberId= Guid.NewGuid(),
                    Email = "stijnseys@gmail.com",
                    FirstName = "stijn",
                    LastName = "seys",
                    SportClubs = new List<Club>()


                };
                var member2 = new Member()
                {
                    MemberId = Guid.NewGuid(),
                    Email = "luclev@gmail.com",
                    FirstName = "Luc",
                    LastName = "lev",
                    SportClubs = new List<Club>()
                };
                var member3 = new Member()
                {
                    MemberId = Guid.NewGuid(),
                    Email = "amadeo@gmail.com",
                    FirstName = "Ama",
                    LastName = "deo",
                    SportClubs = new List<Club>()
                };

                //dummy sportclub
                var club = new Club()
                {
                    Name = "de dulle sporters",
                    Password = "123456",
                    SportClubId =  Guid.NewGuid(),
                    ClubLogo = "C:\\Users\\user\\Desktop\\Eindwerk\\afbeeldingen\\Diegem-Sport.png",
                    Members = new List<Member>(),
                    Sports = new List<Sport>()


                };

                var club2 = new Club()
                {
                    SportClubId = Guid.NewGuid(),
                    Name = "HeuleSport",
                    Password = "123456",
                    ClubLogo = "file:///C:/Users/user/Desktop/Eindwerk/afbeeldingen/Heule.jpg",
                    Members = new List<Member>(),
                    Sports = new List<Sport>()
                };

                //dummy address
                var address1 = new Address()
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
                var address3 = new Address()
                {
                    AddressId = Guid.NewGuid(),
                    City = "kortrijk",
                    Street = "Heulsestraat",
                    Number = "50",
                    PostCode = 8501
                };


                //dummy sports
                var voetbal = new Sport()
                {
                    SportId = Guid.NewGuid(),
                    Name = "voetbal",
                    SportClubs = new List<Club>()
                };

                var tennis = new Sport()
                {
                    SportId = Guid.NewGuid(),
                    Name = "tennis",
                    SportClubs = new List<Club>()
                };
                var basketbal = new Sport()
                {
                    SportId = Guid.NewGuid(),
                    Name = "Basket",
                    SportClubs = new List<Club>()

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

                var material3 = new Material()
                {
                    MaterialId = Guid.NewGuid(),
                    MaterialName = "basketbal",
                    Price = 20.0m
                };

              


                //configure relations 
                material1.Sport = voetbal;
                material2.Sport = tennis;
                material3.Sport = basketbal;

                member1.Address = address1;
                member2.Address = address2;
                member3.Address = address3;
                club.Address = address3;
                club2.Address = address2;

                voetbal.SportClubs.Add(club);
                club.Sports.Add(voetbal);

                basketbal.SportClubs.Add(club2);
                tennis.SportClubs.Add(club2);
                club2.Sports.Add(basketbal);
                club2.Sports.Add(tennis);

                club.Members.Add(member1);
                club.Members.Add(member2);
                club.Members.Add(member3);

                club2.Members.Add(member1);
                club2.Members.Add(member2);


                member1.SportClubs.Add(club);
                member1.SportClubs.Add(club2);

                member2.SportClubs.Add(club);
                member2.SportClubs.Add(club2);

                member3.SportClubs.Add(club);



                //adding to db
                context.Clubs.Add(club);
                context.Clubs.Add(club2);

                context.Addresses.Add(address1);
                context.Addresses.Add(address2);
                context.Addresses.Add(address3);

                context.Members.Add(member1);
                context.Members.Add(member2);
                context.Members.Add(member3);

                context.Materials.Add(material1);
                context.Materials.Add(material2);
                context.Materials.Add(material3);

                context.Sports.Add(voetbal);
                context.Sports.Add(tennis);
                context.Sports.Add(basketbal);



                //save changes
                context.SaveChanges();


            }

        }
    }
}

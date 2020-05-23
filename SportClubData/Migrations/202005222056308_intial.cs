namespace SportClub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Guid(nullable: false),
                        Street = c.String(nullable: false),
                        Number = c.String(nullable: false),
                        City = c.String(nullable: false),
                        PostCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Address_AddressId = c.Guid(),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.Clubs",
                c => new
                    {
                        SportClubId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        ClubLogo = c.String(),
                        ClubColor = c.String(),
                        Password = c.String(nullable: false),
                        Address_AddressId = c.Guid(),
                    })
                .PrimaryKey(t => t.SportClubId)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.Sports",
                c => new
                    {
                        SportId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SportId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Guid(nullable: false),
                        MaterialName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sport_SportId = c.Guid(),
                    })
                .PrimaryKey(t => t.MaterialId)
                .ForeignKey("dbo.Sports", t => t.Sport_SportId)
                .Index(t => t.Sport_SportId);
            
            CreateTable(
                "dbo.ClubMembers",
                c => new
                    {
                        Club_SportClubId = c.Guid(nullable: false),
                        Member_MemberId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Club_SportClubId, t.Member_MemberId })
                .ForeignKey("dbo.Clubs", t => t.Club_SportClubId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Member_MemberId, cascadeDelete: true)
                .Index(t => t.Club_SportClubId)
                .Index(t => t.Member_MemberId);
            
            CreateTable(
                "dbo.SportClubs",
                c => new
                    {
                        Sport_SportId = c.Guid(nullable: false),
                        Club_SportClubId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sport_SportId, t.Club_SportClubId })
                .ForeignKey("dbo.Sports", t => t.Sport_SportId, cascadeDelete: true)
                .ForeignKey("dbo.Clubs", t => t.Club_SportClubId, cascadeDelete: true)
                .Index(t => t.Sport_SportId)
                .Index(t => t.Club_SportClubId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materials", "Sport_SportId", "dbo.Sports");
            DropForeignKey("dbo.SportClubs", "Club_SportClubId", "dbo.Clubs");
            DropForeignKey("dbo.SportClubs", "Sport_SportId", "dbo.Sports");
            DropForeignKey("dbo.ClubMembers", "Member_MemberId", "dbo.Members");
            DropForeignKey("dbo.ClubMembers", "Club_SportClubId", "dbo.Clubs");
            DropForeignKey("dbo.Clubs", "Address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Members", "Address_AddressId", "dbo.Addresses");
            DropIndex("dbo.SportClubs", new[] { "Club_SportClubId" });
            DropIndex("dbo.SportClubs", new[] { "Sport_SportId" });
            DropIndex("dbo.ClubMembers", new[] { "Member_MemberId" });
            DropIndex("dbo.ClubMembers", new[] { "Club_SportClubId" });
            DropIndex("dbo.Materials", new[] { "Sport_SportId" });
            DropIndex("dbo.Clubs", new[] { "Address_AddressId" });
            DropIndex("dbo.Members", new[] { "Address_AddressId" });
            DropTable("dbo.SportClubs");
            DropTable("dbo.ClubMembers");
            DropTable("dbo.Materials");
            DropTable("dbo.Sports");
            DropTable("dbo.Clubs");
            DropTable("dbo.Members");
            DropTable("dbo.Addresses");
        }
    }
}

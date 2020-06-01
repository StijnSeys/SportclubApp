namespace SportClub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clubs", "ClubLogo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clubs", "ClubLogo");
        }
    }
}

namespace SportClub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clubs", "ClubLogo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clubs", "ClubLogo");
        }
    }
}

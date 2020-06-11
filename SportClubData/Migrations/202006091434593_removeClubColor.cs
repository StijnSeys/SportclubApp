namespace SportClub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeClubColor : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Clubs", "ClubColor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clubs", "ClubColor", c => c.String());
        }
    }
}

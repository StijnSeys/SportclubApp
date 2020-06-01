using System.Data.Entity;
using SportClub.Data.EntityModels;

namespace SportClub.Data.DataContext
{
 public class SportClubDBContext : DbContext
    {

        public SportClubDBContext() : base("name=SportClubEntities")
        { 
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SportClubDBContext, Migrations.Configuration>());
        }
        
        public  DbSet<Address> Addresses { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Club> Clubs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}

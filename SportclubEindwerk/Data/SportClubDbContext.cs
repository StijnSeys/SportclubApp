using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportclubEindwerk.Model;

namespace SportclubEindwerk.Data
{
    class SportClubDbContext : DbContext
    {

        public SportClubDbContext() : base("name=SportClubEntities")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SportClubDbContext, SportclubEindwerk.Migrations.Configuration>());
        }
     

        public  DbSet<Address> Addresses { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<SportClub> SportClubs { get; set; }



        // public SportClubDbContext(DbContextOptions<SportClubDbContext> contextOptions)

        //??
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}

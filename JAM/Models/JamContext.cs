using System.Data.Entity;

namespace JAM.Models
{
    public class JamContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        //
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        //
        //// System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MvcMatcher.Models.MvcMatcherContext>());

        public JamContext()
            : base("ApplicationServices")
        {
        }

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<WantedSurvey> WantedSurveys { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<KidWantedCount> KidWantedCounts { get; set; }

        public DbSet<WantedKidWantedCount> WantedKidWantedCounts { get; set; }

        public DbSet<KidCount> KidCounts { get; set; }

        public DbSet<DatePackage> DatePackages { get; set; }

        public DbSet<Referrer> Referrers { get; set; }
    }
}
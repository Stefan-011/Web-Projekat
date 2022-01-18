using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ProjectContext : DbContext
    {
        public DbSet<Igrac> Igraci { get; set; }
        public DbSet<ETeam> ETeamovi { get; set; }
        public DbSet<Sponzor> Sponzori { get; set; }
        public DbSet<Pozicija> Pozicije { get; set; }
        public DbSet<Trener> Treneri { get; set; }
        public ProjectContext(DbContextOptions Options) : base(Options)
        {

        }
        
       protected override void OnModelCreating(ModelBuilder Builder)
       {
           base.OnModelCreating(Builder);
            /*Build started...
              Build succeeded.
              Done. To undo this action, use 'ef migrations remove'*/
       }
    }
}
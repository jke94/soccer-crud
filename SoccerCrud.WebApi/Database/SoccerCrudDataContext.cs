namespace SoccerCrud.WebApi.Database
{
    using SoccerCrud.WebApi.Entities;
    using Microsoft.EntityFrameworkCore;

    public class SoccerCrudDataContext : DbContext
    {
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Season> Seasons { get; set; }
        public virtual DbSet<Stadium> Stadiums { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }   

        public SoccerCrudDataContext(
            DbContextOptions<SoccerCrudDataContext> context
            ) : base(context)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

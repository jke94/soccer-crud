namespace SoccerCrud.WebApi
{
    using SoccerCrud.WebApi.Entities;
    using Microsoft.EntityFrameworkCore;

    public class SoccerCrudDataContext : DbContext
    {
        public virtual DbSet<Player> Players { get;set; }
        public virtual DbSet<Team> Team { get; set; }

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

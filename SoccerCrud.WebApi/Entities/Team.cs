namespace SoccerCrud.WebApi.Entities
{
    public class Team
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }

        public Guid StadiumId { get; set; }

        public ICollection<TeamHome> TeamHomes { get; set; }

        public ICollection<TeamAway> TeamAways { get; set; }
    }
}

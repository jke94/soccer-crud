namespace SoccerCrud.WebApi.Entities
{
    public class Stadium
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Name { get; set; }

        public Guid TeamId { get; set; }

        public ICollection<Match> Matchs { get; set; }
    }
}

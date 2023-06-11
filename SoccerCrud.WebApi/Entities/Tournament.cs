namespace SoccerCrud.WebApi.Entities
{
    public class Tournament
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Name { get; set; }

        public Guid SeasonId { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}

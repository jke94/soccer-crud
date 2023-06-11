namespace SoccerCrud.WebApi.Entities
{
    public class TeamHome
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid TeamId { get; init; }

        public Guid MatchId { get; init; }
    }
}

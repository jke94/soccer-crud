namespace SoccerCrud.WebApi.Entities
{
    public class Match
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid TournamentId { get; set; }

        public ICollection<Goal> Goals { get; set; }

        public Guid StadiumId { get; set; }
    }
}

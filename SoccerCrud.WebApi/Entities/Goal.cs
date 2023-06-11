namespace SoccerCrud.WebApi.Entities
{
    public class Goal
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public int GoalMinute { get; set; }

        public Guid MatchId { get; set; }

        public Guid PlayerId { get; set;}
    }
}

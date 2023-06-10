namespace SoccerCrud.WebApi.Entities
{
    public class Player
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Name { get; set; }

        public Guid TeamId { get; set; }
    }
}

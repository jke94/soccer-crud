namespace SoccerCrud.WebApi.Entities
{
    public class Season
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public int YearStart { get; init; }
        public int YearEnd { get; init; }

        public ICollection<Tournament> Tournaments { get; set; }
    }
}

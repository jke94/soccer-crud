namespace SoccerCrud.WebApi.Database.Seeds
{
    public interface IDataSeed
    {
        Task SeedData(SoccerCrudDataContext soccerCrudDataContext);
    }

    public class DataSeed : IDataSeed
    {
        public async Task SeedData(SoccerCrudDataContext soccerCrudDataContext)
        {
            soccerCrudDataContext.Team.AddRange(TeamDataSeed.GetTeams());
            soccerCrudDataContext.Players.AddRange(PlayerDataSeed.GetPlayers());

            await soccerCrudDataContext.SaveChangesAsync();
        }
    }
}

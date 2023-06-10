namespace SoccerCrud.WebApi.Database.Seeds
{
    using SoccerCrud.WebApi.Entities;

    public static class PlayerDataSeed
    {
        public static Player[] GetPlayers()
        {
            return new Player[]
            {
                new Player()
                {
                    Id = new Guid("c9129f10-4492-4f9a-9351-091618945348"),
                    Name = "De Gea",
                    TeamId = new Guid("69597e82-693c-4f49-a9f1-97eabf368018")
                }
            };
        }
    }
}

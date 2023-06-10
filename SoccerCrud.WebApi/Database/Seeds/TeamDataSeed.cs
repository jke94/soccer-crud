namespace SoccerCrud.WebApi.Database.Seeds
{
    using SoccerCrud.WebApi.Entities;

    public static class TeamDataSeed
    {
        public static Team[] GetTeams()
        {
            return new Team[]
            {
                new Team()
                {
                    Id = new Guid("69597e82-693c-4f49-a9f1-97eabf368018"),
                    Name = "Manchester United"
                },
                new Team()
                {
                    Id = new Guid("eee87c0c-d6c7-4a34-92ea-56b075cc71ee"),
                    Name = "Chelsea",

                },
                new Team()
                {
                    Id = new Guid("f186ea9e-8165-41f0-a9eb-1e3cfddef12b"),
                    Name = "Liverpool",
                    Players = new List<Player>()
                    {
                        new Player()
                        {
                            Id = new Guid("0eb1021e-28f6-4731-be11-b73f704a89bd"),
                            Name = "Mohamed Salah",
                            TeamId = new Guid("f186ea9e-8165-41f0-a9eb-1e3cfddef12b")
                        },
                        new Player()
                        {
                            Id = new Guid("b34b254d-5015-4a06-bdd9-3324210aae0d"),
                            Name = "Alison",
                            TeamId = new Guid("f186ea9e-8165-41f0-a9eb-1e3cfddef12b")
                        },
                        new Player()
                        {
                            Id = new Guid("bb45a182-504f-4030-9a6d-962111fd7edd"),
                            Name = "Luis Díaz",
                            TeamId = new Guid("f186ea9e-8165-41f0-a9eb-1e3cfddef12b")
                        }
                    }
                },
                new Team()
                {
                    Id = new Guid("665f8f43-9300-424c-ab0f-6b3dbc01024d"),
                    Name = "Everton",
                    Players = new List<Player>
                    {
                        new Player()
                        {
                            Id = new Guid("925e5a7e-76c1-49f4-9f5a-758ad0d766f2"),
                            Name = "Jordan Pickfordz",
                            TeamId = new Guid("665f8f43-9300-424c-ab0f-6b3dbc01024d")
                        },
                        new Player()
                        {
                            Id = new Guid(),
                            Name = "Michael Keane",
                            TeamId = new Guid("665f8f43-9300-424c-ab0f-6b3dbc01024d")
                        }

                    }
                }
            };
        }
    }
}

namespace SoccerCrud.WebApi.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SoccerCrud.WebApi.Database;
    using SoccerCrud.WebApi.Dto;
    using SoccerCrud.WebApi.Entities;

    public interface IPlayerRepository
    {
        Task<CreatedPlayerDto?> CreateAsync(CreatePlayerDto createPlayerDto);
        Task<PlayerDto?> GetAsyncById(Guid id);
        Task<IList<PlayerDto>> GetAllAsync();
        Task<PlayerDto?> UpdateAsync(Guid id, UpdatePlayerDto updatePlayerDto);
        Task<bool> DeleteAsync(Guid id);
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly SoccerCrudDataContext _soccerCrudDataContext;

        public PlayerRepository(SoccerCrudDataContext soccerCrudDataContext)
        {
            _soccerCrudDataContext = soccerCrudDataContext;
        }

        public async Task<CreatedPlayerDto?> CreateAsync(CreatePlayerDto createPlayerDto)
        {
            var entityEntry = await _soccerCrudDataContext.AddAsync(new Player()
            {
                Name = createPlayerDto.Name
            });

            await _soccerCrudDataContext.SaveChangesAsync();

            var createdPlayerDto = new CreatedPlayerDto()
            {
                Id = entityEntry.Entity.Id,
                Name = entityEntry.Entity.Name
            };

            return createdPlayerDto;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<PlayerDto>?> GetAllAsync()
        {
            var taskResult = await _soccerCrudDataContext.Players.ToListAsync();

            if (taskResult == null)
            {
                return null;
            }

            var teams = new List<PlayerDto>();

            foreach (var item in taskResult)
            {
                teams.Add(new PlayerDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }

            return teams;
        }

        public Task<PlayerDto?> GetAsyncById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerDto?> UpdateAsync(Guid id, UpdatePlayerDto updatePlayerDto)
        {
            throw new NotImplementedException();
        }
    }
}

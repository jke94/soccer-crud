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

        public async Task<bool> DeleteAsync(Guid id)
        {
            var taskResult = await _soccerCrudDataContext.Players.FirstOrDefaultAsync(x => x.Id == id);

            if (taskResult == null)
            {
                return false;
            }

            var entity = _soccerCrudDataContext.Remove(taskResult);

            if (taskResult == null)
            {
                return false;
            }

            var result = await _soccerCrudDataContext.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
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

        public async Task<PlayerDto?> GetAsyncById(Guid id)
        {
            var taskResult = await _soccerCrudDataContext.Players.FirstOrDefaultAsync(x => x.Id == id);

            if (taskResult == null)
            {
                return null;
            }

            var playerDto = new PlayerDto()
            {
                Id = taskResult.Id,
                Name = taskResult.Name
            };

            return playerDto;
        }

        public async Task<PlayerDto?> UpdateAsync(Guid id, UpdatePlayerDto updatePlayerDto)
        {
            var player = await _soccerCrudDataContext.Players.FirstOrDefaultAsync(x => x.Id == id);

            if (player == null)
            {
                return null;
            }

            player.Name = updatePlayerDto.Name;

            var entity = _soccerCrudDataContext.Update(player);

            if (entity == null)
            {
                return null;
            }

            await _soccerCrudDataContext.SaveChangesAsync();

            var playerDto = new PlayerDto()
            {
                Id = entity.Entity.Id,
                Name = entity.Entity.Name
            };

            return playerDto;
        }
    }
}

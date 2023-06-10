namespace SoccerCrud.WebApi.Repositories
{
    using SoccerCrud.WebApi.Database;
    using SoccerCrud.WebApi.Dto;
    using SoccerCrud.WebApi.Entities;

    public interface IPlayerRepository
    {
        Task<CreatedPlayerDto?> CreateAsync(CreatePlayerDto createTeamDto);
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

        public Task<CreatedPlayerDto?> CreateAsync(CreatePlayerDto createTeamDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<PlayerDto>> GetAllAsync()
        {
            throw new NotImplementedException();
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

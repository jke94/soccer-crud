namespace SoccerCrud.WebApi.Repositories
{
    using SoccerCrud.WebApi.Dto;
    using SoccerCrud.WebApi.Entities;

    public interface IPlayerRepository
    {
        Task<CreatePlayerDto> CreateAsync(Player user);
        Task<PlayerDto?> GetAsyncById(int id);
        Task<IList<PlayerDto>> GetAllAsync();
        Task UpdateAsync(UpdatePlayerDto user);
        Task<bool> DeleteAsync(int id);
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly SoccerCrudDataContext _soccerCrudDataContext;

        public PlayerRepository(SoccerCrudDataContext soccerCrudDataContext)
        {
            _soccerCrudDataContext = soccerCrudDataContext;
        }

        public async Task<CreatePlayerDto> CreateAsync(Player user)
        {
            throw new NotImplementedException();
        }

        private void AddAsync(Player user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<PlayerDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PlayerDto?> GetAsyncById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdatePlayerDto user)
        {
            throw new NotImplementedException();
        }
    }
}

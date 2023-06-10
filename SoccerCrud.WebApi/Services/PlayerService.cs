using SoccerCrud.WebApi.Dto;

namespace SoccerCrud.WebApi.Services
{
    public interface IPlayerService
    {
        Task<CreatedPlayerDto?> CreateAsync(CreatePlayerDto createPlayerDto);

        Task<PlayerDto?> GetAsync(Guid id);

        Task<IList<PlayerDto>> GetAllAsync();

        Task<PlayerDto?> UpdateAsync(Guid id, UpdatePlayerDto updatePlayerDto);

        Task<bool> DeleteAsync(Guid id);
    }

    public class PlayerService : IPlayerService
    {
        public Task<CreatedPlayerDto?> CreateAsync(CreatePlayerDto createPlayerDto)
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

        public Task<PlayerDto?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerDto?> UpdateAsync(Guid id, UpdatePlayerDto updatePlayerDto)
        {
            throw new NotImplementedException();
        }
    }
}

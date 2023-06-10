namespace SoccerCrud.WebApi.Services
{
    using SoccerCrud.WebApi.Dto;
    using SoccerCrud.WebApi.Repositories;

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
        private readonly IUnitOfWork _unitOfWork;

        public PlayerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedPlayerDto?> CreateAsync(CreatePlayerDto createPlayerDto)
        {
            var taskResult = await _unitOfWork.PlayerRepository.CreateAsync(createPlayerDto);

            return taskResult;
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

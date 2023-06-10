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

        public async Task<bool> DeleteAsync(Guid id)
        {
            var taskResult = await _unitOfWork.PlayerRepository.DeleteAsync(id);

            return taskResult;
        }

        public async Task<IList<PlayerDto>> GetAllAsync()
        {
            return await _unitOfWork.PlayerRepository.GetAllAsync();
        }

        public async Task<PlayerDto?> GetAsync(Guid id)
        {
            var taskResult = await _unitOfWork.PlayerRepository.GetAsyncById(id);

            if (taskResult == null)
            {
                return null;
            }

            return taskResult;
        }

        public async Task<PlayerDto?> UpdateAsync(Guid id, UpdatePlayerDto updatePlayerDto)
        {
            var taskResult = await _unitOfWork.PlayerRepository.UpdateAsync(id, updatePlayerDto);

            if (taskResult == null)
            {
                return null;
            }

            return taskResult;
        }
    }
}

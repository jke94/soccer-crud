namespace SoccerCrud.WebApi.Services
{
    using SoccerCrud.WebApi.Dto;
    using SoccerCrud.WebApi.Repositories;

    public interface ITeamService
    {
        Task<CreatedTeamDto?> CreateAsync(CreateTeamDto createTeamDto);
        Task<TeamDto?> GetAsync(Guid id);
        Task<IList<TeamDto>> GetAllAsync();
        Task<TeamDto?> UpdateAsync(Guid id, UpdateTeamDto updateTeamDto);
        Task<bool> DeleteAsync(Guid id);
    }

    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedTeamDto?> CreateAsync(CreateTeamDto createTeamDto)
        {
            var taskResult = await _unitOfWork.TeamRepository.CreateAsync(createTeamDto);

            return taskResult;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var taskResult = await _unitOfWork.TeamRepository.DeleteAsync(id);

            return taskResult;
        }

        public Task<IList<TeamDto>> GetAllAsync()
        {
            return _unitOfWork.TeamRepository.GetAllAsync();
        }

        public async Task<TeamDto?> GetAsync(Guid id)
        {
            var taskResult = await _unitOfWork.TeamRepository.GetAsyncById(id);

            if (taskResult == null)
            {
                return null;
            }

            return taskResult;
        }

        public async Task<TeamDto?> UpdateAsync(Guid id, UpdateTeamDto updateTeamDto)
        {
            var taskResult = await _unitOfWork.TeamRepository.UpdateAsync(id, updateTeamDto);

            if (taskResult == null)
            {
                return null;
            }

            return taskResult;
        }
    }
}

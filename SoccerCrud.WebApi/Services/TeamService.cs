namespace SoccerCrud.WebApi.Services
{
    using SoccerCrud.WebApi.Dto;
    using SoccerCrud.WebApi.Entities;
    using SoccerCrud.WebApi.Repositories;

    public interface ITeamService
    {
        Task<CreatedTeamDto?> CreateAsync(CreateTeamDto createTeamDto);

        Task<TeamDto?> GetAsync(Guid id);

        Task<IEnumerable<Team>> GetAllAsync();

        Task<bool> UpdateAsync(UpdateTeamDto updateTeamDto);

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

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Team>> GetAllAsync()
        {
            throw new NotImplementedException();
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

        public Task<bool> UpdateAsync(UpdateTeamDto updateTeamDto)
        {
            throw new NotImplementedException();
        }
    }
}

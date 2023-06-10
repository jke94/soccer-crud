namespace SoccerCrud.WebApi.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using SoccerCrud.WebApi.Dto;
    using SoccerCrud.WebApi.Entities;

    public interface ITeamRepository
    {
        Task<CreatedTeamDto?> CreateAsync(CreateTeamDto createTeamDto);
        Task<TeamDto?> GetAsyncById(Guid id);
        Task<IList<TeamDto>> GetAllAsync();
        Task UpdateAsync(UpdateTeamDto user);
        Task<bool> DeleteAsync(Guid id);
    }

    public class TeamRepository : ITeamRepository
    {
        private readonly SoccerCrudDataContext _soccerCrudDataContext;

        public TeamRepository(SoccerCrudDataContext soccerCrudDataContext)
        {
            _soccerCrudDataContext = soccerCrudDataContext;
        }

        public async Task<CreatedTeamDto?> CreateAsync(CreateTeamDto createTeamDto)
        {
            var entityEntry = await _soccerCrudDataContext.AddAsync(new Team()
            {
                Name = createTeamDto.Name
            });

            await _soccerCrudDataContext.SaveChangesAsync();

            var createdTeamDto = new CreatedTeamDto()
            {
                Id = entityEntry.Entity.Id,
                Name = entityEntry.Entity.Name
            };

            return createdTeamDto;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TeamDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TeamDto?> GetAsyncById(Guid id)
        {
            var taskResult = await _soccerCrudDataContext.Team.FirstOrDefaultAsync(x => x.Id == id);

            if(taskResult == null)
            {
                return null;
            }

            var teamDto = new TeamDto()
            {
                Id = taskResult.Id,
                Name = taskResult.Name
            };

            return teamDto;
        }

        public Task UpdateAsync(UpdateTeamDto user)
        {
            throw new NotImplementedException();
        }
    }
}

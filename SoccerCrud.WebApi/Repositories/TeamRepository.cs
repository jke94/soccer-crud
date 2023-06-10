namespace SoccerCrud.WebApi.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SoccerCrud.WebApi.Database;
    using SoccerCrud.WebApi.Dto;
    using SoccerCrud.WebApi.Entities;

    public interface ITeamRepository
    {
        Task<CreatedTeamDto?> CreateAsync(CreateTeamDto createTeamDto);
        Task<TeamDto?> GetAsyncById(Guid id);
        Task<IList<TeamDto>> GetAllAsync();
        Task<TeamDto?> UpdateAsync(Guid id, UpdateTeamDto updateTeamDto);
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

        public async Task<bool> DeleteAsync(Guid id)
        {
            var taskResult = await _soccerCrudDataContext.Team.FirstOrDefaultAsync(x => x.Id == id);

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

            if (result > 0 )
            {
                return true;
            }

            return false;
        }

        public async Task<IList<TeamDto>?> GetAllAsync()
        {
            var taskResult = await _soccerCrudDataContext.Team.ToListAsync();

            if (taskResult == null)
            {
                return null;
            }

            var teams = new List<TeamDto>();

            foreach (var item in taskResult)
            {
                teams.Add(new TeamDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }

            return teams;
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

        public async Task<TeamDto?> UpdateAsync(Guid id, UpdateTeamDto updateTeamDto)
        {
            var team = await _soccerCrudDataContext.Team.FirstOrDefaultAsync(x => x.Id == id);

            if(team == null)
            {
                return null;
            }

            team.Name = updateTeamDto.Name;

            var entity = _soccerCrudDataContext.Update(team);

            if (entity == null)
            {
                return null;
            }

            await _soccerCrudDataContext.SaveChangesAsync();

            var teamDto = new TeamDto()
            {
                Id = entity.Entity.Id,
                Name = entity.Entity.Name
            };

            return teamDto;
        }
    }
}

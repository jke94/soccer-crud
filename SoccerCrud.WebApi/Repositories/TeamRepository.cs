namespace SoccerCrud.WebApi.Repositories
{
    using SoccerCrud.WebApi.Dto;
    using SoccerCrud.WebApi.Entities;

    public interface ITeamRepository
    {
        Task<CreatedTeamDto?> CreateAsync(CreateTeamDto createTeamDto);
        Task<TeamDto?> GetAsyncById(int id);
        Task<IList<TeamDto>> GetAllAsync();
        Task UpdateAsync(UpdateTeamDto user);
        Task<bool> DeleteAsync(int id);
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

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TeamDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TeamDto?> GetAsyncById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateTeamDto user)
        {
            throw new NotImplementedException();
        }
    }
}

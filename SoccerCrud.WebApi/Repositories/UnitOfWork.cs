namespace SoccerCrud.WebApi.Repositories
{
    public interface IUnitOfWork
    {
        IPlayerRepository PlayerRepository { get; }
        ITeamRepository TeamRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;

        public UnitOfWork(
            IPlayerRepository playerRepository, 
            ITeamRepository teamRepository)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        }

        public IPlayerRepository PlayerRepository => _playerRepository;

        public ITeamRepository TeamRepository => _teamRepository;
    }
}

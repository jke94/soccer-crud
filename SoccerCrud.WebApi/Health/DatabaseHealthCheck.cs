namespace SoccerCrud.WebApi.Health
{
    #region using

    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using SoccerCrud.WebApi.Repositories;

    #endregion

    public class DatabaseHealthCheck : IHealthCheck
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;

        public DatabaseHealthCheck(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, 
            CancellationToken cancellationToken = new ())
        {
            try
            {
                var player = await _unitOfWork.PlayerRepository.GetAsyncById(new Guid());

                return HealthCheckResult.Healthy();
            }
            catch (Exception exception)
            {
                return HealthCheckResult.Unhealthy(exception: exception);
            }
        }
    }
}

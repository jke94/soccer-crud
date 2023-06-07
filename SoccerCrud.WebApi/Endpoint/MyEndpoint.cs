namespace SoccerCrud.WebApi.Endpoint
{
    using SoccerCrud.WebApi.DTO;

    public class MyEndpoint : Endpoint<MyRequest>
    {
        public override void Configure()
        {
            Post("/api/user/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(MyRequest request, CancellationToken cancellationToken)
        {
            var response = new MyResponse()
            {
                FullName = request.FirstName + " " + request.LastName,
                IsOver18 = request.Age > 18
            };

            await SendAsync(response);
        }
    }
}

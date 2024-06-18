
using Fina.Api.Common.Api;
using Fina.Common.Models;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;
using Fina.Common.Services;

namespace Fina.Api.EndPoints.Transactions
{
    public class CreateTransactionEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("Transactions: Create")
                .WithSummary("Cria uma nova transação")
                .WithDescription("Cria uma nova transação")
                .WithOrder(1)
                .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(
            ITransactionService service,
            CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await service.CreateAsync(request);
            return response.IsSuccess ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response) 
                : TypedResults.BadRequest(response);
        }
    }
}
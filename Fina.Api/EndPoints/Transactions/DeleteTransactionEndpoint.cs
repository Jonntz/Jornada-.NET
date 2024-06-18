
using Fina.Api.Common.Api;
using Fina.Common.Models;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;
using Fina.Common.Services;

namespace Fina.Api.EndPoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HandleAsync)
                .WithName("Transactions: Delete")
                .WithSummary("Exclui uma nova transação")
                .WithDescription("Exclui uma nova transação")
                .WithOrder(1)
                .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(
            ITransactionService service, long id)
        {
            var request = new DeleteTransactionRequest()
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var response = await service.DeleteAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }
    }
}
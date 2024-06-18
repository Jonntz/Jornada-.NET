using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fina.Api.Common.Api;
using Fina.Common.Models;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;
using Fina.Common.Services;

namespace Fina.Api.EndPoints.Transactions
{
    public class GetByIdTransactionEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                .WithName("Transactions: Get By ID")
                .WithSummary("Recupera uma transação")
                .WithDescription("Recupera uma transação")
                .WithOrder(4)
                .Produces<Response<Transaction>?>();
        
        private static async Task<IResult> HandleAsync(
            ITransactionService service, long id)
        {
            var request = new GetByIdTransactionRequest() 
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var response = await service.GetByIdAsync(request);
            return response.IsSuccess ? TypedResults.Ok(response) 
                : TypedResults.BadRequest(response);
        }
    }
}
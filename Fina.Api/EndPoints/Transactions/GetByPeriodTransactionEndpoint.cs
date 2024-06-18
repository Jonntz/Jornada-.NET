using Fina.Api.Common.Api;
using Fina.Common;
using Fina.Common.Models;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;
using Fina.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.EndPoints.Transactions
{
    public class GetByPeriodTransactionEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithName("Transactions: Get All")
                .WithSummary("Recupera todas as categorias")
                .WithDescription("Recupera todas as categorias")
                .WithOrder(5)
                .Produces<PagedResponse<List<Transaction>?>>();
        
        private static async Task<IResult> HandleAsync(
            ITransactionService service,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetByPeriodTransactionRequest()
            {
                UserId = ApiConfiguration.UserId,
                StartDate = startDate,
                EndDate = endDate,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var result = await service.GetByPeriodAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}